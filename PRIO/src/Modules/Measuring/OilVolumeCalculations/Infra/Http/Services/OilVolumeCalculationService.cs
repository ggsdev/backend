using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Services
{
    public class OilVolumeCalculationService
    {
        private readonly IMapper _mapper;
        private readonly IInstallationRepository _installationRepository;
        private readonly IOilVolumeCalculationRepository _oilVolumeCalculationRepository;
        private readonly IMeasuringPointRepository _mpointRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableInstallations;

        public OilVolumeCalculationService(IMapper mapper, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IMeasuringPointRepository mpointRepository, IOilVolumeCalculationRepository oilVolumeCalculationRepository)
        {
            _mapper = mapper;
            _oilVolumeCalculationRepository = oilVolumeCalculationRepository;
            _systemHistoryService = systemHistoryService;
            _installationRepository = installationRepository;
            _mpointRepository = mpointRepository;
        }

        public async Task<OilVolumeCalculationDTO?> CreateOilVolumeCalculation(CreateOilVolumeCalculationViewModel body, User user)
        {
            if (body.UEPCode is null)
                throw new ConflictException("UEP Code is required");

            var installationInDatabase = await _installationRepository.GetByUEPCod(body.UEPCode) ?? throw new NotFoundException("UEP is not found.");

            if (body.Sections is not null)
            {
                if (body.Sections.Count == 0)
                    throw new NotFoundException("Deve ser inseridos ao menos um tramo.");

                foreach (var section in body.Sections)
                {
                    // VERIFICAR SE PONTO DE MEDIÇÃO EXISTE
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(section.MeasuringPointId) ?? throw new NotFoundException("Ponto de medição de tramo não encontrado.");

                    //VERIFICAR SE PONTO DE MEDIÇÃO ESTÁ CADASTRADO EM OUTRO CALCULO
                    var sectionFound = await _oilVolumeCalculationRepository.GetSectionByMeasuringPointIdAsync(section.MeasuringPointId);
                    if (sectionFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {sectionFound.OilVolumeCalculation.Installation.Name}");
                }
            }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(tog.MeasuringPointId) ?? throw new NotFoundException("Ponto de medição TOG não encontrado.");
                    var togFound = await _oilVolumeCalculationRepository.GetTOGByMeasuringPointIdAsync(tog.MeasuringPointId);
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {togFound.OilVolumeCalculation.Installation.Name}");
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(dor.MeasuringPointId) ?? throw new NotFoundException("Ponto de medição DOR não encontrado");
                    var dorFound = await _oilVolumeCalculationRepository.GetDORByMeasuringPointIdAsync(dor.MeasuringPointId);
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {dorFound.OilVolumeCalculation.Installation.Name}");
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(drain.MeasuringPointId) ?? throw new NotFoundException("Ponto de medição Dreno não encontrado.");
                    var drainFound = await _oilVolumeCalculationRepository.GetDrainByMeasuringPointIdAsync(drain.MeasuringPointId);
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {drainFound.OilVolumeCalculation.Installation.Name}");
                }

            HasDuplicateIds(body);

            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Calculo de volume de óleo não encontrado ");

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(section.MeasuringPointId);
                    await _oilVolumeCalculationRepository.AddSection(installationInDatabase.OilVolumeCalculation, MeasuringPoint, section);
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(tog.MeasuringPointId);
                    await _oilVolumeCalculationRepository.AddTOG(installationInDatabase.OilVolumeCalculation, MeasuringPoint, tog);
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(dor.MeasuringPointId);
                    await _oilVolumeCalculationRepository.AddDOR(installationInDatabase.OilVolumeCalculation, MeasuringPoint, dor);
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(drain.MeasuringPointId);
                    await _oilVolumeCalculationRepository.AddDrain(installationInDatabase.OilVolumeCalculation, MeasuringPoint, drain);
                }

            await _oilVolumeCalculationRepository.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(installationInDatabase.OilVolumeCalculation);

            return OilVolumeCalculationDTO;
        }
        public async Task<OilVolumeCalculationDTO?> GetById(Guid installationId)
        {
            var installationInDatabase = await _installationRepository.GetByIdAsync(installationId);
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");

            var oilVolumeCalculation = await _oilVolumeCalculationRepository.GetOilVolumeCalculationByInstallationId(installationId);

            if (oilVolumeCalculation != null)
            {
                oilVolumeCalculation.DrainVolumes = oilVolumeCalculation.DrainVolumes
                    .Where(x => x.IsActive == true)
                    .ToList();

                oilVolumeCalculation.DORs = oilVolumeCalculation.DORs
                    .Where(x => x.IsActive == true)
                    .ToList();

                oilVolumeCalculation.Sections = oilVolumeCalculation.Sections
                    .Where(x => x.IsActive == true)
                    .ToList();

                oilVolumeCalculation.TOGRecoveredOils = oilVolumeCalculation.TOGRecoveredOils
                    .Where(x => x.IsActive == true)
                    .ToList();
            }

            var oilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilVolumeCalculation);
            return oilVolumeCalculationDTO;
        }
        public async Task<List<OilVolumeCalculationDTO>> GetAll()
        {
            var oilVolumeCalculation = await _oilVolumeCalculationRepository.GetAllOils();

            var oilVolumeCalculationDTO = _mapper.Map<List<OilVolumeCalculation>, List<OilVolumeCalculationDTO>>(oilVolumeCalculation);
            return oilVolumeCalculationDTO;
        }
        public async Task<object> GetEquation(Guid installationId)
        {
            var installationInDatabase = await _installationRepository.GetByIdAsync(installationId);
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");

            var OilVolumeCalculation = await _oilVolumeCalculationRepository.GetOilVolumeCalculationByInstallationId(installationId);
            string? equation = "";

            if (OilVolumeCalculation.Sections is not null && OilVolumeCalculation.Sections.Count > 0)
            {
                var countSectionsActives = OilVolumeCalculation.Sections.Where(x => x.IsActive == true && x.IsApplicable == true).ToList();

                for (var i = 0; i < countSectionsActives.Count; i++)
                {
                    if (countSectionsActives[i] is not null)
                    {
                        equation += $"{countSectionsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint} * (1 - BSW {countSectionsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint})";

                        if (i + 1 != countSectionsActives.Count)
                        {
                            equation += " + ";
                        }

                    }
                }
            }

            if (OilVolumeCalculation.TOGRecoveredOils is not null && OilVolumeCalculation.TOGRecoveredOils.Count > 0)
            {
                var countTogsActives = OilVolumeCalculation.TOGRecoveredOils.Where(x => x.IsActive == true && x.IsApplicable == true).ToList();
                if (countTogsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(')
                    {
                        equation += " + ";
                    }

                    int? togsCount = OilVolumeCalculation.TOGRecoveredOils.Count;

                    for (var i = 0; i < countTogsActives.Count; i++)
                    {
                        if (countTogsActives[i] is not null)
                        {
                            equation += $"{countTogsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint}";

                            if (i + 1 != countTogsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DrainVolumes is not null && OilVolumeCalculation.DrainVolumes.Count > 0)
            {
                var countDrainsActives = OilVolumeCalculation.DrainVolumes.Where(x => x.IsActive == true && x.IsApplicable == true).ToList();
                if (countDrainsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                    {
                        equation += " + ";
                    }

                    int? drainCount = OilVolumeCalculation.DrainVolumes.Count;

                    for (var i = 0; i < countDrainsActives.Count; i++)
                    {
                        if (countDrainsActives[i] is not null)
                        {
                            equation += $"{countDrainsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint}";

                            if (i + 1 != countDrainsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DORs is not null && OilVolumeCalculation.DORs.Count > 0)
            {
                var countDorsActives = OilVolumeCalculation.DORs.Where(x => x.IsActive == true && x.IsApplicable == true).ToList();
                if (countDorsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                    {
                        equation += " - (";
                    }

                    for (var i = 0; i < countDorsActives.Count; i++)
                    {
                        if (OilVolumeCalculation.DORs[i] is not null)
                        {
                            equation += $"{countDorsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint} * (1 - BSW {countDorsActives[i].MeasuringPoint.DinamicLocalMeasuringPoint})";

                            if (i + 1 != countDorsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                    equation += ")";
                }
            }

            var eq = new { equation = equation };
            return eq;
        }
        public async Task<OilVolumeCalculationDTO> Update(Guid installationId, CreateOilVolumeCalculationViewModel body)
        {
            var installationInDatabase = await _installationRepository.GetByIdWithCalculationsAsync(installationId);
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (body.Sections is not null)
            {
                if (body.Sections.Count == 0)
                    throw new NotFoundException("Deve ser inseridos ao menos um tramo.");

                foreach (var section in body.Sections)
                {
                    var measuringPoint = await _mpointRepository.GetByIdAsync(section.MeasuringPointId) ?? throw new NotFoundException("Equipment section is not found.");
                    var sectionFound = await _oilVolumeCalculationRepository.GetSectionOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, section.MeasuringPointId);
                    if (sectionFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({sectionFound.OilVolumeCalculation.Installation.Name}).");
                }
            }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var measuringPoint = await _mpointRepository.GetByIdAsync(tog.MeasuringPointId) ?? throw new NotFoundException("Equipment TOG is not found.");
                    var togFound = await _oilVolumeCalculationRepository.GetTOGOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, tog.MeasuringPointId);
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({togFound.OilVolumeCalculation.Installation.Name}).");
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var measuringPoint = await _mpointRepository.GetByIdAsync(dor.MeasuringPointId) ?? throw new NotFoundException("Equipment DOR is not found.");
                    var dorFound = await _oilVolumeCalculationRepository.GetDOROtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, dor.MeasuringPointId);
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({dorFound.OilVolumeCalculation.Installation.Name}).");
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var measuringPoint = await _mpointRepository.GetByIdAsync(drain.MeasuringPointId) ?? throw new NotFoundException("Equipment Drain is not found.");
                    var drainFound = await _oilVolumeCalculationRepository.GetDrainOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, drain.MeasuringPointId);
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({drainFound.OilVolumeCalculation.Installation.Name}).");
                }

            var oilCalculationInDatabase = await _oilVolumeCalculationRepository.GetOilVolumeCalculationById(installationInDatabase.OilVolumeCalculation.Id);
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (oilCalculationInDatabase.Sections.Any())
            {
                await _oilVolumeCalculationRepository.RemoveSectionRange(oilCalculationInDatabase.Sections);
            }

            if (oilCalculationInDatabase.TOGRecoveredOils.Any())
            {
                await _oilVolumeCalculationRepository.RemoveTOGRange(oilCalculationInDatabase.TOGRecoveredOils);
            }

            if (oilCalculationInDatabase.DrainVolumes.Any())
            {
                await _oilVolumeCalculationRepository.RemoveDrainRange(oilCalculationInDatabase.DrainVolumes);
            }

            if (oilCalculationInDatabase.DORs.Any())
            {
                await _oilVolumeCalculationRepository.RemoveDORRange(oilCalculationInDatabase.DORs);
            }

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(section.MeasuringPointId);
                    var createSection = await _oilVolumeCalculationRepository.AddSection(oilCalculationInDatabase, MeasuringPoint, section);
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(tog.MeasuringPointId);
                    var createTog = await _oilVolumeCalculationRepository.AddTOG(oilCalculationInDatabase, MeasuringPoint, tog);
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(dor.MeasuringPointId);

                    var createDor = await _oilVolumeCalculationRepository.AddDOR(oilCalculationInDatabase, MeasuringPoint, dor);
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(drain.MeasuringPointId);

                    var createDrain = await _oilVolumeCalculationRepository.AddDrain(oilCalculationInDatabase, MeasuringPoint, drain);
                }

            await _oilVolumeCalculationRepository.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilCalculationInDatabase);

            return OilVolumeCalculationDTO;
        }
        public async Task<OilVolumeCalculationDTO> Refresh(Guid installationId)
        {
            var installationInDatabase = await _installationRepository.GetByIdWithCalculationsAsync(installationId);
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            var oilCalculationInDatabase = await _oilVolumeCalculationRepository.GetOilVolumeCalculationById(installationInDatabase.OilVolumeCalculation.Id);
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (oilCalculationInDatabase.Sections.Any())
            {
                foreach (var section in oilCalculationInDatabase.Sections)
                {
                    var foundSection = await _oilVolumeCalculationRepository.GetSectionByIdAsync(section.Id);
                    foundSection.DeletedAt = null;
                    foundSection.IsActive = true;

                    await _oilVolumeCalculationRepository.UpdateSection(foundSection);
                }
            }

            if (oilCalculationInDatabase.TOGRecoveredOils.Any())
            {
                foreach (var tog in oilCalculationInDatabase.TOGRecoveredOils)
                {
                    var foundTOG = await _oilVolumeCalculationRepository.GetTOGByIdAsync(tog.Id);
                    foundTOG.DeletedAt = null;
                    foundTOG.IsActive = true;

                    await _oilVolumeCalculationRepository.UpdateTOG(foundTOG);
                }
            }

            if (oilCalculationInDatabase.DrainVolumes.Any())
            {
                foreach (var drain in oilCalculationInDatabase.DrainVolumes)
                {
                    var foundDrain = await _oilVolumeCalculationRepository.GetDrainByIdAsync(drain.Id);
                    foundDrain.DeletedAt = null;
                    foundDrain.IsActive = true;

                    await _oilVolumeCalculationRepository.UpdateDrain(foundDrain);
                }
            }

            if (oilCalculationInDatabase.DORs.Any())
            {
                foreach (var dor in oilCalculationInDatabase.DORs)
                {
                    var foundDOR = await _oilVolumeCalculationRepository.GetDORByIdAsync(dor.Id);
                    foundDOR.DeletedAt = null;
                    foundDOR.IsActive = true;

                    await _oilVolumeCalculationRepository.UpdateDOR(foundDOR);
                }
            }

            await _oilVolumeCalculationRepository.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilCalculationInDatabase);
            return OilVolumeCalculationDTO;
        }
        public async Task Delete(Guid installationId, CreateOilVolumeCalculationViewModel body)
        {
            var installationInDatabase = await _installationRepository.GetByIdWithCalculationsAsync(installationId);
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(section.MeasuringPointId) ?? throw new NotFoundException("Equipment section is not found.");
                    var sectionFound = await _oilVolumeCalculationRepository.GetSectionOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, section.MeasuringPointId);
                    if (sectionFound is not null)
                    {
                        throw new ConflictException($"Equipamento para ser deletado possui relação com outra instalação ({sectionFound.OilVolumeCalculation.Installation.Name}).");
                    }
                    var sectionFoundSame = await _oilVolumeCalculationRepository.GetSectionInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, section.MeasuringPointId);
                    if (sectionFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(tog.MeasuringPointId) ?? throw new NotFoundException("Equipment TOG is not found.");
                    var togFound = await _oilVolumeCalculationRepository.GetTOGOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, tog.MeasuringPointId);
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({togFound.OilVolumeCalculation.Installation.Name}).");

                    var togFoundSame = await _oilVolumeCalculationRepository.GetTOGInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, tog.MeasuringPointId);
                    if (togFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(dor.MeasuringPointId) ?? throw new NotFoundException("Equipment DOR is not found.");
                    var dorFound = await _oilVolumeCalculationRepository.GetDOROtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, dor.MeasuringPointId);
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({dorFound.OilVolumeCalculation.Installation.Name}).");

                    var dorFoundSame = await _oilVolumeCalculationRepository.GetDORInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, dor.MeasuringPointId);
                    if (dorFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _mpointRepository.GetByIdAsync(drain.MeasuringPointId) ?? throw new NotFoundException("Equipment Drain is not found.");
                    var drainFound = await _oilVolumeCalculationRepository.GetDrainOtherInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, drain.MeasuringPointId);
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({drainFound.OilVolumeCalculation.Installation.Name}).");

                    var drainFoundSame = await _oilVolumeCalculationRepository.GetDrainInstallationByIdAsync(installationInDatabase.OilVolumeCalculation.Id, drain.MeasuringPointId);
                    if (drainFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            var oilCalculationInDatabase = await _oilVolumeCalculationRepository.GetOilVolumeCalculationById(installationInDatabase.OilVolumeCalculation.Id);
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (body.Sections.Any())
            {
                foreach (var section in body.Sections)
                {
                    var sectionFound = await _oilVolumeCalculationRepository.GetSectionByMeasuringPointIdAsync(section.MeasuringPointId);
                    sectionFound.IsActive = false;
                    sectionFound.DeletedAt = DateTime.UtcNow.AddHours(-3);

                    await _oilVolumeCalculationRepository.UpdateSection(sectionFound);
                }
            }

            if (body.TOGs.Any())
            {
                foreach (var tog in body.TOGs)
                {
                    var togFound = await _oilVolumeCalculationRepository.GetTOGByMeasuringPointIdAsync(tog.MeasuringPointId);
                    togFound.IsActive = false;
                    togFound.DeletedAt = DateTime.UtcNow.AddHours(-3);

                    await _oilVolumeCalculationRepository.UpdateTOG(togFound);
                }
            }

            if (body.Drains.Any())
            {
                foreach (var drain in body.Drains)
                {
                    var drainFound = await _oilVolumeCalculationRepository.GetDrainByMeasuringPointIdAsync(drain.MeasuringPointId);
                    drainFound.IsActive = false;
                    drainFound.DeletedAt = DateTime.UtcNow.AddHours(-3);

                    await _oilVolumeCalculationRepository.UpdateDrain(drainFound);
                }
            }

            if (body.DORs.Any())
            {
                foreach (var dor in body.DORs)
                {
                    var dorFound = await _oilVolumeCalculationRepository.GetDORByMeasuringPointIdAsync(dor.MeasuringPointId);
                    dorFound.IsActive = false;
                    dorFound.DeletedAt = DateTime.UtcNow.AddHours(-3);

                    await _oilVolumeCalculationRepository.UpdateDOR(dorFound);
                }
            }

            await _oilVolumeCalculationRepository.SaveChangesAsync();
        }
        private static void HasDuplicateIds(CreateOilVolumeCalculationViewModel body)
        {
            HashSet<Guid> idSet = new HashSet<Guid>();
            if (body.Sections != null)
            {
                foreach (var section in body.Sections)
                    idSet.Add(section.MeasuringPointId);
            }
            if (body.TOGs != null)
            {
                foreach (var tog in body.TOGs)
                    idSet.Add(tog.MeasuringPointId);
            }
            if (body.Drains != null)
            {
                foreach (var drain in body.Drains)
                    idSet.Add(drain.MeasuringPointId);
            }
            if (body.DORs != null)
            {
                foreach (var dor in body.DORs)
                    idSet.Add(dor.MeasuringPointId);
            }

            bool haRepetidos = idSet.Count != idSet.Distinct().Count();

            if (haRepetidos)
                throw new ConflictException("");
        }
    }
}
