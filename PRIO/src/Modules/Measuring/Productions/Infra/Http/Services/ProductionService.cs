using AutoMapper;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.Infra.Utils;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Services
{
    public class ProductionService
    {
        private readonly IProductionRepository _repository;
        private readonly IGasVolumeCalculationRepository _gasRepository;
        private readonly IOilVolumeCalculationRepository _oilRepository;
        private readonly IMeasurementHistoryRepository _fileHistoryRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public ProductionService(IProductionRepository productionRepository, IMapper mapper, IGasVolumeCalculationRepository gasVolumeCalculationRepository, IInstallationRepository installationRepository, IOilVolumeCalculationRepository oilVolumeCalculationRepository, IMeasurementHistoryRepository measurementHistoryRepository)
        {
            _repository = productionRepository;
            _mapper = mapper;
            _gasRepository = gasVolumeCalculationRepository;
            _installationRepository = installationRepository;
            _oilRepository = oilVolumeCalculationRepository;
            _fileHistoryRepository = measurementHistoryRepository;

        }

        public async Task<ProductionDtoWithNullableDecimals> GetByDate(DateTime date)
        {
            var production = await _repository.GetExistingByDate(date);

            if (production is null)
                throw new NotFoundException($"Produção na data: {date.ToString("dd/MM/yyyy")} não encontrada");

            var dailyProduction = new DailyProduction
            {
                StatusProduction = production.StatusProduction,
                TotalGasBBL = Math.Round(
                    (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0) +
                    (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0),
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
                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = assistanceGas.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var hpFlare in gasCalculus.HPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == hpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!(burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring)) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = hpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var lpFlare in gasCalculus.LPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = lpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var pilot in gasCalculus.PilotGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == pilot.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = pilot.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var purge in gasCalculus.PurgeGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == purge.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = purge.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var lowPressure in gasCalculus.LowPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lowPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = lowPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            };


                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var highPressure in gasCalculus.HighPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == highPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = highPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            };

                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var import in gasCalculus.ImportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == import.MeasuringPoint.TagPointMeasuring;

                        if (!importedGasPoints.Any(measuring => measuring.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = import.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                            };

                            importedGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var export in gasCalculus.ExportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == export.MeasuringPoint.TagPointMeasuring;

                        if (!exportedGasPoints.Any(measuring => measuring.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasLinear,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_002,
                                LocalPoint = export.StaticLocalMeasuringPoint,
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

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = assistanceGas.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var hpFlare in gasCalculus.HPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == hpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = hpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var lpFlare in gasCalculus.LPFlares)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lpFlare.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = lpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var pilot in gasCalculus.PilotGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == pilot.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = pilot.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                            };
                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var purge in gasCalculus.PurgeGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == purge.MeasuringPoint.TagPointMeasuring;

                        if (!burnetGasPoints.Any(measuring => measuring.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = purge.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                            };

                            burnetGasPoints.Add(measuringPoint);
                            fuelGasPoints.Add(measuringPoint);

                        }
                    }

                    foreach (var lowPressure in gasCalculus.LowPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == lowPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = lowPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                            };
                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var highPressure in gasCalculus.HighPressureGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == highPressure.MeasuringPoint.TagPointMeasuring;

                        if (!fuelGasPoints.Any(measuring => measuring.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = highPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                            };

                            fuelGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var import in gasCalculus.ImportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == import.MeasuringPoint.TagPointMeasuring;

                        if (!importedGasPoints.Any(measuring => measuring.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = import.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                            };

                            importedGasPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var export in gasCalculus.ExportGases)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == export.MeasuringPoint.TagPointMeasuring;

                        if (!exportedGasPoints.Any(measuring => measuring.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalGasPointDto
                            {
                                DateMeasuring = measurementDateGasDiferencial,
                                IndividualProduction = measurement.MED_CORRIGIDO_MVMDO_003,
                                LocalPoint = export.StaticLocalMeasuringPoint,
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
                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {

                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurement.BswManual_001),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = section.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,

                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }

                    foreach (var dor in oilCalculus.DORs)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == dor.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurement.BswManual_001),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = dor.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }


                    foreach (var drain in oilCalculus.DrainVolumes)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == drain.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = drain.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001,
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }


                    foreach (var tog in oilCalculus.TOGRecoveredOils)
                    {
                        var measurementFound = measurement.MeasuringPoint.TagPointMeasuring == tog.MeasuringPoint.TagPointMeasuring;

                        if (!oilPoints.Any(measuring => measuring.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring) && measurementFound)
                        {
                            var measuringPoint = new LocalOilPointDto
                            {
                                DateMeasuring = measurementDateOil,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = tog.StaticLocalMeasuringPoint,
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

            var productionDto = new ProductionDtoWithNullableDecimals
            {
                InstallationName = production.Installation.Name,
                UepName = production.Installation.UepName,
                DailyProduction = dailyProduction,
                Gas = gasDto,
                Oil = oilDto,
                Files = files,
                Comment = commentDto
            };

            var gasCalculationByUepCode = await _gasRepository
               .GetGasVolumeCalculationByInstallationUEP(production.Installation.UepCod);

            var oilCalculationByUepCode = await _oilRepository
                  .GetOilVolumeCalculationByInstallationUEP(production.Installation.UepCod);

            foreach (var file in productionDto.Files)
            {
                if (gasCalculationByUepCode is not null && file.FileType == XmlUtils.File002)
                {
                    var containAssistanceGas = false;

                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = assistanceGas.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };

                                file.Summary.Add(summary);

                                containAssistanceGas = true;
                            }
                        }


                        if (containAssistanceGas is false && assistanceGas.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = assistanceGas.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containExportGas = false;

                    foreach (var export in gasCalculationByUepCode.ExportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (export.IsApplicable && export.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = export.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);

                                containExportGas = true;
                            }
                        }


                        if (containExportGas is false && export.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = export.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containHighPressureGas = false;

                    foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (highPressure.IsApplicable && highPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = highPressure.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);

                                containHighPressureGas = true;
                            }
                        }

                        if (containHighPressureGas is false && highPressure.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = highPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containHPFlare = false;

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = hpFlare.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);

                                containHPFlare = true;
                            }
                        }

                        if (containHPFlare is false && hpFlare.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = hpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containImportGas = false;

                    foreach (var import in gasCalculationByUepCode.ImportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (import.IsApplicable && import.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = import.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);
                                containImportGas = true;
                            }
                        }

                        if (containImportGas is false && import.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = import.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containLowPressureGas = false;

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = lowPressure.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);
                                containLowPressureGas = true;
                            }
                        }

                        if (containLowPressureGas is false && lowPressure.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = lowPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containLPFlare = false;

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = lpFlare.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);
                                containLPFlare = true;
                            }
                        }

                        if (containLPFlare is false && lpFlare.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = lpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containPilotGas = false;

                    foreach (var pilot in gasCalculationByUepCode.PilotGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (pilot.IsApplicable && pilot.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = pilot.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);

                                containPilotGas = true;
                            }
                        }

                        if (containPilotGas is false && pilot.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = pilot.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containPurgeGas = false;

                    foreach (var purge in gasCalculationByUepCode.PurgeGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (purge.IsApplicable && purge.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_002 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = purge.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_002,

                                };
                                file.Summary.Add(summary);

                                containPurgeGas = true;
                            }
                        }

                        if (containPurgeGas is false && purge.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = purge.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }
                }

                if (gasCalculationByUepCode is not null && file.FileType == XmlUtils.File003)
                {
                    var containAssistanceGas = false;

                    foreach (var assistanceGas in gasCalculationByUepCode.AssistanceGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == assistanceGas.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (assistanceGas.IsApplicable && assistanceGas.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = assistanceGas.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containAssistanceGas = true;
                            }
                        }


                        if (containAssistanceGas is false && assistanceGas.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = assistanceGas.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = assistanceGas.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containExportGas = false;

                    foreach (var export in gasCalculationByUepCode.ExportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == export.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (export.IsApplicable && export.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = export.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containExportGas = true;
                            }
                        }


                        if (containExportGas is false && export.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = export.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = export.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containHighPressureGas = false;

                    foreach (var highPressure in gasCalculationByUepCode.HighPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == highPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (highPressure.IsApplicable && highPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = highPressure.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containHighPressureGas = true;
                            }
                        }

                        if (containHighPressureGas is false && highPressure.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = highPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = highPressure.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containHPFlare = false;

                    foreach (var hpFlare in gasCalculationByUepCode.HPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == hpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (hpFlare.IsApplicable && hpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = hpFlare.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containHPFlare = true;
                            }
                        }

                        if (containHPFlare is false && hpFlare.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = hpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = hpFlare.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containImportGas = false;

                    foreach (var import in gasCalculationByUepCode.ImportGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == import.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (import.IsApplicable && import.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = import.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);
                                containImportGas = true;
                            }
                        }

                        if (containImportGas is false && import.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = import.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = import.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containLowPressureGas = false;

                    foreach (var lowPressure in gasCalculationByUepCode.LowPressureGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lowPressure.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (lowPressure.IsApplicable && lowPressure.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = lowPressure.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);
                                containLowPressureGas = true;
                            }
                        }

                        if (containLowPressureGas is false && lowPressure.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = lowPressure.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lowPressure.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containLPFlare = false;

                    foreach (var lpFlare in gasCalculationByUepCode.LPFlares)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == lpFlare.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (lpFlare.IsApplicable && lpFlare.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = lpFlare.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);
                                containLPFlare = true;
                            }
                        }

                        if (containLPFlare is false && lpFlare.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = lpFlare.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = lpFlare.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containPilotGas = false;

                    foreach (var pilot in gasCalculationByUepCode.PilotGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == pilot.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (pilot.IsApplicable && pilot.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = pilot.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containPilotGas = true;
                            }
                        }

                        if (containPilotGas is false && pilot.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = pilot.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = pilot.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containPurgeGas = false;

                    foreach (var purge in gasCalculationByUepCode.PurgeGases)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == purge.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (purge.IsApplicable && purge.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_003 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = purge.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_CORRIGIDO_MVMDO_003,

                                };
                                file.Summary.Add(summary);

                                containPurgeGas = true;
                            }
                        }

                        if (containPurgeGas is false && purge.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = purge.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = purge.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }
                }

                if (oilCalculationByUepCode is not null && file.FileType == XmlUtils.File001)
                {
                    var containSection = false;

                    foreach (var section in oilCalculationByUepCode.Sections)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == section.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (section.IsApplicable && section.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = section.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001,

                                };
                                file.Summary.Add(summary);

                                containSection = true;
                            }
                        }

                        if (containSection is false && section.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = section.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containDOR = false;

                    foreach (var dor in oilCalculationByUepCode.DORs)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == dor.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (dor.IsApplicable && dor.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = dor.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001,

                                };
                                file.Summary.Add(summary);

                                containDOR = true;
                            }
                        }

                        if (containDOR is false && dor.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = dor.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containDrain = false;

                    foreach (var drain in oilCalculationByUepCode.DrainVolumes)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == drain.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (drain.IsApplicable && drain.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = drain.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001,

                                };
                                file.Summary.Add(summary);
                                containDrain = true;
                            }
                        }

                        if (containDrain is false && drain.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = drain.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
                    }

                    var containTOGRecoveredOil = false;

                    foreach (var tog in oilCalculationByUepCode.TOGRecoveredOils)
                    {
                        var pointAlreadyInserted = file.Summary.Any(x => x.TagMeasuringPoint == tog.MeasuringPoint.TagPointMeasuring);

                        for (int i = 0; i < production.Measurements.Count; ++i)
                        {
                            var measurementResponse = production.Measurements[i];

                            if (tog.IsApplicable && tog.MeasuringPoint.TagPointMeasuring == measurementResponse.COD_TAG_PONTO_MEDICAO_001 && pointAlreadyInserted is false)
                            {
                                var summary = new SummaryGeneric
                                {
                                    DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                    LocationMeasuringPoint = tog.StaticLocalMeasuringPoint,
                                    StatusMeasuringPoint = true,
                                    TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                                    Volume = measurementResponse.MED_VOLUME_BRTO_CRRGO_MVMDO_001,

                                };
                                file.Summary.Add(summary);
                                containTOGRecoveredOil = true;
                            }
                        }

                        if (containTOGRecoveredOil is false && tog.IsApplicable && pointAlreadyInserted is false)
                        {
                            var measurementWrong = new SummaryGeneric
                            {
                                StatusMeasuringPoint = false,
                                DateMeasuring = production.MeasuredAt.ToString("dd/MM/yyyy"),
                                LocationMeasuringPoint = tog.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                                Volume = 0
                            };

                            file.Summary.Add(measurementWrong);
                        }
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
                        DateProduction = production.MeasuredAt.ToString("dd/MM/yyyy"),
                        Gas = new GasTotalDto
                        {
                            TotalGasBBL = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0), 2),
                            TotalGasM3 = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0), 2),
                        },
                        Oil = new OilTotalDto
                        {
                            TotalOilBBL = production.Oil is not null ? Math.Round(production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier, 2) : 0,
                            TotalOilM3 = production.Oil is not null ? Math.Round(production.Oil.TotalOil, 2) : 0,
                        },
                        Status = production.StatusProduction,
                        UepName = production.Installation.UepName,
                        Files = filesDto,
                        IsActive = production.IsActive,
                        Water = new WaterTotalDto
                        {
                            TotalWaterM3 = Math.Round(production.Water is not null ? production.Water.TotalWater : 0, 2),
                            TotalWaterBBL = Math.Round(production.Water is not null ? production.Water.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier : 0, 2)
                        }
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
                            TotalGasBBL = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0), 2),
                            TotalGasM3 = Math.Round((production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0), 2),
                        },
                        Oil = new OilTotalDto
                        {
                            TotalOilBBL = production.Oil is not null ? Math.Round(production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier, 2) : 0,
                            TotalOilM3 = production.Oil is not null ? Math.Round(production.Oil.TotalOil, 2) : 0,
                        },
                        Status = production.StatusProduction,
                        UepName = production.Installation.UepName,
                        Files = filesDto,
                        IsActive = production.IsActive,
                        Water = new WaterTotalDto
                        {
                            TotalWaterM3 = Math.Round(production.Water is not null ? production.Water.TotalWater : 0, 2),
                            TotalWaterBBL = Math.Round(production.Water is not null ? production.Water.TotalWater * ProductionUtils.m3ToBBLConversionMultiplier : 0, 2)
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
            var production = await _repository.GetById(id);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.IsActive is false)
                throw new ConflictException(ErrorMessages.InactiveAlready<Production>());

            if (production.GasLinear is not null)
            {
                production.GasLinear.IsActive = false;
                production.GasLinear.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.GasDiferencial is not null)
            {
                production.GasDiferencial.IsActive = false;
                production.GasDiferencial.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.Gas is not null)
            {
                production.Gas.IsActive = false;
                production.Gas.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.Oil is not null)
            {
                production.Oil.IsActive = false;
                production.Oil.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.Water is not null)
            {
                production.Water.IsActive = false;
                production.Water.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.Comment is not null)
            {
                production.Comment.IsActive = false;
                production.Comment.DeletedAt = DateTime.UtcNow.AddHours(-3);
            }

            if (production.NFSMs is not null)
            {
                foreach (var nfsm in production.NFSMs)
                {
                    nfsm.IsActive = false;
                    nfsm.DeletedAt = DateTime.UtcNow.AddHours(-3);
                }
            }

            if (production.FieldsFR is not null)
            {
                foreach (var fieldFR in production.FieldsFR)
                {
                    fieldFR.IsActive = false;
                    fieldFR.DeletedAt = DateTime.UtcNow.AddHours(-3);
                }
            }

            if (production.Measurements is not null)
            {
                foreach (var measurement in production.Measurements)
                {
                    measurement.IsActive = false;
                    measurement.DeletedAt = DateTime.UtcNow.AddHours(-3);
                }
            }

            production.IsActive = false;
            production.DeletedAt = DateTime.UtcNow.AddHours(-3);

            _repository.Update(production);

            await _repository.SaveChangesAsync();
        }
    }
}