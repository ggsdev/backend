using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.CachePolicies;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.ViewModels;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Controllers
{
    [ApiController]
    [Route("productions")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ProductionController : ControllerBase
    {
        private readonly ProductionService _productionService;
        private readonly IOutputCacheStore _cache;

        public ProductionController(ProductionService service, IOutputCacheStore cache)
        {
            _productionService = service;
            _cache = cache;
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpGet("{id}/total-daily")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var production = await _productionService.GetById(id);
            return Ok(production);
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productions = await _productionService.GetAllProductions();
            return Ok(productions);
        }

        //[HttpGet("deleted")]
        //public async Task<IActionResult> GetDeleted()
        //{
        //    var productions = await _productionService
        //        .GetDeletedProductions();

        //    return Ok(productions);

        //}

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpGet("{id}/files")]
        public async Task<IActionResult> DownloadFiles([FromRoute] Guid id)
        {
            var files = await _productionService
                .DownloadAllProductionFiles(id);

            return Ok(files);
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduction([FromRoute] Guid id, CancellationToken ct)
        {
            await _productionService
                .DeleteProduction(id);

            await _cache.EvictByTagAsync("ProductionTag", ct);

            return NoContent();
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpPatch("{id}/gasDetailed")]
        public async Task<IActionResult> UpdateDetailedGas([FromRoute] Guid id, UpdateDetailedGasViewModel body, CancellationToken ct)
        {
            var data = await _productionService.UpdateDetailedGas(id, body);

            await _cache.EvictByTagAsync("ProductionTag", ct);

            return Ok(data);
        }
    }
}
