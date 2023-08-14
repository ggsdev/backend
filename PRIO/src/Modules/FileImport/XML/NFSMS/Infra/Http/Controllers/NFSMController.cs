using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.NFSMS.ViewModels;
using PRIO.src.Modules.FileImport.XML.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.Http.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class NFSMController : ControllerBase
    {
        private readonly NFSMService _service;

        public NFSMController(NFSMService service)
        {
            _service = service;
        }

        [HttpPost("import-nfsm/validate")]
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

        [HttpPost("import-nfsm")]
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

        [HttpGet("nfsms")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();

            return Ok(result);
        }
        [HttpGet("nfsms/{id}")]
        public async Task<IActionResult> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _service.GetOne(id));

        }

        [HttpGet("nfsms/{id}/download")]
        public async Task<IActionResult> DownloadNfsm([FromRoute] Guid id)
        {
            var nfsm = await _service.DownloadNfsm(id);

            return Ok(nfsm);
        }

        [HttpPost("import-nfsms/errors")]
        public ActionResult ErrorsDownload([FromBody] ErrorsImportViewModel data)
        {
            var result = _service.DownloadErrors(data.Errors);

            return Ok(result);
        }
    }
}
