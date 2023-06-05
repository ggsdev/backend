using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PRIO.Data;
using PRIO.Models.Users;

namespace PRIO.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly DataContext _context;
        protected readonly IMemoryCache _cache;
        protected readonly IMapper _mapper;

        protected BaseApiController(DataContext context, IMemoryCache cache, IMapper mapper)
        {
            _context = context;
            _cache = cache;
            _mapper = mapper;
        }

        protected async Task<User?> GetUserFromCache()
        {
            var userId = (Guid)HttpContext.Items["Id"]!;
            if (_cache.TryGetValue(userId, out User? user))
            {
                return user;
            }

            user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                _cache.Set(userId, user, TimeSpan.FromMinutes(30));
            }

            return user;
        }
    }
}
