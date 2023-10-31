using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.FileExport.Templates.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileExport.Templates.Infra.Http.Controllers
{
    [ApiController]
    [Route("export-xlsx")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class XLSXExportController : ControllerBase
    {
        private readonly XLSXExportService _service;

        public XLSXExportController(XLSXExportService service)
        {
            _service = service;
        }

        [HttpPost("opening-closing-events/{fieldId}")]
        public async Task<IActionResult> Create(DateTime beginning, DateTime end, Guid fieldId)
        {
            var data = await _service.GenerateClosingOpeningRelatory(beginning, end, fieldId);

            return Created($"export-xlsx/{data.Id}", data);
        }
    }
}
