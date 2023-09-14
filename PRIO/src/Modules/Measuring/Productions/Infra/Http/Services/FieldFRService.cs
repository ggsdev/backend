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


            if (body.Gas is not null)
            {
                if (body.Production.Oil is not null && body.Production.FieldsFR is not null && body.Production.FieldsFR.Any() is false && body.Gas.FR.IsApplicable)
                    throw new BadRequestException("Óleo não foi rateado, logo não é possível ratear o gás.", status: "GÁS");

                if (body.Production.Oil is not null && body.Production.FieldsFR is not null && body.Production.FieldsFR.Any() && body.Gas.FR.IsApplicable is false)
                    throw new BadRequestException("Óleo foi rateado, logo é necessário ratear o gás.", status: "GÁS");

                foreach (var field in body.Gas.FR.Fields)
                {
                    var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                    if (fieldInDatabase is null)
                        throw new NotFoundException(ErrorMessages.NotFound<Field>());

                    if (fieldInDatabase is not null && fieldInDatabase.IsActive is false)
                        throw new ConflictException(ErrorMessages.Inactive<Field>());

                }

                var sumGas = 0m;

                foreach (var field in body.Gas.FR.Fields)
                {
                    if (body.Gas.FR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(field.FluidFr)[3])[2];

                    if (decimalPlaces > 4)
                        throw new BadRequestException("Fator de rateio do óleo pode ter no máximo duas casas decimais.");

                    if (body.Gas.FR.IsApplicable)
                        sumGas += field.FluidFr;
                }

                if (sumGas != 1 && body.Gas.FR.IsApplicable)
                    throw new ConflictException("Gás: Soma dos fatores de rateio deve ser 100%.");


                if (body.Gas.FR.IsApplicable)
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
                                    TotalProductionInField = ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr,
                                    GasProductionInField = ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr,
                                };

                                fr.ProductionInFieldAsPercentage = fr.TotalProductionInField / body.Production.TotalProduction;

                                await _installationRepository.AddFRAsync(fr);
                            }

                            if (existingFr is not null && existingFr.FRGas is null)
                            {
                                existingFr.TotalProductionInField += ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr;
                                existingFr.FRGas = field.FluidFr;
                                existingFr.GasProductionInField = ((body.Production.GasLinear is not null ? body.Production.GasLinear.TotalGas : 0) + (body.Production.GasDiferencial is not null ? body.Production.GasDiferencial.TotalGas : 0)) * field.FluidFr;

                                existingFr.ProductionInFieldAsPercentage = existingFr.TotalProductionInField / body.Production.TotalProduction;

                                _installationRepository.UpdateFr(existingFr);
                            }
                        }
                    }


                }

            }

            if (body.Oil is not null)
            {
                if (body.Production.Gas is not null && body.Production.FieldsFR is not null && body.Production.FieldsFR.Any() is false && body.Oil.FR.IsApplicable)
                    throw new BadRequestException("Gás não foi rateado, logo não é possível ratear o óleo.", status: "ÓLEO");

                if (body.Production.Gas is not null && body.Production.FieldsFR is not null && body.Production.FieldsFR.Any() && body.Oil.FR.IsApplicable is false)
                    throw new BadRequestException("Gás foi rateado, logo é necessário ratear o óleo.", status: "ÓLEO");

                foreach (var field in body.Oil.FR.Fields)
                {
                    var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                    if (fieldInDatabase is null)
                        throw new NotFoundException(ErrorMessages.NotFound<Field>());

                    if (fieldInDatabase is not null && fieldInDatabase.IsActive is false)
                        throw new ConflictException(ErrorMessages.Inactive<Field>());
                    //if (fieldInDatabase.Installation.Id != body.InstallationId)
                    //    throw new BadRequestException("Poço não pertence a instalação.");
                }

                var sumOil = 0m;

                foreach (var field in body.Oil.FR.Fields)
                {
                    if (body.Oil.FR is null)
                        throw new ConflictException("Fator de rateio do campo não encontrado.");

                    var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(field.FluidFr)[3])[2];

                    if (decimalPlaces > 4)
                        throw new BadRequestException("Fator de rateio do óleo pode ter no máximo duas casas decimais.");

                    if (body.Oil.FR.IsApplicable)
                        sumOil += field.FluidFr;
                }

                if (sumOil != 1 && body.Oil.FR.IsApplicable)
                    throw new ConflictException("Óleo: Soma dos fatores de rateio deve ser 100%.");

                if (body.Oil.FR.IsApplicable)
                    foreach (var field in body.Oil.FR.Fields)
                    {
                        if (body.Production is not null)
                        {
                            var existingFr = await _installationRepository.GetFrByDateMeasuredAndFieldId(body.Production.MeasuredAt, field.FieldId);

                            var fieldInDatabase = await _fieldRepository.GetByIdAsync(field.FieldId);

                            if (fieldInDatabase is null)
                                throw new NotFoundException(ErrorMessages.NotFound<Field>());

                            if (fieldInDatabase is not null && fieldInDatabase.IsActive is false)
                                throw new ConflictException(ErrorMessages.Inactive<Field>());

                            if (existingFr is null)
                            {
                                var fr = new FieldFR
                                {
                                    Id = Guid.NewGuid(),
                                    DailyProduction = body.Production,
                                    Field = fieldInDatabase,
                                    FROil = field.FluidFr,
                                    TotalProductionInField = body.Oil.TotalOilProductionM3 * field.FluidFr, /*+ (body.Water is not null ? body.Water.TotalWaterM3 : 0)*/
                                    OilProductionInField = body.Oil.TotalOilProductionM3 * field.FluidFr
                                };

                                fr.ProductionInFieldAsPercentage = fr.TotalProductionInField / body.Production.TotalProduction;

                                await _installationRepository.AddFRAsync(fr);
                            }
                            else
                            {

                                existingFr.TotalProductionInField += body.Oil.TotalOilProductionM3 * field.FluidFr /*+ (body.Water is not null ? body.Water.TotalWaterM3 : 0)*/;
                                existingFr.FROil = field.FluidFr;

                                existingFr.OilProductionInField = body.Oil.TotalOilProductionM3 * field.FluidFr;

                                existingFr.ProductionInFieldAsPercentage = existingFr.TotalProductionInField / body.Production.TotalProduction;

                                _installationRepository.UpdateFr(existingFr);
                            }
                        }
                    }
            }
        }
    }
}
