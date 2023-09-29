using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Interfaces;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Equipments.Interfaces;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Services
{
    public class InstallationService
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IWellRepository _wellRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IMeasuringPointRepository _measuringPointRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IOilVolumeCalculationRepository _oilVolumeCalculationRepository;
        private readonly IGasVolumeCalculationRepository _gasVolumeCalculationRepository;
        private readonly IWellEventRepository _eventWellRepository;
        private readonly IProductionRepository _productionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInstallationsAccessRepository _installationsAccessRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableInstallations;

        public InstallationService(IMapper mapper, IInstallationRepository installationRepository, IClusterRepository clusterRepository, SystemHistoryService systemHistoryService, IFieldRepository fieldRepository, IZoneRepository zoneRepository, IWellRepository wellRepository, IReservoirRepository reservoirRepository, ICompletionRepository completionRepository, IMeasuringPointRepository measuringPointRepository, IEquipmentRepository equipmentRepository, IOilVolumeCalculationRepository oilVolumeCalculationRepository, IGasVolumeCalculationRepository gasVolumeCalculationRepository, IWellEventRepository wellEventRepository, IProductionRepository productionRepository, IUserRepository userRepository, IInstallationsAccessRepository installationsAccessRepository)
        {
            _mapper = mapper;
            _clusterRepository = clusterRepository;
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _zoneRepository = zoneRepository;
            _reservoirRepository = reservoirRepository;
            _wellRepository = wellRepository;
            _measuringPointRepository = measuringPointRepository;
            _oilVolumeCalculationRepository = oilVolumeCalculationRepository;
            _equipmentRepository = equipmentRepository;
            _completionRepository = completionRepository;
            _systemHistoryService = systemHistoryService;
            _gasVolumeCalculationRepository = gasVolumeCalculationRepository;
            _eventWellRepository = wellEventRepository;
            _productionRepository = productionRepository;
            _userRepository = userRepository;
            _installationsAccessRepository = installationsAccessRepository;
        }

        public async Task<CreateUpdateInstallationDTO> CreateInstallation(CreateInstallationViewModel body, User user)
        {
            var installationExistingCode = await _installationRepository
                .GetByCod(body.CodInstallationAnp);

            if (installationExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Installation>());

            var clusterInDatabase = await _clusterRepository
               .GetClusterByIdAsync(body.ClusterId);

            if (clusterInDatabase is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (clusterInDatabase.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Cluster>());

            var installationSameName = await _installationRepository.GetByNameAsync(body.Name);
            if (installationSameName is not null)
                throw new ConflictException($"Já existe uma instalação com o nome: {body.Name}");

            decimal? tratedNumber = null;
            if (body.GasSafetyBurnVolume is not null)
            {
                var trated = body.GasSafetyBurnVolume.ToString();

                if (trated.Contains(","))
                {
                    string[] partes = trated.Split(',');
                    string? tratedDecimal = null;
                    if (partes[1].Length > 4)
                    {
                        tratedDecimal = partes[1].Substring(0, 4);
                    }
                    tratedDecimal = partes[1];

                    if (partes[0].Length > 6)
                        throw new ConflictException("Formato não aceito. (000000,0000)");

                    string? resultado = partes[0] + "," + tratedDecimal;
                    tratedNumber = decimal.Parse(resultado);
                }
                else
                {
                    if (trated.Length > 6)
                        throw new ConflictException("Formato não aceito. (000000,0000)");

                    string? resultado = trated + "," + "0000";
                    tratedNumber = decimal.Parse(resultado);

                }

            }

            var installationId = Guid.NewGuid();

            var installation = new Installation
            {
                Id = installationId,
                Name = body.Name,
                Description = body.Description,
                UepCod = body.UepCod,
                UepName = body.UepName,
                CodInstallationAnp = body.CodInstallationAnp,
                GasSafetyBurnVolume = body.GasSafetyBurnVolume,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                IsProcessingUnit = body.UepCod == body.CodInstallationAnp
            };
            await _installationRepository.AddAsync(installation);

            if (installation.IsProcessingUnit == true)
            {
                await _oilVolumeCalculationRepository.AddOilVolumeCalculationAsync(installation);
                await _gasVolumeCalculationRepository.AddGasVolumeCalculationAsync(installation);
            }

            var usersMaster = await _userRepository.GetAdminUsers();
            foreach (var userMaster in usersMaster)
            {
                var InstallataionAccess = new InstallationsAccess
                {
                    Id = Guid.NewGuid(),
                    Installation = installation,
                    User = userMaster
                };
                await _installationsAccessRepository.AddInstallationsAccess(InstallataionAccess);
            }

            await _systemHistoryService
                .Create<Installation, InstallationHistoryDTO>(_tableName, user, installationId, installation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }
        //public async Task<List<FRFieldDTO>> ApplyFR(CreateFRsFieldsViewModel body, User user)
        //{
        //    var installation = await _installationRepository.GetByIdAsync(body.InstallationId);

        //    if (installation is null)
        //        throw new NotFoundException("Instalação não encontrada.");

        //    if (installation.IsProcessingUnit == false)
        //        throw new ConflictException("Instalação não é uma unidade de processamento.");

        //    var installationsWithFields = await _installationRepository.GetByUEPWithFieldsCod(installation.UepCod);

        //    foreach (var installationUEP in installationsWithFields)
        //    {
        //        foreach (var field in installationUEP.Fields)
        //        {
        //            if (body.Fields.Any(x => x.FieldId == field.Id))
        //            {
        //            }
        //            else
        //            {
        //                throw new ConflictException("Não encontrado");
        //            }
        //        }
        //    }

        //    foreach (var item in body.Fields)
        //    {
        //        var field = await _fieldRepository.GetByIdAsync(item.FieldId);
        //        if (field is null)
        //            throw new NotFoundException("Campo não encontrado");
        //    }

        //    if (body.isApplicableFROil is true)
        //    {
        //        decimal? sumOil = 0;
        //        foreach (var item in body.Fields)
        //        {
        //            if (item.OilFR is null)
        //                throw new ConflictException("Fator de rateio do campo não encontrado.");

        //            sumOil += item.OilFR;
        //        }
        //        if (sumOil != 1)
        //            throw new ConflictException("Óleo: Soma dos fatores deve ser 100%.");
        //    }
        //    else
        //    {
        //        foreach (var item in body.Fields)
        //        {

        //            if (item.OilFR is not null)
        //                throw new ConflictException("Óleo: Fator de rateio não se aplica.");
        //        }
        //    }

        //    if (body.isApplicableFRGas is true)
        //    {
        //        decimal? sumGas = 0;
        //        foreach (var item in body.Fields)
        //        {
        //            if (item.GasFR is null)
        //                throw new ConflictException("Fator de rateio do campo não encontrado.");

        //            sumGas += item.GasFR;
        //        }
        //        if (sumGas != 1)
        //            throw new ConflictException("Gás: Soma dos fatores deve ser 100%.");
        //    }
        //    else
        //    {
        //        foreach (var item in body.Fields)
        //        {
        //            if (item.GasFR is not null)
        //                throw new ConflictException("Gás: Fator de rateio não se aplica.");
        //        }
        //    }

        //    if (body.isApplicableFRWater is true)
        //    {
        //        decimal? sumWater = 0;
        //        foreach (var item in body.Fields)
        //        {
        //            if (item.WaterFR is null)
        //                throw new ConflictException("Fator de rateio do campo não encontrado.");

        //            sumWater += item.WaterFR;
        //        }
        //        if (sumWater != 1)
        //            throw new ConflictException("Água: Soma dos fatores deve ser 100%.");
        //    }
        //    else
        //    {
        //        foreach (var item in body.Fields)
        //        {
        //            if (item.WaterFR is not null)
        //                throw new ConflictException("Água: Fator de rateio não se aplica.");
        //        }
        //    }

        //    foreach (var installationUEP in installationsWithFields)
        //    {
        //        foreach (var field in installationUEP.Fields)
        //        {
        //            foreach (var fr in field.FRs)
        //            {
        //                fr.IsActive = false;
        //            }
        //        }
        //    }

        //    foreach (var item in body.Fields)
        //    {
        //        var field = await _fieldRepository.GetByIdAsync(item.FieldId);
        //        var createOilFr = new FieldFR
        //        {
        //            Id = Guid.NewGuid(),
        //            Field = field,
        //            FROil = item.OilFR,
        //            FRGas = item.GasFR,
        //            FRWater = item.WaterFR,
        //            IsActive = true,
        //        };
        //        await _installationRepository.AddFRAsync(createOilFr);
        //    }
        //    await _installationRepository.SaveChangesAsync();

        //    var frs = await _installationRepository.GetFRsByUEPAsync(installation.UepCod);

        //    var frsDTO = _mapper.Map<List<FieldFR>, List<FRFieldDTO>>(frs);

        //    return frsDTO;
        //}
        public async Task<List<FRFieldDTO>> GetFRsField(Guid installationId)
        {
            var installation = await _installationRepository.GetByIdAsync(installationId);

            if (installation is null)
                throw new NotFoundException("Instalação não encontrada.");

            if (installation.IsProcessingUnit == false)
                throw new ConflictException("Instalação não é uma unidade de processamento.");

            var frs = await _installationRepository.GetFRsByUEPAsync(installation.UepCod);

            var frsDTO = _mapper.Map<List<FieldFR>, List<FRFieldDTO>>(frs);

            return frsDTO;

        }
        public async Task<List<InstallationDTO>> GetInstallations(User user)
        {
            var installations = await _installationRepository
                .GetAsync(user);

            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return installationsDTO;
        }
        public async Task<List<InstallationDTO>> GetUEPs()
        {
            var installations = await _installationRepository
                .GetUEPsAsync();

            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return installationsDTO;
        }
        public async Task<List<InstallationDTO>> GetUEPsCreate(string table)
        {
            var installations = await _installationRepository
                .GetUEPsCreateAsync(table);

            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return installationsDTO;
        }
        public async Task<InstallationWithFieldsEquipmentsDTO> GetInstallationById(Guid id)
        {
            var installation = await _installationRepository.GetByIdWithFieldsMeasuringPointsAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var installationDTO = _mapper.Map<Installation, InstallationWithFieldsEquipmentsDTO>(installation);

            return installationDTO;
        }
        public async Task<CreateUpdateInstallationDTO> UpdateInstallation(UpdateInstallationViewModel body, Guid id, User user)
        {
            var installation = await _installationRepository
                .GetByIdWithFieldsMeasuringPointsAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Installation>());

            if (installation.Fields is not null)
                if (installation.Fields.Count > 0)
                    if (body.CodInstallationAnp is not null)
                        if (body.CodInstallationAnp != installation.CodInstallationAnp)
                            throw new ConflictException(ErrorMessages.CodCantBeUpdated<Installation>());

            if (installation.Cluster is not null)
                if (installation.Fields is not null)
                    if (body.ClusterId is not null)
                        if (body.ClusterId != installation.Cluster.Id)
                            throw new ConflictException("Relacionamento não pode ser alterado.");

            if (body.CodInstallationAnp is not null)
            {
                var installationInDatabase = await _installationRepository.GetByCod(body.CodInstallationAnp);
                if (installationInDatabase is not null && installationInDatabase.Id != installation.Id)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Installation>());
            }

            if (body.Name is not null)
            {
                var installationInDatabase = await _installationRepository.GetByNameAsync(body.Name);
                if (installationInDatabase is not null && installationInDatabase.Id != installation.Id)
                    throw new ConflictException($"Já existe uma instalação com esse nome: {body.Name}");
            }

            var beforeChangesInstallation = _mapper.Map<InstallationHistoryDTO>(installation);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(installation, body);

            if (updatedProperties.Any() is false && (body.ClusterId is null || body.ClusterId == installation.Cluster?.Id))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Installation>());

            installation.IsProcessingUnit = installation.UepCod == installation.CodInstallationAnp;

            if (installation.IsProcessingUnit && installation.GasVolumeCalculation is null && installation.OilVolumeCalculation is null)
            {
                await _oilVolumeCalculationRepository.AddOilVolumeCalculationAsync(installation);
                await _gasVolumeCalculationRepository.AddGasVolumeCalculationAsync(installation);
            }


            if (body.ClusterId is not null && installation.Cluster?.Id != body.ClusterId)
            {
                var clusterInDatabase = await _clusterRepository.GetClusterByIdAsync(body.ClusterId);

                if (clusterInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

                installation.Cluster = clusterInDatabase;
                updatedProperties[nameof(InstallationHistoryDTO.clusterId)] = clusterInDatabase.Id;
            }

            _installationRepository.Update(installation);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, installation.Id, installation, beforeChangesInstallation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }
        public async Task DeleteInstallation(Guid id, User user, string StatusDate)
        {
            DateTime date;
            if (StatusDate is null)
            {
                throw new ConflictException("Data da inativação não informada");
            }
            else
            {
                var checkDate = DateTime.TryParse(StatusDate, out DateTime day);
                if (checkDate is false)
                    throw new ConflictException("Data não é válida.");

                var dateToday = DateTime.UtcNow.AddHours(-3);
                if (dateToday < day)
                    throw new NotFoundException("Data fornecida é maior que a data atual.");

                date = day;
            }

            var production = await _productionRepository.GetCleanByDate(date);
            if (production is not null)
                throw new ConflictException("Existe uma produção para essa data.");

            var installation = await _installationRepository
                .GetInstallationAndChildren(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Installation>());

            var installationpropertiesUpdated = new
            {
                IsActive = false,
                InactivatedAt = date,
                DeletedAt = DateTime.UtcNow.AddHours(-3),
            };

            if (installation.MeasuringPoints is not null)
                foreach (var mpoint in installation.MeasuringPoints)
                {
                    if (mpoint.IsActive is true)
                    {
                        var mpointPropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var mpointUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(mpoint, mpointPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<MeasuringPoint, MeasuringPointHistoryDTO>(HistoryColumns.TableInstallations, user, mpointUpdatedProperties, mpoint.Id, mpoint);

                        _measuringPointRepository.Delete(mpoint);
                    }

                    if (mpoint.MeasuringEquipments is not null)
                        foreach (var mEquipment in mpoint.MeasuringEquipments)
                        {
                            if (mEquipment.IsActive is true)
                            {
                                var mEquipmentPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var mEquipmentUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(mEquipment, mEquipmentPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<MeasuringEquipment, MeasuringEquipmentHistoryDTO>(HistoryColumns.TableInstallations, user, mEquipmentUpdatedProperties, mEquipment.Id, mEquipment);

                                _equipmentRepository.Delete(mEquipment);
                            }
                        }
                }

            if (installation.Fields is not null)
                foreach (var field in installation.Fields)
                {
                    if (field.IsActive is true)
                    {
                        var fieldPropertiesToUpdate = new
                        {
                            IsActive = false,
                            InactivatedAt = date,
                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        var fieldUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(field, fieldPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Field, FieldHistoryDTO>(HistoryColumns.TableInstallations, user, fieldUpdatedProperties, field.Id, field);

                        _installationRepository.Delete(installation);
                    }

                    if (field.Zones is not null)
                        foreach (var zone in field.Zones)
                        {
                            if (zone.IsActive is true)
                            {
                                var zonePropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    InactivatedAt = date,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var zoneUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(zone, zonePropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Zone, ZoneHistoryDTO>(HistoryColumns.TableZones, user, zoneUpdatedProperties, zone.Id, zone);

                                _zoneRepository.Delete(zone);
                            }

                            if (zone.Reservoirs is not null)
                                foreach (var reservoir in zone.Reservoirs)
                                {
                                    if (reservoir.IsActive is true)
                                    {
                                        var reservoirPropertiesToUpdate = new
                                        {
                                            IsActive = false,
                                            InactivatedAt = date,
                                            DeletedAt = DateTime.UtcNow.AddHours(-3),
                                        };

                                        var reservoirUpdatedProperties = UpdateFields
                                        .CompareUpdateReturnOnlyUpdated(reservoir, reservoirPropertiesToUpdate);

                                        await _systemHistoryService
                                            .Delete<Reservoir, ReservoirHistoryDTO>(HistoryColumns.TableReservoirs, user, reservoirUpdatedProperties, reservoir.Id, reservoir);

                                        _reservoirRepository.Delete(reservoir);

                                    }

                                    if (reservoir.Completions is not null)
                                        foreach (var completion in reservoir.Completions)
                                        {
                                            if (completion.IsActive is true)
                                            {
                                                var completionPropertiesToUpdate = new
                                                {
                                                    IsActive = false,
                                                    InactivatedAt = date,
                                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                                };

                                                var completionUpdatedProperties = UpdateFields
                                                .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                                await _systemHistoryService
                                                    .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                                _completionRepository.Delete(completion);
                                            }
                                        }
                                }
                        }

                    if (field.Wells is not null)
                        foreach (var well in field.Wells)
                        {
                            if (well.IsActive is true)
                            {
                                var wellPropertiesToUpdate = new
                                {
                                    IsActive = false,
                                    InactivatedAt = date,
                                    DeletedAt = DateTime.UtcNow.AddHours(-3),
                                };

                                var wellUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(well, wellPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellUpdatedProperties, well.Id, well);

                                //criação evento de fechamento
                                var lastEventOfAll = well.WellEvents
                                   .Where(we => we.EndDate == null)
                                   .LastOrDefault();

                                if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "A")
                                {
                                    var lastEventOfTypeClosing = well.WellEvents
                                    .OrderBy(e => e.StartDate)
                                    .LastOrDefault(x => x.EventStatus == "F");

                                    int lastCode;
                                    var codeSequencial = string.Empty;
                                    if (lastEventOfTypeClosing is not null && int.TryParse(lastEventOfTypeClosing.IdAutoGenerated.Split(" ")[0].Substring(3), out lastCode))
                                    {
                                        lastCode++;
                                        codeSequencial = lastCode.ToString("0000");
                                    }

                                    if (lastEventOfTypeClosing is null)
                                        codeSequencial = "0001";

                                    var wellEvent = new WellEvent
                                    {
                                        Id = Guid.NewGuid(),
                                        StartDate = date,
                                        IdAutoGenerated = $"{well.Field?.Name?.Substring(0, 3)}{codeSequencial} {well.Name}",
                                        Well = well,
                                        EventStatus = "F",
                                        StateANP = "4",
                                        StatusANP = "Fechado",
                                        CreatedBy = user

                                    };
                                    await _eventWellRepository.Add(wellEvent);

                                    var newEventReason = new EventReason
                                    {
                                        Id = Guid.NewGuid(),
                                        SystemRelated = "Inativo",
                                        StartDate = date,
                                        WellEvent = wellEvent,
                                        CreatedBy = user
                                    };

                                    await _eventWellRepository.AddReasonClosedEvent(newEventReason);

                                    lastEventOfAll.Interval = (date - lastEventOfAll.StartDate).TotalHours;
                                    lastEventOfAll.EndDate = date;

                                    _eventWellRepository.Update(lastEventOfAll);
                                }
                                else if (lastEventOfAll is not null && lastEventOfAll.EventStatus.ToUpper() == "F" && lastEventOfAll.EndDate is null)
                                {
                                    var eventReason = lastEventOfAll.EventReasons.OrderBy(x => x.StartDate).LastOrDefault();
                                    if (eventReason.StartDate >= date)
                                        throw new ConflictException("Data da inativação não pode ser menor que data do último evento.");

                                    if (eventReason.StartDate < date && eventReason.EndDate is null)
                                    {
                                        var dif = (date - lastEventOfAll.StartDate).TotalHours / 24;
                                        eventReason.EndDate = eventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10);

                                        var FirstresultIntervalTimeSpan = (eventReason.StartDate.Date.AddDays(1).AddMilliseconds(-10) - eventReason.StartDate).TotalHours;
                                        int FirstintervalHours = (int)FirstresultIntervalTimeSpan;
                                        var FirstintervalMinutesDecimal = (FirstresultIntervalTimeSpan - FirstintervalHours) * 60;
                                        int FirstintervalMinutes = (int)FirstintervalMinutesDecimal;
                                        var FirstintervalSecondsDecimal = (FirstintervalMinutesDecimal - FirstintervalMinutes) * 60;
                                        int FirstintervalSeconds = (int)FirstintervalSecondsDecimal;
                                        string FirstReasonFormattedHours;
                                        string firstFormattedMinutes = FirstintervalMinutes < 10 ? $"0{FirstintervalMinutes}" : FirstintervalMinutes.ToString();
                                        string firstFormattedSecond = FirstintervalSeconds < 10 ? $"0{FirstintervalSeconds}" : FirstintervalSeconds.ToString();
                                        if (FirstintervalHours >= 1000)
                                        {
                                            int digitCount = (int)Math.Floor(Math.Log10(FirstintervalHours)) + 1;
                                            FirstReasonFormattedHours = FirstintervalHours.ToString(new string('0', digitCount));
                                        }
                                        else
                                        {
                                            FirstReasonFormattedHours = FirstintervalHours.ToString("00");
                                        }
                                        var FirstReasonFormattedTime = $"{FirstReasonFormattedHours}:{firstFormattedMinutes}:{firstFormattedSecond}";
                                        eventReason.Interval = FirstReasonFormattedTime;

                                        DateTime refStartDate = eventReason.StartDate.Date.AddDays(1);
                                        DateTime refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);

                                        var resultIntervalTimeSpan = (refStartEnd - refStartDate).TotalHours;
                                        int intervalHours = (int)resultIntervalTimeSpan;
                                        var intervalMinutesDecimal = (resultIntervalTimeSpan - intervalHours) * 60;
                                        int intervalMinutes = (int)intervalMinutesDecimal;
                                        var intervalSecondsDecimal = (intervalMinutesDecimal - intervalMinutes) * 60;
                                        int intervalSeconds = (int)intervalSecondsDecimal;

                                        for (int j = 0; j < dif; j++)
                                        {
                                            var newEventReason = new EventReason
                                            {
                                                Id = Guid.NewGuid(),
                                                StartDate = refStartDate,
                                                WellEvent = lastEventOfAll,
                                                SystemRelated = eventReason.SystemRelated,
                                                CreatedBy = user
                                            };
                                            if (j == 0)
                                            {
                                                if (date.Date == eventReason.StartDate.Date)
                                                {
                                                    Console.WriteLine("oi");
                                                    eventReason.EndDate = date;
                                                    var Interval = FormatTimeInterval(date, eventReason);
                                                    eventReason.Interval = Interval;

                                                    newEventReason.StartDate = date;
                                                    newEventReason.SystemRelated = "Inativo";
                                                    await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                                                    break;
                                                }
                                            }
                                            if (date.Date == refStartDate)
                                            {
                                                var newEventReason2 = new EventReason
                                                {
                                                    Id = Guid.NewGuid(),
                                                    SystemRelated = eventReason.SystemRelated,
                                                    Comment = eventReason.Comment,
                                                    WellEvent = lastEventOfAll,
                                                    StartDate = refStartDate,
                                                    EndDate = date,
                                                    IsActive = true,
                                                    IsJobGenerated = false,
                                                    CreatedBy = user
                                                };
                                                var Interval = FormatTimeInterval(date, newEventReason2);
                                                newEventReason2.Interval = Interval;

                                                newEventReason.EndDate = null;
                                                newEventReason.StartDate = date;
                                                newEventReason.SystemRelated = "Inativo";

                                                await _eventWellRepository.AddReasonClosedEvent(newEventReason2);
                                                await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                                                break;
                                            }
                                            else
                                            {
                                                newEventReason.EndDate = refStartEnd;
                                                string ReasonFormattedMinutes = intervalMinutes < 10 ? $"0{intervalMinutes}" : intervalMinutes.ToString();
                                                string ReasonFormattedSecond = intervalSeconds < 10 ? $"0{intervalSeconds}" : intervalSeconds.ToString();
                                                string ReasonFormattedHours;
                                                if (intervalHours >= 1000)
                                                {
                                                    int digitCount = (int)Math.Floor(Math.Log10(intervalHours)) + 1;
                                                    ReasonFormattedHours = intervalHours.ToString(new string('0', digitCount));
                                                }
                                                else
                                                {
                                                    ReasonFormattedHours = intervalHours.ToString("00");
                                                }
                                                var reasonFormattedTime = $"{ReasonFormattedHours}:{ReasonFormattedMinutes}:{ReasonFormattedSecond}";
                                                newEventReason.Interval = reasonFormattedTime;
                                                refStartDate = newEventReason.StartDate.AddDays(1);
                                                refStartEnd = refStartDate.AddDays(1).AddMilliseconds(-10);
                                            }

                                            await _eventWellRepository.AddReasonClosedEvent(newEventReason);
                                        }
                                    }

                                    _wellRepository.Delete(well);
                                }

                                if (well.Completions is not null)
                                    foreach (var completion in well.Completions)
                                    {
                                        if (completion.IsActive is true)
                                        {

                                            var completionPropertiesToUpdate = new
                                            {
                                                IsActive = false,
                                                InactivatedAt = date,
                                                DeletedAt = DateTime.UtcNow.AddHours(-3),
                                            };

                                            var completionUpdatedProperties = UpdateFields
                                            .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                            await _systemHistoryService
                                                .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                            _completionRepository.Delete(completion);
                                        }
                                    }
                            }
                        }
                    var updatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(installation, installationpropertiesUpdated);

                    await _systemHistoryService
                        .Delete<Installation, InstallationHistoryDTO>(_tableName, user, updatedProperties, installation.Id, installation);

                    _installationRepository.Update(installation);

                }
            await _installationRepository.SaveChangesAsync();

        }
        public async Task<CreateUpdateInstallationDTO> RestoreInstallation(Guid id, User user)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Installation>());

            if (installation.Cluster is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            if (installation.Cluster.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Cluster>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(installation, propertiesUpdated);

            await _systemHistoryService
                .Restore<Installation, InstallationHistoryDTO>(_tableName, user, updatedProperties, installation.Id, installation);

            _installationRepository.Update(installation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }
        private static string FormatTimeInterval(DateTime dateNow, EventReason lastEventReason)
        {
            var resultTimeSpan = (dateNow - lastEventReason.StartDate).TotalHours;

            int hours = (int)resultTimeSpan;
            var minutesDecimal = (resultTimeSpan - hours) * 60;
            int minutes = (int)minutesDecimal;
            var secondsDecimal = (minutesDecimal - minutes) * 60;
            int seconds = (int)secondsDecimal;
            string formattedMinutes = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string formattedSecond = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            string formattedHours;
            if (hours >= 1000)
            {
                int digitCount = (int)Math.Floor(Math.Log10(hours)) + 1;
                formattedHours = hours.ToString(new string('0', digitCount));
            }
            else
            {
                formattedHours = hours.ToString("00");
            }

            return $"{formattedHours}:{formattedMinutes}:{formattedSecond}";
        }
        public async Task<List<SystemHistory>> GetInstallationHistory(Guid id)
        {
            var installationHistories = await _systemHistoryService
                .GetAll(id) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            foreach (var history in installationHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return installationHistories;
        }
    }
}

