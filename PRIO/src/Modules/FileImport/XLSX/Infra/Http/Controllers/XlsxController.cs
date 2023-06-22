using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XLSX.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XLSX.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.FileImport.XLSX.Infra.Http.Controllers
{
    [ApiController]
    [Route("xlsx")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class XLSXController : ControllerBase
    {
        private readonly XLSXService _service;

        public XLSXController(XLSXService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostBase64File([FromBody] RequestXslxViewModel data)
        {
            var user = HttpContext.Items["User"] as User;

            await _service.ImportFiles(data, user);

            return Created("xlsx", new ImportResponseDTO
            {
                Message = "File imported successfully"
            });
        }
    }
}