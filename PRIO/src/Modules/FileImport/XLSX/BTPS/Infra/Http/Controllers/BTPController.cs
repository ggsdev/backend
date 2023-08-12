
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

        [HttpPost("xls/validate")]
        public async Task<IActionResult> Validate([FromBody] RequestWellTestXls body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.ValidateImportFiles(body, user);

            return Ok(result);
        }

        [HttpPost("xls/import")]
        public async Task<IActionResult> PostData([FromBody] ImportViewModel body)
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

        [HttpGet("{wellId}/date")]
        public async Task<IActionResult> GetByDate([FromQuery] string date, [FromRoute] Guid wellId)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var btpDTO = await _BTPService.GetByDate(date, wellId);

            return Ok(btpDTO);
        }

        [HttpGet("data")]
        public async Task<IActionResult> RenderBTPData()
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.GetBTPData();

            return Ok(result);
        }

        [HttpPatch("data/{dataId}")]
        public async Task<IActionResult> RenderBTPData([FromRoute] Guid dataId)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.UpdateByDataId(dataId);

            return Ok(result);
        }

        [HttpGet("data/{wellId}")]
        public async Task<IActionResult> RenderBTPDataByWellId([FromRoute] Guid wellId)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _BTPService.GetBTPDataByWellId(wellId);

            return Ok(result);
        }
    }
}
