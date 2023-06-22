using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Dtos;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.ViewModels;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.Http.Services
{
    public class FieldService
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;

        public FieldService(IMapper mapper, IFieldRepository fieldRepository, ISystemHistoryRepository systemRepository, IInstallationRepository installationRepository)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _systemHistoryRepository = systemRepository;
            _installationRepository = installationRepository;
        }

        public async Task<CreateUpdateFieldDTO> CreateField(CreateFieldViewModel body, User user)
        {
            var installationInDatabase = await _installationRepository
                .GetByIdAsync(body.InstallationId);

            if (installationInDatabase is null)
                throw new NotFoundException("Installation not found");

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

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = fieldId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

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
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException("Field not found");

            var fieldDTO = _mapper.Map<Field, FieldDTO>(field);
            return fieldDTO;
        }

        public async Task<CreateUpdateFieldDTO> UpdateField(Guid id, UpdateFieldViewModel body, User user)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null)
                throw new NotFoundException("Field not found");

            var beforeChangesField = _mapper.Map<FieldHistoryDTO>(field);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(field, body);

            if (updatedProperties.Any() is false && field.Installation?.Id == body.InstallationId)
                throw new BadRequestException("This field already has these values, try to update to other values.");

            if (body.InstallationId is not null)
            {
                var installationInDatabase = await _installationRepository.GetByIdAsync(body.InstallationId);

                if (installationInDatabase is null)
                    throw new NotFoundException("Installation not found");

                field.Installation = installationInDatabase;
                updatedProperties[nameof(FieldHistoryDTO.installationId)] = installationInDatabase.Id;
            }

            _fieldRepository.Update(field);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesField,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);

            return fieldDTO;
        }

        public async Task DeleteField(Guid id, User user)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null || field.IsActive is false)
                throw new NotFoundException("Field not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            field.IsActive = false;
            field.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = (DateTime)field.DeletedAt;
            currentData.deletedAt = field.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = field.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    field.IsActive,
                    field.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _fieldRepository.Update(field);

            await _fieldRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateFieldDTO> RestoreField(Guid id, User user)
        {
            var field = await _fieldRepository.GetByIdAsync(id);

            if (field is null || field.IsActive is true)
                throw new NotFoundException("Field not found or active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            field.IsActive = true;
            field.DeletedAt = null;

            var currentData = _mapper.Map<Field, FieldHistoryDTO>(field);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableFields,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = field.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = field.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    field.IsActive,
                    field.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _fieldRepository.Update(field);
            await _fieldRepository.SaveChangesAsync();

            var fieldDTO = _mapper.Map<Field, CreateUpdateFieldDTO>(field);
            return fieldDTO;
        }

        public async Task<List<SystemHistory>> GetFieldHistory(Guid id)
        {
            var fieldHistories = await _systemHistoryRepository.GetAll(id);

            if (fieldHistories is null)
                throw new NotFoundException("Field not found");

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