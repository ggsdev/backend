using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Hierarchy.ViewModels;
using PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;

namespace PRIO.src.Modules.FileImport.XLSX.Infra.Http.Controllers
{
    [ApiController]
    [Route("xlsx")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class XLSXController : ControllerBase
    {
        private readonly XLSXService _service;
        private readonly SystemHistoryService _systemHistoryService;

        public XLSXController(XLSXService service, SystemHistoryService systemHistoryService)
        {
            _service = service;
            _systemHistoryService = systemHistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data)
        {
            var user = HttpContext.Items["User"] as User;

            var result = await _service.ImportFiles(data, user);

            return Created("xlsx", result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryImports()
        {
            var data = await _systemHistoryService.GetImports();

            return Ok(data);
        }
    }
}
