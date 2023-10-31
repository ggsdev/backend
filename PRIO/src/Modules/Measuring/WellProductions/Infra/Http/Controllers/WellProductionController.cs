using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.CachePolicies;
using PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Services;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.Http.Controllers
{
    [ApiController]
    [Route("production-appropriate")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class WellProductionController : ControllerBase
    {
        private readonly WellProductionService _service;
        private readonly IOutputCacheStore _cache;

        public WellProductionController(WellProductionService service, IOutputCacheStore cache)
        {
            _service = service;
            _cache = cache;
        }

        [OutputCache(PolicyName = nameof(AuthProductionCachePolicy))]
        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute] Guid id, CancellationToken ct)
        {
            var data = await _service.CreateAppropriation(id);

            await _cache.EvictByTagAsync("ProductionTag", ct);

            return Ok(data);
        }
    }
}
