using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.ViewModels;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;
using System.Text;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.Http.Services
{
    public class GasVolumeCalculationService
    {

        private readonly SystemHistoryService _systemHistoryService;
        private readonly IGasVolumeCalculationRepository _repository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;
        private readonly string _tableName = HistoryColumns.TableGasVolume;


        public GasVolumeCalculationService(SystemHistoryService systemHistoryService, IGasVolumeCalculationRepository repository, IMapper mapper, IMeasuringPointRepository measuringPointRepository, IInstallationRepository installationRepository)
        {
            _systemHistoryService = systemHistoryService;
            _repository = repository;
            _mapper = mapper;
            _measuringPointRepository = measuringPointRepository;
            _installationRepository = installationRepository;
        }

        public async Task<GasVolumeCalculationDto> CreateGasCalculaton(CreateGasVolumeCalculationViewModel body, User user)
        {
            var installation = await _installationRepository
                .GetByUEPCod(body.UepCode);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.GasVolumeCalculation is null)
                throw new ConflictException("Cálculo de volume de gás não registrado nessa instalação.");

            foreach (var assistanceGas in body.AssistanceGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(assistanceGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {assistanceGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var assistanceGasInDatabase = await _repository
                    .GetAssistanceGas(measuringPoint.Id);
                if (assistanceGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {assistanceGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var exportGas in body.ExportGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(exportGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {exportGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var exportGasInDatabase = await _repository
                    .GetExportGas(measuringPoint.Id);

                if (exportGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {exportGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var highPressureGas in body.HighPressureGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(highPressureGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {highPressureGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var highPressureGasInDatabase = await _repository
                    .GetHighPressureGas(measuringPoint.Id);

                if (highPressureGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {highPressureGasInDatabase.GasVolumeCalculation.Installation.Name}");
                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var hpFlares in body.HPFlares)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(hpFlares.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {hpFlares.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var hpFlaresInDatabase = await _repository
                    .GetHPFlare(measuringPoint.Id);

                if (hpFlaresInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {hpFlaresInDatabase.GasVolumeCalculation.Installation.Name}");
                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var importGas in body.ImportGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(importGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {importGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var importGasInDatabase = await _repository
                    .GetImportGas(measuringPoint.Id);

                if (importGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {importGasInDatabase.GasVolumeCalculation.Installation.Name}");
                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var lowPressureGas in body.LowPressureGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(lowPressureGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {lowPressureGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var lowPressureGasInDatabase = await _repository
                    .GetLowPressureGas(measuringPoint.Id);

                if (lowPressureGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {lowPressureGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var lpFlareGas in body.LPFlares)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(lpFlareGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {lpFlareGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var lpFlareGasInDatabase = await _repository
                    .GetLPFlare(measuringPoint.Id);

                if (lpFlareGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {lpFlareGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var pilotGas in body.PilotGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(pilotGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {pilotGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var pilotGasInDatabase = await _repository
                    .GetPilotGas(measuringPoint.Id);

                if (pilotGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {pilotGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var purgeGas in body.PurgeGases)
            {
                var measuringPoint = await _measuringPointRepository
                    .GetByIdAsync(purgeGas.MeasuringPointId);

                if (measuringPoint is null)
                    throw new NotFoundException($"Local do ponto de medição: {purgeGas.StaticMeasuringPointName}, {ErrorMessages.NotFound<MeasuringPoint>()}");

                var purgeGasInDatabase = await _repository
                    .GetPurgeGas(measuringPoint.Id);

                if (purgeGasInDatabase is not null)
                    throw new ConflictException($"Ponto de medição já possui relação com o cálculo da instalação {purgeGasInDatabase.GasVolumeCalculation.Installation.Name}");

                if (measuringPoint.AssistanceGas is not null || measuringPoint.ExportGas is not null || measuringPoint.ImportGas is not null || measuringPoint.PilotGas is not null || measuringPoint.HighPressureGas is not null || measuringPoint.HPFlare is not null || measuringPoint.PurgeGas is not null || measuringPoint.LowPressureGas is not null || measuringPoint.LPFlare is not null)
                    throw new ConflictException("Já existe um ponto de medição para essa configuração de cálculo");
            }

            foreach (var assistanceGas in body.AssistanceGases)

            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(assistanceGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdAssistanceGas = new AssistanceGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = assistanceGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = assistanceGas.IsApplicable

                    };

                    await _repository.AddAssistanceGas(createdAssistanceGas);
                }
            }

            foreach (var exportGas in body.ExportGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(exportGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdExportGas = new ExportGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = exportGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = exportGas.IsApplicable
                    };

                    await _repository.AddExportGas(createdExportGas);
                }
            }

            foreach (var highPressureGas in body.HighPressureGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(highPressureGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdHighPressureGas = new HighPressureGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = highPressureGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = highPressureGas.IsApplicable
                    };

                    await _repository.AddHighPressureGas(createdHighPressureGas);
                }
            }

            foreach (var hpFlare in body.HPFlares)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(hpFlare.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdHPFlare = new HPFlare
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = hpFlare.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = hpFlare.IsApplicable
                    };

                    await _repository.AddHPFlare(createdHPFlare);
                }
            }
            foreach (var importGas in body.ImportGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(importGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdImportGas = new ImportGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = importGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = importGas.IsApplicable
                    };

                    await _repository.AddImportGas(createdImportGas);
                }
            }
            foreach (var lowPressureGas in body.LowPressureGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(lowPressureGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdLowPressureGas = new LowPressureGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = lowPressureGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = lowPressureGas.IsApplicable
                    };

                    await _repository.AddLowPressureGas(createdLowPressureGas);
                }
            }

            foreach (var lpFlare in body.LPFlares)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(lpFlare.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdLPFlare = new LPFlare
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = lpFlare.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = lpFlare.IsApplicable
                    };

                    await _repository.AddLPFlare(createdLPFlare);
                }
            }

            foreach (var pilotGas in body.PilotGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(pilotGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdPilotGas = new PilotGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = pilotGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = pilotGas.IsApplicable
                    };

                    await _repository.AddPilotGas(createdPilotGas);
                }
            }

            foreach (var purgeGas in body.PurgeGases)
            {
                var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(purgeGas.MeasuringPointId);
                if (measuringPoint is not null)
                {
                    var createdPurgeGas = new PurgeGas
                    {
                        Id = Guid.NewGuid(),
                        MeasuringPoint = measuringPoint,
                        StaticLocalMeasuringPoint = purgeGas.StaticMeasuringPointName,
                        GasVolumeCalculation = installation.GasVolumeCalculation,
                        IsApplicable = purgeGas.IsApplicable
                    };

                    await _repository.AddPurgeGas(createdPurgeGas);
                }
            }

            await _repository.SaveChangesAsync();
            var gasVolumeCalculationDto = _mapper.Map<GasVolumeCalculationDto>(installation.GasVolumeCalculation);

            return gasVolumeCalculationDto;
        }

        public async Task<GasVolumeCalculationDto> GetGasCalculationByInstallationId(Guid installationId)
        {
            var installationInDatabase = await _installationRepository.GetByIdAsync(installationId);
            if (installationInDatabase is null)
                throw new NotFoundException("Instalação não encontrada");

            var gasVolumeCalculation = await _repository.GetGasVolumeCalculationByInstallationId(installationId);

            if (gasVolumeCalculation is null)
                throw new NotFoundException("Cálculo de gás não encontrado nessa instalação");

            if (gasVolumeCalculation is not null)
            {
                gasVolumeCalculation.AssistanceGases = gasVolumeCalculation.AssistanceGases
                    .Where(x => x.IsActive)
                    .ToList();

                gasVolumeCalculation.ExportGases = gasVolumeCalculation.ExportGases
                    .Where(x => x.IsActive)
                    .ToList();

                gasVolumeCalculation.HighPressureGases = gasVolumeCalculation.HighPressureGases
                    .Where(x => x.IsActive)
                    .ToList();

                gasVolumeCalculation.HPFlares = gasVolumeCalculation.HPFlares
                    .Where(x => x.IsActive)
                    .ToList();
                gasVolumeCalculation.ImportGases = gasVolumeCalculation.ImportGases
                   .Where(x => x.IsActive)
                   .ToList();

                gasVolumeCalculation.LowPressureGases = gasVolumeCalculation.LowPressureGases
                   .Where(x => x.IsActive)
                   .ToList();

                gasVolumeCalculation.LPFlares = gasVolumeCalculation.LPFlares
                   .Where(x => x.IsActive)
                   .ToList();

                gasVolumeCalculation.PilotGases = gasVolumeCalculation.PilotGases
                   .Where(x => x.IsActive)
                   .ToList();

                gasVolumeCalculation.PurgeGases = gasVolumeCalculation.PurgeGases
                   .Where(x => x.IsActive)
                   .ToList();

            }

            var gasVolumeCalculationDTO = _mapper.Map<GasVolumeCalculation, GasVolumeCalculationDto>(gasVolumeCalculation);
            return gasVolumeCalculationDTO;
        }

        public async Task<GasEquationDto> GetGasEquationByInstallationId(Guid installationId)
        {
            var installation = await _installationRepository.GetByIdWithCalculationsAsync(installationId);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.GasVolumeCalculation is null)
                throw new ConflictException("Cálculo de volume de gás não registrado nessa instalação.");

            var gasVolumeCalculation = await _repository.GetGasVolumeCalculationByInstallationId(installationId);

            if (gasVolumeCalculation is null)
                throw new NotFoundException("Cálculo de gás não encontrado nessa instalação");

            var equationBuilder = new StringBuilder();
            var totalBurnedGasBuilder = new StringBuilder();
            var totalFuelGasBuilder = new StringBuilder();

            var exportGasBuilder = new StringBuilder();
            var importGasBuilder = new StringBuilder();

            var pilotGasBuilder = new StringBuilder();
            var purgeGasBuilder = new StringBuilder();

            var applicableHPFlares = gasVolumeCalculation.HPFlares
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var hpFlare in applicableHPFlares)
            {
                equationBuilder.Append($" + {hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint}");
                totalBurnedGasBuilder.Append($" + {hpFlare.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableLPFlares = gasVolumeCalculation.LPFlares
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var lpFlare in applicableLPFlares)
            {
                equationBuilder.Append($" + {lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint}");
                totalBurnedGasBuilder.Append($" + {lpFlare.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableAssistanceGases = gasVolumeCalculation.AssistanceGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var assistanceGas in applicableAssistanceGases)
            {
                equationBuilder.Append($" + {assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
                totalBurnedGasBuilder.Append($" + {assistanceGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicablePilotGases = gasVolumeCalculation.PilotGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var pilotGas in applicablePilotGases)
            {
                pilotGasBuilder.Append($" + {pilotGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicablePurgeGases = gasVolumeCalculation.PurgeGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var purgeGas in applicablePurgeGases)
            {
                purgeGasBuilder.Append($" + {purgeGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableLowPressureGases = gasVolumeCalculation.LowPressureGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var lowPressureGas in applicableLowPressureGases)
            {
                equationBuilder.Append($" + {lowPressureGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
                totalFuelGasBuilder.Append($" + {lowPressureGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableHighPressureGases = gasVolumeCalculation.HighPressureGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var highPressureGas in applicableHighPressureGases)
            {
                equationBuilder.Append($" + {highPressureGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
                totalFuelGasBuilder.Append($" + {highPressureGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableExportGases = gasVolumeCalculation.ExportGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var exportGas in applicableExportGases)
            {
                exportGasBuilder.Append($" + {exportGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            var applicableImportGases = gasVolumeCalculation.ImportGases
                .Where(x => x.IsActive && x.IsApplicable)
                .ToList();

            foreach (var importGas in applicableImportGases)
            {
                importGasBuilder.Append($" - {importGas.MeasuringPoint.DinamicLocalMeasuringPoint}");
            }

            totalBurnedGasBuilder.Append(purgeGasBuilder);
            totalBurnedGasBuilder.Append(pilotGasBuilder);
            equationBuilder.Append(pilotGasBuilder);
            equationBuilder.Append(purgeGasBuilder);

            // Handle parentheses for PilotGases and PurgeGases if needed
            if (!string.IsNullOrEmpty(pilotGasBuilder.ToString()) || !string.IsNullOrEmpty(purgeGasBuilder.ToString()))
            {
                equationBuilder.Append(" - (");
                equationBuilder.Append(pilotGasBuilder);
                equationBuilder.Append(purgeGasBuilder);
                equationBuilder.Append(")");

                totalFuelGasBuilder.Append(" - (");
                totalFuelGasBuilder.Append(pilotGasBuilder);
                totalFuelGasBuilder.Append(purgeGasBuilder);
                totalFuelGasBuilder.Append(")");
            }

            // Add the ExportGases and ImportGases in parentheses
            if (!string.IsNullOrEmpty(exportGasBuilder.ToString()) || !string.IsNullOrEmpty(importGasBuilder.ToString()))
            {
                equationBuilder.Append(" + (");
                equationBuilder.Append(exportGasBuilder);
                equationBuilder.Append(importGasBuilder);
                equationBuilder.Append(")");
            }

            // Remove the leading '+ ' and trailing '- ' from the equation string
            var equation = equationBuilder.ToString().TrimStart().TrimStart('+').TrimStart().TrimStart('-');
            var totalBurnedGas = totalBurnedGasBuilder.ToString().TrimStart().TrimStart('+').TrimStart().TrimStart('-');
            var totalFuelGas = totalFuelGasBuilder.ToString().TrimStart().TrimStart('+').TrimStart().TrimStart('-');

            var equationReturned = new GasEquationDto
            {
                TotalProducedGas = equation,
                TotalBurnedGas = totalBurnedGas,
                TotalFuelGas = totalFuelGas
            };

            return equationReturned;
        }


        public async Task<GasVolumeCalculationDto> UpdateGasCalculationByInstallationId(Guid installationId, UpdateGasVolumeCalculationViewModel body)
        {
            var installation = await _installationRepository
                .GetByIdWithCalculationsAsync(installationId);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.OilVolumeCalculation is null)
                throw new NotFoundException("Instalação não possui cálculo para ser atualizado");

            var gasCalculationInDatabase = await _repository
                .GetGasVolumeCalculationByIdAsync(installation.GasVolumeCalculation.Id);

            if (gasCalculationInDatabase is null)
                throw new NotFoundException("Instalação não possui cálculo para ser atualizado");

            if (body.AssistanceGases is not null)
            {
                foreach (var assistanceGas in body.AssistanceGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(assistanceGas.MeasuringPointId);
                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {assistanceGas.StaticMeasuringPointName} não encontrado.");

                    var assistanceGasFound = await _repository
                        .GetOtherAssistanceGas(installation.GasVolumeCalculation.Id, assistanceGas.MeasuringPointId);

                    if (assistanceGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({assistanceGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.AssistanceGases);

                foreach (var assistanceGas in body.AssistanceGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(assistanceGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdAssistanceGas = new AssistanceGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = assistanceGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = assistanceGas.IsApplicable

                        };

                        await _repository.AddAssistanceGas(createdAssistanceGas);
                    }
                }
            }

            if (body.ExportGases is not null)
            {
                foreach (var exportGas in body.ExportGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(exportGas.MeasuringPointId);
                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {exportGas.StaticMeasuringPointName} não encontrado.");

                    var exportGasFound = await _repository
                        .GetOtherExportGas(installation.GasVolumeCalculation.Id, exportGas.MeasuringPointId);

                    if (exportGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({exportGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.ExportGases);

                foreach (var exportGas in body.ExportGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(exportGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdExportGas = new ExportGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = exportGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = exportGas.IsApplicable

                        };

                        await _repository.AddExportGas(createdExportGas);
                    }
                }
            }

            if (body.HighPressureGases is not null)
            {
                foreach (var highPressureGas in body.HighPressureGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(highPressureGas.MeasuringPointId);
                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {highPressureGas.StaticMeasuringPointName} não encontrado.");

                    var highPressureGasFound = await _repository
                        .GetOtherHighPressureGas(installation.GasVolumeCalculation.Id, highPressureGas.MeasuringPointId);

                    if (highPressureGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({highPressureGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.HighPressureGases);

                foreach (var highPressureGas in body.HighPressureGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(highPressureGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdHighPressureGas = new HighPressureGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = highPressureGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = highPressureGas.IsApplicable

                        };

                        await _repository.AddHighPressureGas(createdHighPressureGas);
                    }
                }
            }

            if (body.HPFlares is not null)
            {
                foreach (var hpFlare in body.HPFlares)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(hpFlare.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {hpFlare.StaticMeasuringPointName} não encontrado.");

                    var hpFlareFound = await _repository
                        .GetOtherHPFlare(installation.GasVolumeCalculation.Id, hpFlare.MeasuringPointId);

                    if (hpFlareFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({hpFlareFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.HPFlares);

                foreach (var hpFlare in body.HPFlares)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(hpFlare.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdHPFlare = new HPFlare
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = hpFlare.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = hpFlare.IsApplicable

                        };

                        await _repository.AddHPFlare(createdHPFlare);
                    }
                }
            }

            if (body.ImportGases is not null)
            {
                foreach (var importGas in body.ImportGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(importGas.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {importGas.StaticMeasuringPointName} não encontrado.");

                    var importGasFound = await _repository
                        .GetOtherImportGas(installation.GasVolumeCalculation.Id, importGas.MeasuringPointId);

                    if (importGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({importGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.ImportGases);

                foreach (var importGas in body.ImportGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(importGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdImportGas = new ImportGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = importGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = importGas.IsApplicable

                        };

                        await _repository.AddImportGas(createdImportGas);
                    }
                }
            }

            if (body.LowPressureGases is not null)
            {
                foreach (var lowPressureGas in body.LowPressureGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(lowPressureGas.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {lowPressureGas.StaticMeasuringPointName} não encontrado.");

                    var lowPressureGasFound = await _repository
                        .GetOtherLowPressureGas(installation.GasVolumeCalculation.Id, lowPressureGas.MeasuringPointId);

                    if (lowPressureGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({lowPressureGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.LowPressureGases);

                foreach (var lowPressureGas in body.LowPressureGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(lowPressureGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdLowPressureGas = new LowPressureGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = lowPressureGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = lowPressureGas.IsApplicable

                        };

                        await _repository.AddLowPressureGas(createdLowPressureGas);
                    }
                }
            }

            if (body.LPFlares is not null)
            {
                foreach (var lpFlare in body.LPFlares)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(lpFlare.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {lpFlare.StaticMeasuringPointName} não encontrado.");

                    var lpFlareFound = await _repository
                        .GetOtherLPFlare(installation.GasVolumeCalculation.Id, lpFlare.MeasuringPointId);

                    if (lpFlareFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({lpFlareFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.LPFlares);

                foreach (var lpFlare in body.LPFlares)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(lpFlare.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdLPFlare = new LPFlare
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = lpFlare.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = lpFlare.IsApplicable

                        };

                        await _repository.AddLPFlare(createdLPFlare);
                    }
                }
            }

            if (body.PilotGases is not null)
            {
                foreach (var pilotGas in body.PilotGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(pilotGas.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {pilotGas.StaticMeasuringPointName} não encontrado.");

                    var pilotGasFound = await _repository
                        .GetOtherPilotGas(installation.GasVolumeCalculation.Id, pilotGas.MeasuringPointId);

                    if (pilotGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({pilotGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.PilotGases);

                foreach (var pilotGas in body.PilotGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(pilotGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdPilotGas = new PilotGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = pilotGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = pilotGas.IsApplicable

                        };

                        await _repository.AddPilotGas(createdPilotGas);
                    }
                }
            }

            if (body.PilotGases is not null)
            {
                foreach (var pilotGas in body.PilotGases)
                {
                    var measuringPoint = await _measuringPointRepository.GetByIdAsync(pilotGas.MeasuringPointId);

                    if (measuringPoint is null)
                        throw new NotFoundException($"Ponto de medição {pilotGas.StaticMeasuringPointName} não encontrado.");

                    var pilotGasFound = await _repository
                        .GetOtherPilotGas(installation.GasVolumeCalculation.Id, pilotGas.MeasuringPointId);

                    if (pilotGasFound is not null)
                        throw new ConflictException($"Ponto de medição para ser atualizado possui relação com outra instalação ({pilotGasFound.GasVolumeCalculation.Installation.Name}).");
                }

                _repository.RemoveRange(gasCalculationInDatabase.PilotGases);

                foreach (var pilotGas in body.PilotGases)
                {
                    var measuringPoint = await _measuringPointRepository
                        .GetByIdAsync(pilotGas.MeasuringPointId);

                    if (measuringPoint is not null)
                    {
                        var createdPilotGas = new PilotGas
                        {
                            Id = Guid.NewGuid(),
                            MeasuringPoint = measuringPoint,
                            StaticLocalMeasuringPoint = pilotGas.StaticMeasuringPointName,
                            GasVolumeCalculation = installation.GasVolumeCalculation,
                            IsApplicable = pilotGas.IsApplicable

                        };

                        await _repository.AddPilotGas(createdPilotGas);
                    }
                }
            }

            await _repository.SaveChangesAsync();

            var gasVolumeCalculationDTO = _mapper.Map<GasVolumeCalculation, GasVolumeCalculationDto>(gasCalculationInDatabase);

            return gasVolumeCalculationDTO;
        }
    }
}
