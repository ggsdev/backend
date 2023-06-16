using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.ControlAccessDTOS;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("groups")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class GroupController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GroupController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("{groupId}/user/{userId}")]
        public async Task<IActionResult> InsertUser([FromRoute] Guid groupId, [FromRoute] Guid userId)
        {
            var groupPermissions = await _context.GroupPermissions.Include(x => x.Group).Where(x => x.Group.Id == groupId).ToListAsync();
            var groupPermissionsDTO = _mapper.Map<List<GroupPermission>, List<GroupPermissionsDTO>>(groupPermissions);
            return Ok(groupPermissions);
        }
    }
}
