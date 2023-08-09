using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.NFSMs.ViewModels;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Services;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Controllers
{
    [ApiController]
    [Route("import-nfsm")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class NFSMController : ControllerBase
    {
        private readonly NFSMService _service;

        public NFSMController(NFSMService service)
        {
            _service = service;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] NFSMImportViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _service.Validate(body, user);

            return Created($"import-nfsm", result);
        }

        [HttpPost]
        public async Task<IActionResult> Import([FromBody] ResponseNFSMDTO body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var result = await _service.ImportAndFix(body, user);

            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    var result = await _service

        //}
    }
}
