using AutoMapper;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Services
{
    public class FieldFRService
    {
        private readonly IInstallationRepository _installationRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;

        public FieldFRService(IMapper mapper, IInstallationRepository installationRepository, IFieldRepository fieldRepository, IProductionRepository productionRepository)
        {
            _mapper = mapper;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _productionRepository = productionRepository;
        }

        public async Task ApplyFR(FieldFRBodyService body, DateTime dateProduction)
        {
            var installation = await _installationRepository.GetByIdAsync(body.InstallationId);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsProcessingUnit == false)
                throw new ConflictException("Instalação não é uma unidade de processamento.");

            //var installationsWithFields = await _installationRepository.GetByIdWithFieldsCod(body.InstallationId);

            if (body.Gas is not null)
            {
                foreach (var field in body.Gas.FR.Fields)
                {
                    var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                    if (fieldInDatabase is null)
                        throw new NotFoundException(ErrorMessages.NotFound<Field>());

                    //if (fieldInDatabase.Installation.Id != body.InstallationId)
                    //    throw new BadRequestException("Poço não pertence a instalação.");

                }

                var sumGas = 0m;

                foreach (var field in body.Gas.FR.Fields)
                {
                    if (body.Gas.FR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    if (body.Gas.FR.IsApplicable)
                        sumGas += field.FluidFr;
                }

                if (sumGas != 1 && body.Gas.FR.IsApplicable)
                    throw new ConflictException("Gás: Soma dos fatores de rateio deve ser 1.");

                if (/*body.BothGas &&*/ body.Gas.FR.IsApplicable)
                {
                    foreach (var field in body.Gas.FR.Fields)
                    {
                        var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                        if (fieldInDatabase is null)
                            throw new NotFoundException(ErrorMessages.NotFound<Field>());

                        if (body.Production is not null)
                        {
                            var existingFr = await _installationRepository.GetFrByDateMeasuredAndFieldId(body.Production.MeasuredAt, field.FieldId);

                            if (existingFr is null)
                            {
                                var fr = new FieldFR
                                {
                                    Id = Guid.NewGuid(),
                                    DailyProduction = body.Production,
                                    Field = fieldInDatabase,
                                    FRGas = field.FluidFr,
                                    ProductionInField = ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr,
                                };

                                await _installationRepository.AddFRAsync(fr);
                            }

                            if (existingFr is not null && existingFr.FRGas is null)
                            {
                                existingFr.ProductionInField += ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr;
                                existingFr.FRGas = field.FluidFr;


                                _installationRepository.UpdateFr(existingFr);
                            }
                        }
                    }


                }

            }

            if (body.Oil is not null)
            {
                foreach (var field in body.Oil.FR.Fields)
                {
                    var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                    if (fieldInDatabase is null)
                        throw new NotFoundException(ErrorMessages.NotFound<Field>());

                    //if (fieldInDatabase.Installation.Id != body.InstallationId)
                    //    throw new BadRequestException("Poço não pertence a instalação.");
                }

                var sumOil = 0m;

                foreach (var field in body.Oil.FR.Fields)
                {
                    if (body.Oil.FR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    if (body.Oil.FR.IsApplicable)
                        sumOil += field.FluidFr;
                }

                if (sumOil != 1 && body.Oil.FR.IsApplicable)
                    throw new ConflictException("Óleo: Soma dos fatores de rateio deve ser 1.");

                if (body.Oil.FR.IsApplicable)
                    foreach (var field in body.Oil.FR.Fields)
                    {
                        if (body.Production is not null)
                        {
                            var existingFr = await _installationRepository.GetFrByDateMeasuredAndFieldId(body.Production.MeasuredAt, field.FieldId);

                            var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                            if (fieldInDatabase is null)
                                throw new NotFoundException(ErrorMessages.NotFound<Field>());

                            if (existingFr is null)
                            {
                                var fr = new FieldFR
                                {
                                    Id = Guid.NewGuid(),
                                    DailyProduction = body.Production,
                                    Field = fieldInDatabase,
                                    FROil = field.FluidFr,
                                    ProductionInField = body.Oil.TotalOilProductionM3 * field.FluidFr /*+ (body.Water is not null ? body.Water.TotalWaterM3 : 0)*/,

                                };

                                await _installationRepository.AddFRAsync(fr);
                            }
                            else
                            {

                                existingFr.ProductionInField += body.Oil.TotalOilProductionM3 * field.FluidFr /*+ (body.Water is not null ? body.Water.TotalWaterM3 : 0)*/;
                                existingFr.FROil = field.FluidFr;

                                _installationRepository.UpdateFr(existingFr);
                            }
                        }
                    }


            }
        }
    }
}
