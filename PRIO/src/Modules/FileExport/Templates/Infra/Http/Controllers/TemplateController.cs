using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.FileExport.Templates.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileExport.Templates.Infra.Http.Controllers
{
    [ApiController]
    [Route("export-templates")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class TemplateController : ControllerBase
    {
        private readonly TemplateService _service;

        public TemplateController(TemplateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetTemplates();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _service.GetTemplateById(id);

            return Ok(data);
        }
    }
}
