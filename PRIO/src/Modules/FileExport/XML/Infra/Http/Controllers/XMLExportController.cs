using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileExport.XML.Infra.Http.Services;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileExport.XML.Infra.Http.Controllers
{
    [ApiController]
    [Route("export-xml")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class XMLExportController : ControllerBase
    {
        private readonly XMLExportService _XMLService;

        public XMLExportController(XMLExportService XMLService)
        {
            _XMLService = XMLService;
        }

        [HttpPost("{table}/{id}")]
        public async Task<IActionResult> ExportXML([FromRoute] Guid id, [FromRoute] string table)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var xmlDTO = await _XMLService.ExportXML(id, table);
            return Ok(xmlDTO);
        }
    }
}
