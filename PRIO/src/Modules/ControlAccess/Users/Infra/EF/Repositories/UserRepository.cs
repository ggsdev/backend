using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Factories;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserFactory _userFactory;

        public UserRepository(DataContext context, UserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }
        public async Task<User> CreateAndAddUser(CreateUserViewModel body, string treatedUsername)
        {
            var user = _userFactory.CreateUser(body, treatedUsername);
            await AddUser(user);
            return user;
        }
        public async Task AddUser(User user)
        {
            await _context.AddAsync(user);
        }
        public async Task<List<User>> GetAdminUsers()
        {
            return await _context.Users
                .Include(x => x.Group)
                .Where(x => x.Group.Name.ToLower() == "master")
                .ToListAsync();
        }

        public async Task AddUserPermission(UserPermission userPermission)
        {
            await _context.AddAsync(userPermission);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.Group).ToListAsync();
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
                .Include(x => x.InstallationsAccess)
                    .ThenInclude(x => x.Installation)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user!;
        }
        public async Task ValidateMenu(InsertUserPermissionViewModel body)
        {
            await MenuErrors.ValidateMenu(_context, body);
        }
        public void UpdateUser(User userHasGroup)
        {
            _context.Update(userHasGroup);
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
