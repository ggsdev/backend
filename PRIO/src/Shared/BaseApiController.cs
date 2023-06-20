using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared
{
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        protected BaseApiController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
