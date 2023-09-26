﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XML.Dtos;
using PRIO.src.Modules.FileImport.XML.Infra.Http.Services;
using PRIO.src.Modules.FileImport.XML.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;
using PRIO.src.Shared.Utils;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("import-measurements")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public partial class XMLImportController : ControllerBase
    {
        private readonly XMLImportService _service;
        private readonly IOutputCacheStore _cache;


        public XMLImportController(XMLImportService XMLImportService, IOutputCacheStore cache)
        {
            _service = XMLImportService;
            _cache = cache;
        }

        [HttpPost]
        [OutputCache(PolicyName = "ProductionPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImportResponseDTO))]
        public async Task<ActionResult> ImportFiles([FromBody] ResponseXmlDto data, CancellationToken ct)
        {
            var user = HttpContext.Items["User"] as User;
            var result = await _service.Import(data, user);

            await _cache.EvictByTagAsync("ProductionPolicyTag", ct);

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
            var result = Download.DownloadErrors(data.Errors);

            return Ok(result);
        }

    }
}

