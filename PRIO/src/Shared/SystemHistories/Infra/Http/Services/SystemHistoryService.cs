using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Shared.SystemHistories.Infra.Http.Services
{
    public class SystemHistoryService
    {
        private readonly IMapper _mapper;
        private readonly ISystemHistoryRepository _systemHistoryRepository;

        public SystemHistoryService(IMapper mapper, ISystemHistoryRepository systemHistoryRepository)
        {
            _mapper = mapper;
            _systemHistoryRepository = systemHistoryRepository;
        }
        public async Task Create<T, U>(string tableName, User user, Guid tableItemId, T objectCreated) where T : class where U : class
        {
            dynamic currentData = _mapper.Map<T, U>(objectCreated);
            var dateCurrent = DateTime.UtcNow;

            currentData.createdAt = dateCurrent;
            currentData.updatedAt = dateCurrent;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = tableItemId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);
        }

        public async Task<List<SystemHistory>> GetAll(Guid tableItemId)
        {
            return await _systemHistoryRepository.GetAll(tableItemId);
        }


        public async Task Update<T, U>(string tableName, User user, Dictionary<string, object> updatedProperties, Guid tableItemId, T objectUpdated, U objectBeforeChanges)
        where T : class where U : class
        {
            var firstHistory = await _systemHistoryRepository.GetFirst(tableItemId);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            dynamic currentData = _mapper.Map<T, U>(objectUpdated);

            var dateCurrent = DateTime.UtcNow;

            currentData.createdAt = dateCurrent;
            currentData.updatedAt = dateCurrent;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = tableItemId,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = objectBeforeChanges,
            };

            await _systemHistoryRepository.AddAsync(history);
        }

        public async Task Delete<T, U>(string tableName, User user, Dictionary<string, object> updatedProperties, Guid tableItemId, T objectDeleted)
           where T : class where U : class
        {
            var lastHistory = await _systemHistoryRepository.GetLast(tableItemId);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            dynamic currentData = _mapper.Map<T, U>(objectDeleted);

            var dateCurrent = DateTime.UtcNow;

            currentData.updatedAt = dateCurrent;
            currentData.deletedAt = dateCurrent;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = lastHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = tableItemId,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = changedFields
            };

            await _systemHistoryRepository.AddAsync(history);
        }

        public async Task Restore<T, U>(string tableName, User user, Dictionary<string, object> updatedProperties, Guid tableItemId, T objectRestored)
           where T : class where U : class
        {
            var lastHistory = await _systemHistoryRepository.GetLast(tableItemId);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            dynamic currentData = _mapper.Map<T, U>(objectRestored);

            currentData.updatedAt = DateTime.UtcNow;
            currentData.deletedAt = null;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = lastHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = tableItemId,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = changedFields
            };

            await _systemHistoryRepository.AddAsync(history);
        }


        public async Task Import<T, U>(string tableName, User user, Guid tableItemId, T objectCreated) where T : class where U : class
        {

            dynamic currentData = _mapper.Map<T, U>(objectCreated);
            var dateCurrent = DateTime.UtcNow;

            currentData.createdAt = dateCurrent;
            currentData.updatedAt = dateCurrent;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.Import,
                CreatedBy = user?.Id,
                TableItemId = tableItemId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);
        }

        public async Task ImportUpdate<T, U>(string tableName, User user, Dictionary<string, object> updatedProperties, Guid tableItemId, T objectUpdated, U objectBeforeChanges)
        where T : class where U : class
        {
            var firstHistory = await _systemHistoryRepository.GetFirst(tableItemId);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            dynamic currentData = _mapper.Map<T, U>(objectUpdated);

            var dateCurrent = DateTime.UtcNow;

            currentData.createdAt = dateCurrent;
            currentData.updatedAt = dateCurrent;

            var history = new SystemHistory
            {
                Table = tableName,
                TypeOperation = HistoryColumns.ImportUpdate,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = tableItemId,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = objectBeforeChanges,
            };

            await _systemHistoryRepository.AddAsync(history);

        }

    }
}
