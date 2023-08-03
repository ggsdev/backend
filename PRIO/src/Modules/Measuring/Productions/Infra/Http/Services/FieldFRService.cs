using AutoMapper;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Services
{
    public class FieldFRService
    {
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;

        public FieldFRService(IMapper mapper, IInstallationRepository installationRepository, IFieldRepository fieldRepository)
        {
            _mapper = mapper;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
        }

        public async Task<List<FRFieldDTO>> ApplyFR(CreateFRsFieldsViewModel body)
        {
            var installation = await _installationRepository.GetByIdAsync(body.InstallationId);

            if (installation is null)
                throw new NotFoundException("Instalação não encontrada.");

            if (installation.IsProcessingUnit == false)
                throw new ConflictException("Instalação não é uma unidade de processamento.");

            var installationsWithFields = await _installationRepository.GetByUEPWithFieldsCod(installation.UepCod);

            foreach (var installationUEP in installationsWithFields)
            {
                foreach (var field in installationUEP.Fields)
                {
                    if (!body.Fields.Any(x => x.FieldId == field.Id))
                        throw new ConflictException(ErrorMessages.NotFound<Field>());
                }
            }

            foreach (var item in body.Fields)
            {
                var field = await _fieldRepository.GetByIdAsync(item.FieldId);
                if (field is null)
                    throw new NotFoundException("Campo não encontrado");
            }

            if (body.isApplicableFROil is true)
            {
                decimal? sumOil = 0;
                foreach (var item in body.Fields)
                {
                    if (item.OilFR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    sumOil += item.OilFR;
                }
                if (sumOil != 1)
                    throw new ConflictException("Óleo: Soma dos fatores deve ser 100%.");
            }
            else
            {
                foreach (var item in body.Fields)
                {

                    if (item.OilFR is not null)
                        throw new ConflictException("Óleo: Fator de rateio não se aplica.");
                }
            }

            if (body.isApplicableFRGas is true)
            {
                decimal? sumGas = 0;
                foreach (var item in body.Fields)
                {
                    if (item.GasFR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    sumGas += item.GasFR;
                }
                if (sumGas != 1)
                    throw new ConflictException("Gás: Soma dos fatores deve ser 100%.");
            }
            else
            {
                foreach (var item in body.Fields)
                {
                    if (item.GasFR is not null)
                        throw new ConflictException("Gás: Fator de rateio não se aplica.");
                }
            }

            if (body.isApplicableFRWater is true)
            {
                decimal? sumWater = 0;
                foreach (var item in body.Fields)
                {
                    if (item.WaterFR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    sumWater += item.WaterFR;
                }
                if (sumWater != 1)
                    throw new ConflictException("Água: Soma dos fatores deve ser 100%.");
            }
            else
            {
                foreach (var item in body.Fields)
                {
                    if (item.WaterFR is not null)
                        throw new ConflictException("Água: Fator de rateio não se aplica.");
                }
            }

            foreach (var installationUEP in installationsWithFields)
            {
                foreach (var field in installationUEP.Fields)
                {
                    foreach (var fr in field.FRs)
                    {
                        fr.IsActive = false;
                    }
                }
            }

            foreach (var item in body.Fields)
            {
                var field = await _fieldRepository.GetByIdAsync(item.FieldId);
                var createOilFr = new FieldFR
                {
                    Id = Guid.NewGuid(),
                    Field = field,
                    FROil = item.OilFR,
                    FRGas = item.GasFR,
                    FRWater = item.WaterFR,
                    IsActive = true,
                };
                await _installationRepository.AddFRAsync(createOilFr);
            }
            await _installationRepository.SaveChangesAsync();

            var frs = await _installationRepository.GetFRsByUEPAsync(installation.UepCod);

            var frsDTO = _mapper.Map<List<FieldFR>, List<FRFieldDTO>>(frs);

            return frsDTO;
        }

    }
}
