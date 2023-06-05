using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRIO.Data;

namespace PRIO.Controllers
{
    public class BaseApiController : ControllerBase
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
