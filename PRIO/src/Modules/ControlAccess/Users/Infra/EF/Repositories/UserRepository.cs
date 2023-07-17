using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task CreateUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task AddUserPermission(UserPermission userPermission)
        {
            await _context.AddAsync(userPermission);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User?> GetUsersByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Email == email || x.Username == email);
            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync((x) => x.Username.ToLower().Trim() == username.ToLower().Trim());
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _context.Users
                .Include(x => x.Group)
                .Include(x => x.UserPermissions)
                .ThenInclude(x => x.UserOperation)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user!;
        }
        public async Task ValidateMenu(InsertUserPermissionViewModel body)
        {
            await MenuErrors.ValidateMenu(_context, body);
        }
        public async Task UpdateUser(User userHasGroup)
        {
            _context.Update(userHasGroup);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUsers(List<User> users)
        {
            _context.UpdateRange(users);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetUserWithGroupAndPermissionsAsync(Guid userId)
        {
            var userHasGroup = await _context.Users
               .Where(x => x.Id == userId)
               .Include(x => x.Group)
               .Include(x => x.UserPermissions)
               .FirstOrDefaultAsync();

            return userHasGroup;
        }
        public async Task<List<User>> GetUsersByLastGroupId(Guid groupId)
        {
            return await _context.Users
                .Where(x => x.LastGroupId == groupId)
                .ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
