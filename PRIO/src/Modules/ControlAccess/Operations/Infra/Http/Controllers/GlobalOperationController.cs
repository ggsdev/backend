using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.ControlAccess.Operations.Infra.Http.Controllers
{
    [ApiController]
    [Route("operations")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class GlobalOperationController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GlobalOperationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<List<GlobalOperation>> Get()
        {
            var operations = await _context.GlobalOperations.ToListAsync();
            return operations;
        }
    }
}
