using AutoMapper;
using PRIO.src.Modules.FileImport.XML.Measuring.Dtos;
using PRIO.src.Modules.FileImport.XML.Measuring.Infra.Utils;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Modules.Measuring.Productions.ViewModels;
using PRIO.src.Modules.Measuring.WellProductions.Dtos;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Services
{
    public class ProductionService
    {
        private readonly IProductionRepository _repository;
        private readonly IWellRepository _wellRepository;
        private readonly IGasVolumeCalculationRepository _gasRepository;
        private readonly IOilVolumeCalculationRepository _oilRepository;
        private readonly IMeasurementHistoryRepository _fileHistoryRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public ProductionService(IProductionRepository productionRepository, IMapper mapper, IGasVolumeCalculationRepository gasVolumeCalculationRepository, IInstallationRepository installationRepository, IOilVolumeCalculationRepository oilVolumeCalculationRepository, IMeasurementHistoryRepository measurementHistoryRepository, IFieldRepository fieldRepository, IWellRepository wellRepository)
        {
            _repository = productionRepository;
            _mapper = mapper;
            _gasRepository = gasVolumeCalculationRepository;
            _installationRepository = installationRepository;
            _oilRepository = oilVolumeCalculationRepository;
            _fileHistoryRepository = measurementHistoryRepository;
            _fieldRepository = fieldRepository;
            _wellRepository = wellRepository;
        }

        public async Task<ProductionDtoWithNullableDecimals> GetById(Guid id)
        {
            var production = await _repository
                .GetById(id);

            if (production is null)
                throw new NotFoundException($"Produção não encontrada.");

            var dailyProduction = new DailyProduction
            {
                StatusProduction = production.StatusProduction,
                TotalGasSCF = Math.Round(
                    (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0) +
                    (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0),
                    5),
                TotalGasM3 = Math.Round(
                    (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) +
                    (production.GasLinear is not null ? production.GasLinear.TotalGas : 0),
                    5),
                TotalOilBBL = Math.Round(
                    production.Oil is not null ? production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier : 0,
                    5),
                TotalOilM3 = Math.Round(
                    production.Oil is not null ? production.Oil.TotalOil : 0,
                    5),
                StatusGas = (production.GasDiferencial is not null ? production.GasDiferencial.StatusGas : false) || (production.GasLinear is not null ? production.GasLinear.StatusGas : false),
                StatusOil = production.Oil is not null ? production.Oil.StatusOil : false,
                IsActive = production.IsActive
            };

            var gasBurnt = new GasBurntDto
            {
                TotalGasBurnt = (production.GasLinear is not null ? production.GasLinear.BurntGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.BurntGas : 0),
            };

            var gasFuel = new GasFuelDto
            {
                TotalGasFuel = (production.GasLinear is not null ? production.GasLinear.FuelGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.FuelGas : 0),
            };

            var gasImported = new GasImportedDto
            {
                TotalGasImported = (production.GasLinear is not null ? production.GasLinear.ImportedGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.ImportedGas : 0),
            };

            var gasExported = new GasExportedDto
            {
                TotalGasExported = (production.GasLinear is not null ? production.GasLinear.ExportedGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.ExportedGas : 0),
            };

            var files = new List<MeasurementHistoryWithMeasurementsDto>();
            var oilPoints = new List<LocalOilPointDto>();
            var burnetGasPoints = new List<LocalGasPointDto>();
            var fuelGasPoints = new List<LocalGasPointDto>();
            var importedGasPoints = new List<LocalGasPointDto>();
            var exportedGasPoints = new List<LocalGasPointDto>();

            foreach (var measurement in production.Measurements)
            {
                var historyDto = _mapper.Map<MeasurementHistory, MeasurementHistoryWithMeasurementsDto>(measurement.MeasurementHistory);

                if (!files.Any(file => file.ImportId == historyDto.ImportId))
                {
                    files.Add(historyDto);
                }

                var measurementDateOil = measurement.DHA_INICIO_PERIODO_MEDICAO_001 is not null ? measurement.DHA_INICIO_PERIODO_MEDICAO_001.Value.ToString("dd/MM/yyyy") : string.Empty;
                var measurementDateGasLinear = measurement.DHA_INICIO_PERIODO_MEDICAO_002 is not null ? measurement.DHA_INICIO_PERIODO_MEDICAO_002.Value.ToString("dd/MM/yyyy") : string.Empty;
                var measurementDateGasDiferencial = measurement.DHA_INICIO_PERIODO_MEDICAO_003 is not null ? measurement.DHA_INICIO_PERIODO_MEDICAO_003.Value.ToString("dd/MM/yyyy") : string.Empty;

                if (measurement.DHA_INICIO_PERIODO_MEDICAO_002 is not null && measurement.COD_INSTALACAO_002 is not null)
                {
                    var gasCalculus = await _gasRepository
                        .GetGasVolumeCalculationByInstallationUEP(measurement.COD_INSTALACAO_002);

                    if (gasCalculus is null)
                        throw new NotFoundException($"Instalação não encontrada código: {measurement.COD_INSTALACAO_002}");

                    foreach (var assistanceGas in gasCalculus.AssistanceGases)
                    {

                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == assistanceGas.MeasuringPoint.TagPointMeasuring;
                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var hpFlare in gasCalculus.HPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == hpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!(burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring)) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var lpFlare in gasCalculus.LPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var pilot in gasCalculus.PilotGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == pilot.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = pilot.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var purge in gasCalculus.PurgeGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == purge.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = purge.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var lowPressure in gasCalculus.LowPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lowPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            };


                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var highPressure in gasCalculus.HighPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == highPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            };

                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var import in gasCalculus.ImportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == import.MeasuringPoint.TagPointMeasuring;

                        if (!importedGasPoints.Any(measuring => measuring.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                            };

                            importedGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var export in gasCalculus.ExportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == export.MeasuringPoint.TagPointMeasuring;

                        if (!exportedGasPoints.Any(measuring => measuring.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_002 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_002.Value, 5),
                                LocalPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                            };


                            exportedGasPoints.Add(measuringPoint);

                        }
                    }
                }

                if (measurement.DHA_INICIO_PERIODO_MEDICAO_003 is not null && measurement.COD_INSTALACAO_003 is not null)
                {
                    var gasCalculus = await _gasRepository
                         .GetGasVolumeCalculationByInstallationUEP(measurement.COD_INSTALACAO_003);

                    if (gasCalculus is null)
                        throw new NotFoundException($"Instalação não encontrada código: {measurement.COD_INSTALACAO_003}");

                    foreach (var assistanceGas in gasCalculus.AssistanceGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == assistanceGas.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var hpFlare in gasCalculus.HPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == hpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var lpFlare in gasCalculus.LPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var pilot in gasCalculus.PilotGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == pilot.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = pilot.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var purge in gasCalculus.PurgeGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == purge.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = purge.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var lowPressure in gasCalculus.LowPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lowPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            };
                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var highPressure in gasCalculus.HighPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == highPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            };

                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var import in gasCalculus.ImportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == import.MeasuringPoint.TagPointMeasuring;

                        if (!importedGasPoints.Any(measuring => measuring.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                            };

                            importedGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var export in gasCalculus.ExportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == export.MeasuringPoint.TagPointMeasuring;

                        if (!exportedGasPoints.Any(measuring => measuring.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.MED_CORRIGIDO_MVMDO_003 is not null)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = Math.Round(measurement.MED_CORRIGIDO_MVMDO_003.Value, 5),
                                LocalPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                            };

                            exportedGasPoints.Add(measuringPoint);
                        }
                    }


                }

                if (measurement.DHA_INICIO_PERIODO_MEDICAO_001 is not null && measurement.COD_INSTALACAO_001 is not null)
                {
                    var installation = await _installationRepository
                      .GetByUEPCod(measurement.COD_INSTALACAO_001);

                    if (installation is null)
                        throw new NotFoundException($"Instalação com código UEP: {measurement.COD_INSTALACAO_001} não encontrado");

                    var oilCalculus = await _oilRepository
                        .GetOilVolumeCalculationByInstallationUEP(measurement.COD_INSTALACAO_001);

                    foreach (var section in oilCalculus.Sections)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == section.MeasuringPoint.TagPointMeasuring;
                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.VolumeAfterManualBsw_001 is not null)
                        {

                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = Math.Round(measurement.VolumeAfterManualBsw_001.Value, 5)/*MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurement.BswManual_001)*/,
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = section.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,

                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var dor in oilCalculus.DORs)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == dor.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.VolumeAfterManualBsw_001 is not null)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = Math.Round(measurement.VolumeAfterManualBsw_001.Value, 5),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = dor.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var drain in oilCalculus.DrainVolumes)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == drain.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.VolumeAfterManualBsw_001 is not null)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = Math.Round(measurement.VolumeAfterManualBsw_001.Value, 5),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = drain.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var tog in oilCalculus.TOGRecoveredOils)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == tog.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring) && measurementFound && measurement.VolumeAfterManualBsw_001 is not null)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = Math.Round(measurement.VolumeAfterManualBsw_001.Value, 5),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = tog.MeasuringPoint.DinamicLocalMeasuringPoint,
                                TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }
                }

            }

            var oilDto = new OilConsultingDtoFrsNull
            {
                TotalOilProduction = production.Oil is not null ? production.Oil.TotalOil : 0,
                MeasuringPoints = oilPoints
            };

            gasBurnt.MeasuringPoints = burnetGasPoints;
            gasFuel.MeasuringPoints = fuelGasPoints;
            gasImported.MeasuringPoints = importedGasPoints;
            gasExported.MeasuringPoints = exportedGasPoints;
            oilDto.MeasuringPoints = oilPoints;


            var gasDto = new GasConsultingDtoFrsNull
            {
                TotalGasProduction = (production.GasLinear is not null ? production.GasLinear.TotalGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0),
                DetailedBurnedGas = production.Gas is not null ? new DetailBurn
                {
                    EmergencialBurn = production.Gas.EmergencialBurn,
                    ForCommissioningBurn = production.Gas.ForCommissioningBurn,
                    LimitOperacionalBurn = production.Gas.LimitOperacionalBurn,
                    OthersBurn = production.Gas.OthersBurn,
                    ScheduledStopBurn = production.Gas.ScheduledStopBurn,
                    VentedGas = production.Gas.VentedGas,
                    WellTestBurn = production.Gas.WellTestBurn

                } : new DetailBurn { },

                GasBurnt = gasBurnt,
                GasFuel = gasFuel,
                GasExported = gasExported,
                GasImported = gasImported

            };

            var waterDto = new WaterDto
            {
                Id = production.Water is not null ? production.Water.Id : null,
                TotalWaterM3 = Math.Round(production.Water is not null ? production.Water.TotalWater : 0, 5),
                TotalWaterBBL = Math.Round(production.Water is not null ? production.Water.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier : 0, 5),
            };

            var oilFrs = new FRViewModelNull
            {
                Fields = new(),
            };
            var gasFrs = new FRViewModelNull
            {
                Fields = new(),

            };

            if (production.FieldsFR is not null)
            {
                foreach (var fr in production.FieldsFR)
                {
                    if (fr.FROil is not null)
                    {
                        var createdFr = new FRFieldsViewModelNull
                        {
                            FieldId = fr.Field.Id,
                            FieldName = fr.Field.Name,
                            FluidFr = fr.FROil,
                            ProductionInField = fr.OilProductionInField

                        };

                        oilFrs.Fields.Add(createdFr);

                    }
                }
                foreach (var fr in production.FieldsFR)
                {
                    if (fr.FRGas is not null)
                    {
                        var createdFr = new FRFieldsViewModelNull
                        {
                            FieldId = fr.Field.Id,
                            FieldName = fr.Field.Name,
                            FluidFr = fr.FRGas,
                            ProductionInField = fr.GasProductionInField
                        };

                        gasFrs.Fields.Add(createdFr);
                    }
                }

            }

            gasDto.FR = gasFrs;
            oilDto.FR = oilFrs;

            var commentDto = _mapper.Map<CreateUpdateCommentDto>(production.Comment);

            var appropriateDto = new AppropriationDto
            {
                FieldProductions = new(),
                WaterProduction = waterDto,
                ProductionId = production.Id
            };

            var fieldProductions = await _repository.GetAllFieldProductionByProduction(production.Id);

            foreach (var fieldP in fieldProductions)
            {
                var field = await _fieldRepository.GetByIdAsync(fieldP.FieldId);

                var fieldPDto = new FieldProductionDto
                {
                    FieldName = field.Name,
                    FieldProductionId = fieldP.Id,
                    GasProductionInFieldM3 = Math.Round(fieldP.GasProductionInField, 5),
                    GasProductionInFieldSCF = Math.Round(fieldP.GasProductionInField * ProductionUtils.m3ToSCFConversionMultipler, 5),
                    OilProductionInFieldM3 = Math.Round(fieldP.OilProductionInField, 5),
                    OilProductionInFieldBBL = Math.Round(fieldP.OilProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                    WaterProductionInFieldBBL = Math.Round(fieldP.WaterProductionInField * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                    WaterProductionInFieldM3 = Math.Round(fieldP.WaterProductionInField, 5),
                    WellAppropriations = new()
                };

                appropriateDto.FieldProductions.Add(fieldPDto);

                foreach (var wellP in fieldP.WellProductions)
                {
                    var well = await _wellRepository
                        .GetByIdAsync(wellP.WellId);
                    if (well is not null)
                        fieldPDto.WellAppropriations.Add(new WellProductionDto
                        {
                            WellName = well.Name is not null ? well.Name : string.Empty,
                            Downtime = wellP.Downtime,
                            ProductionGasInWellM3 = Math.Round(wellP.ProductionGasInWellM3, 5),
                            ProductionGasInWellSCF = Math.Round(wellP.ProductionGasInWellM3 * ProductionUtils.m3ToSCFConversionMultipler, 5),

                            ProductionOilInWellM3 = Math.Round(wellP.ProductionOilInWellM3, 5),
                            ProductionOilInWellBBL = Math.Round(wellP.ProductionOilInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier, 5),

                            ProductionWaterInWellM3 = Math.Round(wellP.ProductionWaterInWellM3, 5),

                            ProductionWaterInWellBBL = Math.Round(wellP.ProductionWaterInWellM3 * ProductionUtils.m3ToBBLConversionMultiplier, 5),

                            WellProductionId = wellP.Id,

                            EfficienceLoss = Math.Round(wellP.EfficienceLoss, 5),
                            ProductionLostOilM3 = Math.Round(wellP.ProductionLostOil, 5),
                            ProportionalDay = Math.Round(wellP.ProportionalDay, 5),

                            ProductionLostGasM3 = Math.Round(wellP.ProductionLostGas, 5),

                            ProductionLostWaterM3 = Math.Round(wellP.ProductionLostWater, 5),

                            ProductionLostGasSCF = Math.Round(wellP.ProductionLostGas * ProductionUtils.m3ToSCFConversionMultipler, 5),
                            ProductionLostOilBBL = Math.Round(wellP.ProductionLostOil * ProductionUtils.m3ToBBLConversionMultiplier, 5),
                            ProductionLostWaterBBL = Math.Round(wellP.ProductionLostWater * ProductionUtils.m3ToBBLConversionMultiplier, 5),

                        });

                }

                var orderedWellAppropriationsDto = fieldPDto.WellAppropriations
                           .OrderBy(x => x.WellName)
                           .ToList();

                fieldPDto.WellAppropriations = orderedWellAppropriationsDto;

                var fieldLossOil = fieldPDto.WellAppropriations.Sum(x => x.ProductionLostOilM3);
                var fieldLossGas = fieldPDto.WellAppropriations.Sum(x => x.ProductionLostGasM3);
                var fieldLossWater = fieldPDto.WellAppropriations.Sum(x => x.ProductionLostWaterM3);

                fieldPDto.WaterLossInFieldM3 = Math.Round(fieldLossWater, 5);
                fieldPDto.GasLossInFieldM3 = Math.Round(fieldLossGas, 5);
                fieldPDto.OilLossInFieldM3 = Math.Round(fieldLossOil, 5);

                fieldPDto.WaterLossInFieldBBL = Math.Round(fieldLossWater * ProductionUtils.m3ToBBLConversionMultiplier, 5);
                fieldPDto.GasLossInFieldSCF = Math.Round(fieldLossGas * ProductionUtils.m3ToSCFConversionMultipler, 5);
                fieldPDto.OilLossInFieldBBL = Math.Round(fieldLossOil * ProductionUtils.m3ToBBLConversionMultiplier, 5);
            }

            var productionDto = new ProductionDtoWithNullableDecimals
            {
                ProductionId = production.Id,
                DateProduction = production.MeasuredAt.ToString("dd/MM/yyyy"),
                InstallationName = production.Installation.Name,
                UepName = production.Installation.UepName,
                DailyProduction = dailyProduction,
                Gas = gasDto,
                Oil = oilDto,
                Files = files,
                Comment = commentDto,
                Water = waterDto,
                WellAppropriation = appropriateDto,
                IsCalculated = production.IsCalculated,
                CanDetailGasBurned = production.CanDetailGasBurned

            };

            var gasCalculationByUepCode = await _gasRepository
               .GetGasVolumeCalculationByInstallationUEP(production.Installation.UepCod);

            var oilCalculationByUepCode = await _oilRepository
                  .GetOilVolumeCalculationByInstallationUEP(production.Installation.UepCod);

            foreach (var file in productionDto.Files)
            {
                if (gasCalculationByUepCode is not null && file.FileType == XmlUtils.File002)
                {
                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };

                                    file.Summary.Add(summary);

                                }
                            }
                        }

                        //if (containAssistanceGas is false && assistanceGas.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var export in gasCalculationByUepCode.ExportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (export.IsApplicable && export.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }
                        //if (containExportGas is false && export.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (highPressure.IsApplicable && highPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                        //if (containHighPressureGas is false && highPressure.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }
                        //if (containHPFlare is false && hpFlare.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var import in gasCalculationByUepCode.ImportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (import.IsApplicable && import.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                        //if (containImportGas is false && import.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                        //if (containLowPressureGas is false && lowPressure.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                        //if (containLPFlare is false && lpFlare.IsApplicable && pointAlreadyInserted is false)
                        //{
                        //    var measurementWrong = new SummaryGeneric
                        //    {
                        //        StatusMeasuringPoint = false,
                        //        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        //        LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                        //        TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                        //        Volume = 0
                        //    };

                        //    file.Summary.Add(measurementWrong);
                        //}
                    }

                    foreach (var pilot in gasCalculationByUepCode.PilotGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (pilot.IsApplicable && pilot.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = pilot.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }

                    foreach (var purge in gasCalculationByUepCode.PurgeGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (purge.IsApplicable && purge.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_002 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = purge.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_002.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }
                }

                if (gasCalculationByUepCode is not null && file.FileType == XmlUtils.File003)
                {
                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };

                                    file.Summary.Add(summary);

                                }
                            }
                        }
                    }

                    foreach (var export in gasCalculationByUepCode.ExportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (export.IsApplicable && export.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }
                    }

                    foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (highPressure.IsApplicable && highPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }

                    foreach (var import in gasCalculationByUepCode.ImportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (import.IsApplicable && import.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                    }

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }
                    }

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                    }

                    foreach (var pilot in gasCalculationByUepCode.PilotGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (pilot.IsApplicable && pilot.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = pilot.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }

                    foreach (var purge in gasCalculationByUepCode.PurgeGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (purge.IsApplicable && purge.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false && measurementResponse.MED_CORRIGIDO_MVMDO_003 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = purge.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_CORRIGIDO_MVMDO_003.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }
                    }

                }

                if (oilCalculationByUepCode is not null && file.FileType == XmlUtils.File001)
                {

                    foreach (var section in oilCalculationByUepCode.Sections)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (section.IsApplicable && section.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false && measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = section.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }

                    }


                    foreach (var dor in oilCalculationByUepCode.DORs)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (dor.IsApplicable && dor.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false && measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = dor.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001.Value, 5),

                                    };
                                    file.Summary.Add(summary);

                                }
                            }
                        }


                    }


                    foreach (var drain in oilCalculationByUepCode.DrainVolumes)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (drain.IsApplicable && drain.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false && measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = drain.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001.Value, 5),

                                    };

                                    file.Summary.Add(summary);
                                }
                            }
                        }
                    }


                    foreach (var tog in oilCalculationByUepCode.TOGRecoveredOils)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];
                            if (measurementResponse.MeasurementHistory.Id == file.ImportId)
                            {
                                if (tog.IsApplicable && tog.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false && measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001 is not null)
                                {
                                    var summary = new SummaryGeneric
                                    {
                                        DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                        LocationMeasuringPoint = tog.MeasuringPoint.DinamicLocalMeasuringPoint,
                                        StatusMeasuringPoint = true,
                                        TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                                        Volume = Math.Round(measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001.Value, 5),

                                    };
                                    file.Summary.Add(summary);
                                }
                            }
                        }

                    }
                }
            }

            if (oilCalculationByUepCode is not null)
            {
                foreach (var section in oilCalculationByUepCode.Sections)
                {
                    var containsSection = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring && section.IsApplicable))
                        {
                            containsSection = true;
                        }
                    }

                    if (containsSection is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = section.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"

                        });
                    }
                }

                foreach (var dor in oilCalculationByUepCode.DORs)
                {
                    var containsDor = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring && dor.IsApplicable))
                        {
                            containsDor = true;
                        }
                    }

                    if (containsDor is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = dor.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }

                foreach (var tog in oilCalculationByUepCode.TOGRecoveredOils)
                {
                    var containsTog = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring && tog.IsApplicable))
                        {
                            containsTog = true;
                        }
                    }

                    if (containsTog is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = tog.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"

                        });
                    }
                }

                foreach (var drain in oilCalculationByUepCode.DrainVolumes)
                {
                    var containsDrain = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring && drain.IsApplicable))
                        {
                            containsDrain = true;
                        }
                    }

                    if (containsDrain is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = drain.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                            Volume = 0,
                            Fluid = "Oil"
                        });
                    }
                }
            }

            if (gasCalculationByUepCode is not null)
            {
                foreach (var assistance in gasCalculationByUepCode.AssistanceGases)
                {
                    var containsAssistanceGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == assistance.MeasuringPoint.TagPointMeasuring && assistance.IsApplicable))
                        {
                            containsAssistanceGases = true;
                        }
                    }

                    if (containsAssistanceGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == assistance.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = assistance.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = assistance.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var export in gasCalculationByUepCode.ExportGases)
                {
                    var containsExportGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring && export.IsApplicable))
                        {
                            containsExportGases = true;
                        }
                    }

                    if (containsExportGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = export.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var import in gasCalculationByUepCode.ImportGases)
                {
                    var containsImportGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring && import.IsApplicable))
                        {
                            containsImportGases = true;
                        }
                    }

                    if (containsImportGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = import.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                {
                    var containsHPFlares = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring && hpFlare.IsApplicable))
                        {
                            containsHPFlares = true;
                        }
                    }

                    if (containsHPFlares is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                {
                    var containsLPFlares = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring && lpFlare.IsApplicable))
                        {
                            containsLPFlares = true;
                        }
                    }

                    if (containsLPFlares is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var purge in gasCalculationByUepCode.PurgeGases)
                {
                    var containsPurgeGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring && purge.IsApplicable))
                        {
                            containsPurgeGases = true;
                        }
                    }

                    if (containsPurgeGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = purge.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var pilot in gasCalculationByUepCode.PilotGases)
                {
                    var containsPilotGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring && pilot.IsApplicable))
                        {
                            containsPilotGases = true;
                        }
                    }

                    if (containsPilotGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = pilot.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                {
                    var containsHighPressureGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring && highPressure.IsApplicable))
                        {
                            containsHighPressureGases = true;
                        }
                    }

                    if (containsHighPressureGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = highPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }

                foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                {
                    var LowPressuresistanceGases = false;

                    foreach (var file in productionDto.Files)
                    {
                        if (file.Summary.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring && lowPressure.IsApplicable))
                        {
                            LowPressuresistanceGases = true;
                        }
                    }

                    if (LowPressuresistanceGases is false && productionDto.Files.Any() && !productionDto.MeasurementsNotFound.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring))
                    {
                        productionDto.MeasurementsNotFound.Add(new SummaryProduction
                        {
                            Status = false,
                            Date = production.MeasuredAt.ToString("dd/MM/yyyy"),
                            LocationMeasuringPoint = lowPressure.MeasuringPoint.DinamicLocalMeasuringPoint,
                            TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            Volume = 0
                        });
                    }
                }
            }

            return productionDto;
        }

        public async Task<List<GetAllProductionsDto>> GetAllProductions()
        {
            var productions = await _repository
                .GetAllProductions();

            var productionsDto = new List<GetAllProductionsDto>();
            foreach (var production in productions)
            {
                var files = await _fileHistoryRepository
                    .GetAllFilesByDate(production.MeasuredAt);

                var filesDto = new List<ProductionFilesDto>();

                foreach (var file in files)
                {
                    var fileDto = new ProductionFilesDto
                    {
                        FileName = file.FileName,
                        FileType = file.FileType,
                        ImportedAt = file.ImportedAt,
                        FileId = file.Id
                    };

                    filesDto.Add(fileDto);
                }

                if (production.IsActive)
                {
                    var productionDto = new GetAllProductionsDto
                    {
                        Id = production.Id,
                        IsCalculated = production.IsCalculated,
                        DateProduction = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        Gas = new GasTotalDto
                        {
                            TotalGasSCF = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0), 5),
                            TotalGasM3 = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0), 5),
                        },
                        Oil = new OilTotalDto
                        {
                            TotalOilBBL = production.Oil is not null ? Math.Round(production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier, 5) : 0,
                            TotalOilM3 = production.Oil is not null ? Math.Round(production.Oil.TotalOil, 5) : 0,
                        },
                        Status = production.StatusProduction,
                        UepName = production.Installation.UepName,
                        Files = filesDto,
                        IsActive = production.IsActive,
                        Water = new WaterTotalDto
                        {
                            TotalWaterM3 = Math.Round(production.Water is not null ? production.Water.TotalWater : 0, 5),
                            TotalWaterBBL = Math.Round(production.Water is not null ? production.Water.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier : 0, 5)
                        },
                        CanDetailGasBurned = production.CanDetailGasBurned,

                    };

                    productionsDto.Add(productionDto);
                }
            }

            return productionsDto;
        }

        public async Task<List<GetAllProductionsDto>> GetDeletedProductions()
        {
            var productions = await _repository
                .GetAllProductions();

            var productionsDto = new List<GetAllProductionsDto>();

            foreach (var production in productions)
            {
                var files = await _fileHistoryRepository
                    .GetAllFilesByDate(production.MeasuredAt);

                var filesDto = new List<ProductionFilesDto>();

                foreach (var file in files)
                {
                    var fileDto = new ProductionFilesDto
                    {
                        FileName = file.FileName,
                        FileType = file.FileType,
                        ImportedAt = file.ImportedAt,
                        FileId = file.Id
                    };

                    filesDto.Add(fileDto);
                }

                if (production.IsActive is false)
                {
                    var productionDto = new GetAllProductionsDto
                    {
                        Id = production.Id,
                        DateProduction = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        Gas = new GasTotalDto
                        {
                            TotalGasSCF = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToSCFConversionMultipler : 0), 5),
                            TotalGasM3 = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0), 5),
                        },
                        Oil = new OilTotalDto
                        {
                            TotalOilBBL = production.Oil is not null ? Math.Round(production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier, 5) : 0,
                            TotalOilM3 = production.Oil is not null ? Math.Round(production.Oil.TotalOil, 5) : 0,
                        },
                        Status = production.StatusProduction,
                        UepName = production.Installation.UepName,
                        Files = filesDto,
                        IsActive = production.IsActive,
                        Water = new WaterTotalDto
                        {
                            TotalWaterM3 = Math.Round(production.Water is not null ? production.Water.TotalWater : 0, 5),
                            TotalWaterBBL = Math.Round(production.Water is not null ? production.Water.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier : 0, 5)
                        }
                    };

                    productionsDto.Add(productionDto);
                }
            }

            return productionsDto;
        }

        public async Task<List<ProductionFilesDtoWithBase64>> DownloadAllProductionFiles(Guid productionId)
        {
            var production = await _repository
                .GetById(productionId);

            if (production is null)
                throw new NotFoundException("Produção não encontrada");

            var files = await _fileHistoryRepository
                .GetAllFilesByDate(production.MeasuredAt);

            var filesDto = new List<ProductionFilesDtoWithBase64>();

            foreach (var file in files)
            {
                var fileDto = new ProductionFilesDtoWithBase64
                {
                    FileName = file.FileName,
                    FileType = file.FileType,
                    ImportedAt = file.ImportedAt,
                    FileId = file.Id,
                    Base64 = file.FileContent
                };

                filesDto.Add(fileDto);
            }
            return filesDto;
        }

        public async Task DeleteProduction(Guid id)
        {
            var production = await _repository
                .GetById(id);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.IsActive is false)
                throw new ConflictException(ErrorMessages.InactiveAlready<Production>());

            if (production.WellProductions is not null && production.WellProductions.Count > 0)
                throw new ConflictException("Apropriação já foi feita, não é possível deletar a produção desse dia.");

            var deletedAt = DateTime.UtcNow.AddHours(-3);

            if (production.GasLinear is not null)
            {
                production.GasLinear.IsActive = false;
                production.GasLinear.DeletedAt = deletedAt;
            }

            if (production.GasDiferencial is not null)
            {
                production.GasDiferencial.IsActive = false;
                production.GasDiferencial.DeletedAt = deletedAt;
            }

            if (production.Gas is not null)
            {
                production.Gas.IsActive = false;
                production.Gas.DeletedAt = deletedAt;
            }

            if (production.Oil is not null)
            {
                production.Oil.IsActive = false;
                production.Oil.DeletedAt = deletedAt;
            }

            //if (production.Water is not null)
            //{
            //    production.Water.IsActive = false;
            //    production.Water.DeletedAt = deletedAt;
            //}

            //if (production.Comment is not null)
            //{
            //    production.Comment.IsActive = false;
            //    production.Comment.DeletedAt = deletedAt;
            //}

            if (production.NFSMs is not null)
            {
                foreach (var nfsm in production.NFSMs)
                {
                    nfsm.IsActive = false;
                    nfsm.DeletedAt = deletedAt;

                }
            }

            if (production.FieldsFR is not null)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    fieldFR.IsActive = false;
                    fieldFR.DeletedAt = deletedAt;
                }
            }

            if (production.Measurements is not null)
            {
                foreach (var measurement in production.Measurements)
                {
                    measurement.IsActive = false;
                    measurement.DeletedAt = deletedAt;

                    measurement.MeasurementHistory.IsActive = false;
                    measurement.MeasurementHistory.DeletedAt = deletedAt;
                }
            }

            production.IsActive = false;
            production.DeletedAt = deletedAt;

            _repository.Update(production);

            await _repository.SaveChangesAsync();
        }

        public async Task<DetailedGasBurnedDto> UpdateDetailedGas(Guid productionId, UpdateDetailedGasViewModel body)
        {
            var production = await _repository
                .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.CanDetailGasBurned is false)
                throw new ConflictException("Detalhamento de queima não é possível, somente após notificação de falha.");

            if (production.Gas is null)
                throw new NotFoundException("Produção de gás não encontrada.");

            var sumOfDetailedBurn = body.OthersBurn + body.EmergencialBurn + body.LimitOperacionalBurn + body.ForCommissioningBurn + body.ScheduledStopBurn + body.VentedGas + body.WellTestBurn;

            var totalBurnedGas = (production.GasLinear is not null ? production.GasLinear.BurntGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.BurntGas : 0);

            if (Math.Round(sumOfDetailedBurn, 5) != Math.Round(totalBurnedGas, 5))
                throw new BadRequestException("Total do volume detalhado deve ser igual ao total do volume de queima.");

            UpdateFields.CompareUpdateReturnOnlyUpdated(production.Gas, body);

            //production.CanDetailGasBurned = false;
            _repository.Update(production);

            var gasDto = new DetailedGasBurnedDto
            {
                GasId = production.Gas.Id,
                EmergencialBurn = production.Gas.EmergencialBurn,
                ForCommissioningBurn = production.Gas.ForCommissioningBurn,
                LimitOperacionalBurn = production.Gas.LimitOperacionalBurn,
                OthersBurn = production.Gas.OthersBurn,
                ScheduledStopBurn = production.Gas.ScheduledStopBurn,
                VentedGas = production.Gas.VentedGas,
                WellTestBurn = production.Gas.WellTestBurn
            };

            await _repository.SaveChangesAsync();

            return gasDto;
        }
    }


}