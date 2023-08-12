using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("import-measurements")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public partial class XMLImportController : ControllerBase
    {
        private readonly XMLImportService _service;

        public XMLImportController(XMLImportService XMLImportService)
        {
            _service = XMLImportService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImportResponseDTO))]
        public async Task<ActionResult> ImportFiles([FromBody] ResponseXmlDto data)
        {
            var user = HttpContext.Items["User"] as User;
            var result = await _service.Import(data, user);

            return Ok(result);
        }

        [HttpPost("validate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOFiles))]
        public async Task<ActionResult> ValidateImport([FromBody] RequestXmlViewModel data)
        {
            var user = HttpContext.Items["User"] as User;
            var result = await _service.Validate(data, user);

            return Ok(result);
        }

        [HttpPost("errors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOFiles))]
        public ActionResult ErrorsDownload([FromBody] ErrorsImportViewModel data)
        {
            var result = _service.DownloadErrors(data.Errors);

            return Ok(result);
        }

    }
}

