using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.UserDTOS;
using PRIO.Models.UserControlAccessModels;
using PRIO.ViewModels.Users;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.Http.Services
{
    public class UserServices
    {

        private DataContext _context;
        private IMapper _mapper;

        public UserServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(x => x.Session)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<List<UserDTO>> RetrieveAllUsersAndMap()
        {

            var users = await _context.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                if (!user.IsActive)
                    continue;

                var userDTO = _mapper.Map<User, UserDTO>(user);
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
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

    }
}

