﻿using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces
{
    public interface IUserOperationRepository
    {
        Task AddUserOperation(UserOperation userOperation);
        Task<List<UserOperation>> GetUserOperationsByUserId(Guid userId);
        Task<List<UserOperation>> GetUserOperationsByGroupId(Guid groupId);
        Task RemoveUserOperations(List<UserOperation> userOperations);
        void UpdateUserOperations(List<UserOperation> userOperations);
    }
}
