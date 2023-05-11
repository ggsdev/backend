using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels;

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
            return await _context.Users
                .Include(x => x.Units)
                .Include(x => x.Clusters)
                .Include(x => x.Fields)
                .Include(x => x.Installations)
                .Include(x => x.Reservoirs)
                .Include(x => x.Completions)
                .Include(x => x.Wells)
                .Include(x => x.Session)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UserDTO>> RetrieveAllUsersAndMap()
        {

            var users = await _context.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                if (!user.IsActive)
                    continue;

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                userDTOs.Add(userDTO);
            }

            return userDTOs;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserViewModel body)
        {
            var user = new User
            {
                Name = body.Name,
                Username = body.Username,
                Email = body.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(body.Password),
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
            };

            return userDTO;
        }
    }
}

