using AutoMapper;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Measurements.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Dtos;
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

        public async Task<ProductionDto> GetByDate(DateTime date)
        {
            var production = await _repository.GetExistingByDate(date);

            if (production is null)
                throw new NotFoundException($"Produção na data: {date.ToString("dd/mm/yyyy")} não encontrada");

            var dailyProduction = new DailyProduction
            {
                Status = production.StatusProduction,
                TotalGasBBL = Math.Round(
                    (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0) +
                    (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0),
                    5),
                TotalGasM3 = Math.Round(
                    (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) +
                    (production.GasLinear is not null ? production.GasLinear.TotalGas : 0),
                    5),
                TotalOilBBL = Math.Round(
                    production.Oil is not null ? production.Oil.TotalOil : 0,
                    5),
                TotalOilM3 = Math.Round(
                    production.Oil is not null ? production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier : 0,
                    5),
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

            //var files = new List<MeasurementHistoryWithMeasurementsDto>();
            var files = new List<MeasurementHistoryDto>();
            var oilPoints = new List<LocalOilPointDto>();
            var burnetGasPoints = new List<LocalGasPointDto>();
            var fuelGasPoints = new List<LocalGasPointDto>();
            var importedGasPoints = new List<LocalGasPointDto>();
            var exportedGasPoints = new List<LocalGasPointDto>();

            //foreach (var measurement in production.Measurements)
            //{
            //    Console.WriteLine(measurement.COD_TAG_PONTO_MEDICAO_003);

            //    var historyDto = _mapper.Map<MeasurementHistory, MeasurementHistoryWithMeasurementsDto>(measurement.MeasurementHistory);

            //    if (historyDto.FileType == XmlUtils.File001)
            //    {
            //        historyDto.Summary.Add(new ClientInfoOil
            //        {
            //            DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_001,
            //            LocationMeasuringPoint = measurement.COD_TAG_PONTO_MEDICAO_001,
            //            StatusMeasuringPoint = measurement.StatusMeasuringPoint,
            //            TagMeasuringPoint = measurement.MeasuringPoint.TagPointMeasuring,
            //            VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
            //            Bsw = measurement.BswManual_001,
            //            VolumeAfterBsw = measurement.VolumeAfterManualBsw_001,
            //        });
            //    }
            //    else if (historyDto.FileType == XmlUtils.File002)
            //    {
            //        historyDto.Summary.Add(new ClientInfoGas
            //        {
            //            DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
            //            LocationMeasuringPoint = measurement.COD_TAG_PONTO_MEDICAO_002,
            //            StatusMeasuringPoint = measurement.StatusMeasuringPoint,
            //            TagMeasuringPoint = measurement.MeasuringPoint.TagPointMeasuring,
            //            Volume = measurement.MED_CORRIGIDO_MVMDO_002,
            //        });
            //    }
            //    else if (historyDto.FileType == XmlUtils.File003)
            //    {
            //        historyDto.Summary.Add(new ClientInfoGas
            //        {
            //            DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
            //            LocationMeasuringPoint = measurement.COD_TAG_PONTO_MEDICAO_003,
            //            StatusMeasuringPoint = measurement.StatusMeasuringPoint,
            //            TagMeasuringPoint = measurement.MeasuringPoint.TagPointMeasuring,
            //            Volume = measurement.MED_CORRIGIDO_MVMDO_003,
            //        });
            //    }

            //    if (!files.Any(file => file.ImportId == historyDto.ImportId))
            //    {
            //        files.Add(historyDto);
            //    }
            //}

            foreach (var measurement in production.Measurements)
            {
                var historyDto = _mapper.Map<MeasurementHistory, MeasurementHistoryDto>(measurement.MeasurementHistory);
                if (!files.Any(file => file.ImportId == historyDto.ImportId))
                {
                    files.Add(historyDto);
                }

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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_002,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_003,
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_001,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurement.BswManual_001),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = section.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = section.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_001,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001 * (1 - measurement.BswManual_001),
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = dor.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = dor.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_001,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = drain.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = drain.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001
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
                                DateMeasuring = measurement.DHA_INICIO_PERIODO_MEDICAO_001,
                                VolumeAfterBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                VolumeBeforeBsw = measurement.MED_VOLUME_BRTO_CRRGO_MVMDO_001,
                                LocalPoint = tog.StaticLocalMeasuringPoint,
                                TagMeasuringPoint = tog.MeasuringPoint.TagPointMeasuring,
                                Bsw = measurement.BswManual_001
                            };

                            oilPoints.Add(measuringPoint);
                        }
                    }


                }

            }

            //foreach (var file in files)
            //{
            //    var fileHistoryInDatabase = await _fileHistoryRepository.GetById(file.ImportId);
            //    var historyDto = _mapper.Map<MeasurementHistory, MeasurementHistoryWithMeasurementsDto>(fileHistoryInDatabase);

            //    foreach (var summary in file.Summary)
            //    {
            //        summary.Volume = historyDto.

            //    }
            //}

            var oilDto = new OilConsultingDto
            {
                TotalOilProduction = production.Oil is not null ? production.Oil.TotalOil : 0,
                MeasuringPoints = oilPoints
            };

            gasBurnt.MeasuringPoints = burnetGasPoints;
            gasFuel.MeasuringPoints = fuelGasPoints;
            gasImported.MeasuringPoints = importedGasPoints;
            gasExported.MeasuringPoints = exportedGasPoints;
            oilDto.MeasuringPoints = oilPoints;

            var gasDto = new GasConsultingDto
            {

                TotalGasProduction = (production.GasLinear is not null ? production.GasLinear.TotalGas : 0) + (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0),

                GasBurnt = gasBurnt,
                GasFuel = gasFuel,
                GasExported = gasExported,
                GasImported = gasImported

            };


            var productionDto = new ProductionDto
            {
                DailyProduction = dailyProduction,
                Gas = gasDto,
                Oil = oilDto,
                Files = files,
            };

            return productionDto;
        }


        public async Task<List<ProductionDto>> GetAllProductions(DateTime date)
        {

        }
    }
}
