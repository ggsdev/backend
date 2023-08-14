using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Zones.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Zones.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services
{
    public class FieldService
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableFields;
        private readonly IZoneRepository _zoneRepository;
        private readonly IReservoirRepository _reservoirRepository;
        private readonly IWellRepository _wellRepository;
        private readonly ICompletionRepository _completionRepository;

        public FieldService(IMapper mapper, IFieldRepository fieldRepository, SystemHistoryService systemHistoryService, IInstallationRepository installationRepository, IZoneRepository zoneRepository, IWellRepository wellRepository, ICompletionRepository completionRepository, IReservoirRepository reservoirRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _installationRepository = installationRepository;
            _systemHistoryService = systemHistoryService;
            _zoneRepository = zoneRepository;
            _wellRepository = wellRepository;
            _completionRepository = completionRepository;
            _reservoirRepository = reservoirRepository;
        }

        public async Task<CreateUpdateFieldDTO> CreateField(CreateFieldViewModel body, User user)
        {
            var fieldExistingCode = await _fieldRepository.GetByCod(body.CodField);
            if (fieldExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Field>());

            var installationInDatabase = await _installationRepository
                .GetByIdAsync(body.InstallationId) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installationInDatabase.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Installation>());

            var fieldSameName = await _fieldRepository.GetByNameAsync(body.Name);
            if (fieldSameName is not null)
                throw new ConflictException($"Já existe um campo com o nome: {body.Name}.");

            var fieldId = Guid.NewGuid();

            var field = new Field
            {
                Id = fieldId,
                Name = body.Name,
                User = user,
                Description = body.Description,
                Basin = body.Basin,
                Location = body.Location,
                State = body.State,
                CodField = body.CodField,
                Installation = installationInDatabase,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _fieldRepository.AddAsync(field);

            await _systemHistoryService
                .Create<Field, FieldHistoryDTO>(_tableName, user, fieldId, field);

            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return fieldDTO;
        }

        public async Task<List<FieldDTO>> GetFields()
        {
            var fields = await _fieldRepository.GetAsync();

            var fieldsDTO = _mapper.Map<List<Field>, List<FieldDTO>>(fields);
            return fieldsDTO;
        }

        public async Task<FieldDTO> GetFieldById(Guid id)
        {
            var field = await _fieldRepository
                .GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return fieldDTO;
        }

        public async Task<CreateUpdateFieldDTO> UpdateField(Guid id, UpdateFieldViewModel body, User user)
        {
            var field = await _fieldRepository.GetByIdWithWellsAndZonesAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Field>());

            if (field.Wells.Count > 0 || field.Zones.Count > 0)
                if (body.CodField is not null)
                    if (body.CodField != field.CodField)
                        throw new ConflictException(ErrorMessages.CodCantBeUpdated<Field>());

            if (field.Installation is not null)
                if (field.Wells is not null || field.Zones is not null)
                    if (body.InstallationId is not null)
                        if (body.InstallationId != field.Installation.Id)
                            throw new ConflictException("Relacionamento não pode ser alterado.");

            if (body.CodField is not null)
            {
                var fieldInDatabase = await _fieldRepository.GetByCod(body.CodField);
                if (fieldInDatabase is not null)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Field>());
            }


            var beforeChangesField = _mapper.Map<FieldHistoryDTO>(field);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(field, body);

            if (updatedProperties.Any() is false && (body.InstallationId is null || field.Installation?.Id == body.InstallationId))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Installation>());

            if (body.InstallationId is not null && field.Installation?.Id != body.InstallationId)
            {
                var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId);

                if (installationInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Installation>());

                field.Installation = installationInDatabase;
                updatedProperties[nameof(FieldHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _fieldRepository.Update(field);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, field.Id, field, beforeChangesField);

            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);

            return fieldDTO;
        }

        public async Task DeleteField(Guid id, User user)
        {
            var field = await _fieldRepository.GetFieldAndChildren(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Field>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(field, propertiesUpdated);

            _fieldRepository.Update(field);

            await _systemHistoryService
                .Delete<Field, FieldHistoryDTO>(_tableName, user, updatedProperties, field.Id, field);

            if (field.Zones is not null)
                foreach (var zone in field.Zones)
                {
                    if (zone.IsActive is true)
                    {
                        var zonePropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow,
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
                                    DeletedAt = DateTime.UtcNow,
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
                                            DeletedAt = DateTime.UtcNow,
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
                            DeletedAt = DateTime.UtcNow,
                        };

                        var wellUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(well, wellPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Well, WellHistoryDTO>(HistoryColumns.TableWells, user, wellUpdatedProperties, well.Id, well);

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
                                    DeletedAt = DateTime.UtcNow,
                                };

                                var completionUpdatedProperties = UpdateFields
                                .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                                await _systemHistoryService
                                    .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableCompletions, user, completionUpdatedProperties, completion.Id, completion);

                                _completionRepository.Delete(completion);

                            }
                        }
                }

            await _fieldRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateFieldDTO> RestoreField(Guid id, User user)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Field>());

            if (field.Installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (field.Installation.IsActive is false)
                throw new ConflictException(ErrorMessages.Inactive<Installation>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(field, propertiesUpdated);

            await _systemHistoryService
                .Restore<Field, FieldHistoryDTO>(_tableName, user, updatedProperties, field.Id, field);

            _fieldRepository.Update(field);
            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return fieldDTO;
        }

        public async Task<List<SystemHistory>> GetFieldHistory(Guid id)
        {
            var fieldHistories = await _systemHistoryService
                .GetAll(id);

            if (fieldHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            foreach (var history in fieldHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return fieldHistories;
        }
    }
}