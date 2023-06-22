﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Interfaces;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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

        public async Task<User?> GetUserWithGroupAndPermissionsAsync(Guid userId)
        {
            var userHasGroup = await _context.Users
               .Where(x => x.Id == userId)
               .Include(x => x.Group)
               .Include(x => x.UserPermissions)
               .FirstOrDefaultAsync();

            return userHasGroup;
        }
    }
}
