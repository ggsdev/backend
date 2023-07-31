
using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Controllers
{
    [ApiController]
    [Route("btp")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class BTPController : ControllerBase
    {
        private readonly BTPService _BTPService;
        public BTPController(BTPService service)
        {
            _BTPService = service;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var btpsDTO = await _BTPService.Get();
            return Ok(btpsDTO);
        }
    }
}
