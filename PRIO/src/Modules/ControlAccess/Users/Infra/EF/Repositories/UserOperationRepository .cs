using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Dtos;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class UserOperationRepository : IUserOperationRepository
    {
        private readonly DataContext _context;
        private readonly UserOperationFactory _userOperationFactory;

        public UserOperationRepository(DataContext context, UserOperationFactory userOperationFactory)
        {
            _context = context;
            _userOperationFactory = userOperationFactory;
        }
        public async Task<UserOperation> CreateAndAddUserOperation(GroupOperation operation, UserPermission userPermission, Group group, UserGroupOperationDTO userGroupOperationDTO)
        {
            var userOperation = _userOperationFactory.CreateUserOperation(userGroupOperationDTO, userPermission, operation.GlobalOperation, group);
            await AddUserOperation(userOperation);
            return userOperation;
        }

        public async Task AddUserOperation(UserOperation userOperation)
        {
            await _context.AddAsync(userOperation);
        }

        public async Task<List<UserOperation>> GetUserOperationsByUserId(Guid userId)
        {
            return await _context.UserOperations
                .Include(x => x.UserPermission)
                .ThenInclude(x => x.User)
                .Where(x => x.UserPermission.User.Id == userId)
                .ToListAsync();
        }

        public async Task RemoveUserOperations(List<UserOperation> userOperations)
        {
            _context.RemoveRange(userOperations);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserOperation>> GetUserOperationsByGroupId(Guid groupId)
        {
            return await _context.UserOperations
                .Include(x => x.UserPermission)
                .Where(x => x.UserPermission.GroupId == groupId)
                .ToListAsync();
        }
        public async Task<UserOperation> GetUserOperationsByOperationNameMenuNameAndPermissionId(string? operationName, string? menuName, Guid userPermissionId)
        {
            return await _context
                        .UserOperations.Include(x => x.UserPermission)
                        .Where(x => x.OperationName == operationName)
                        .Where(x => x.UserPermission.MenuName == menuName)
                        .Where(x => x.UserPermission.User.Id == userPermissionId)
                        .FirstOrDefaultAsync();
        }
        public void UpdateUserOperations(List<UserOperation> userOperations)
        {
            _context.UserOperations.UpdateRange(userOperations);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
