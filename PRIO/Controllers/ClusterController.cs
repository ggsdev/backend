using Microsoft.AspNetCore.Mvc;
using PRIO.Filters;
using PRIO.Models.UserControlAccessModels;
using PRIO.Services;
using PRIO.ViewModels.Clusters;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("clusters")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ClusterController : ControllerBase
    {
        private readonly ClusterServices _clusterServices;

        public ClusterController(ClusterServices services)

        {
            _clusterServices = services;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClusterViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterDTO = await _clusterServices.Create(body, user);

            return Created($"clusters/{clusterDTO.Id}", clusterDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clustersDTO = await _clusterServices.Get();

            return Ok(clustersDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var clusterDTO = await _clusterServices.GetById(id);

            return Ok(clusterDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClusterViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterDTO = await _clusterServices.Update(id, body, user);

            return Ok(clusterDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            await _clusterServices.Delete(id, user);

            return NoContent();
        }


        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterDTO = await _clusterServices.Restore(id, user);

            return Ok(clusterDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var clusterHistories = await _clusterServices.GetHistory(id);

            return Ok(clusterHistories);
        }
    }
}
