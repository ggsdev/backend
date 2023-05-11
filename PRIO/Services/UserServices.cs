using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.Models;

namespace PRIO.Services
{
    public class UserServices
    {
        private readonly DataContext _context;

        public UserServices(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

