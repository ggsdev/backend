
using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels;
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
        public async Task<IActionResult> Get([FromQuery] string? type)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var btpsDTO = type is null ? await _BTPService.Get() : await _BTPService.GetByType(type);

            return Ok(btpsDTO);
        }

        [HttpGet("xls")]
        public async Task<IActionResult> Render([FromBody] RequestWellTestXls body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.GetImportFiles(body, user);

            return Ok(result);
        }

        [HttpPost("xls")]
        public async Task<IActionResult> PostData([FromBody] RequestWellTestXls body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.PostImportFiles(body, user);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var btpDTO = await _BTPService.GetById(id);

            return Ok(btpDTO);
        }
    }
}
