﻿using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Interfaces
{
    public interface IPIRepository
    {
        Task<List<Value>> GetValuesByDate(DateTime date);
        Task<List<Infra.EF.Models.Attribute>> GetTagsByWellName(string wellName, string wellOperatorName);
        Task AddTag(Infra.EF.Models.Attribute atr);
        Task<bool> AnyTag(string tagName);
        Task SaveChanges();
        Task<Element?> GetElementByParameter(string parameter);
        Task<WellsValues?> GetWellValuesById(Guid id);
    }
}
