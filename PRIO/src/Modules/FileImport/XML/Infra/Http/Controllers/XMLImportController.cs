using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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
        public async Task<ActionResult> ImportFiles([FromBody] RequestXmlViewModel data)
        {
            var user = HttpContext.Items["User"] as User;
            var result = await _service.Import(data, user);


            return Created("import-measurements", result);
        }


        [HttpPost("validate")]
        public async Task<ActionResult> ValidateImport([FromBody] RequestXmlViewModel data)
        {
            var user = HttpContext.Items["User"] as User;
            var result = await _service.Import(data, user);


            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromServices] DataContext _context, [FromQuery] string? acronym, [FromQuery] string? name)
        //{
        //    var filesQuery = _context.FileTypes.Include(x => x.Measurements).ThenInclude(m => m.User);

        //    if (!string.IsNullOrEmpty(acronym))
        //    {
        //        var possibleAcronymValues = new List<string> { "PMO", "PMGL", "PMGD", "EFM" };
        //        var isValidValue = possibleAcronymValues.Contains(acronym.ToUpper().Trim());
        //        if (!isValidValue)
        //            return BadRequest(new ErrorResponseDTO
        //            {
        //                Message = $"Acronym valid values are: PMO, PMGL, PMGD, EFM"
        //            });

        //        filesQuery = _context.FileTypes
        //           .Where(x => x.Acronym == acronym)
        //           .Include(x => x.Measurements)
        //           .ThenInclude(m => m.User);
        //    }

        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        var possibleNameValues = new List<string> { "001", "002", "003", "039" };
        //        var isValidValue = possibleNameValues.Contains(name.ToUpper().Trim());
        //        if (!isValidValue)
        //            return BadRequest(new ErrorResponseDTO
        //            {
        //                Message = "Name valid values are: 001, 002, 003, 039"
        //            });

        //        filesQuery = _context.FileTypes
        //            .Where(x => x.Name == name)
        //            .Include(x => x.Measurements)
        //            .ThenInclude(m => m.User);
        //    }

        //    var files = await filesQuery.ToListAsync();
        //    var measurements = files.SelectMany(file => file.Measurements);

        //    foreach (var measurement in measurements)
        //    {
        //        switch (measurement.FileType.Name)
        //        {
        //            case "001":
        //                _responseResult._001File ??= new List<_001DTO>();
        //                _responseResult._001File.Add(_mapper.Map<_001DTO>(measurement));
        //                break;

        //            case "002":
        //                _responseResult._002File ??= new List<_002DTO>();
        //                _responseResult._002File.Add(_mapper.Map<_002DTO>(measurement));
        //                break;

        //            case "003":
        //                _responseResult._003File ??= new List<_003DTO>();
        //                _responseResult._003File.Add(_mapper.Map<_003DTO>(measurement));
        //                break;

        //            case "039":
        //                _responseResult._039File ??= new List<_039DTO>();
        //                _responseResult._039File.Add(_mapper.Map<_039DTO>(measurement));
        //                break;
        //        }
        //    }

        //    return Ok(_responseResult);
        //}

    }
}

