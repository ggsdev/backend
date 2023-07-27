using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Services;
using PRIO.src.Modules.Hierarchy.Clusters.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.Http.Controllers
{
    [ApiController]
    [Route("clusters")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ClusterController : ControllerBase
    {
        private readonly ClusterService _clusterService;

        public ClusterController(ClusterService service)

        {
            _clusterService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClusterViewModel body)
        {
            Console.WriteLine("io");

            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var clusterDTO = await _clusterService.CreateCluster(body, user);

            return Created($"clusters/{clusterDTO.Id}", clusterDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clustersDTO = await _clusterService.GetClusters();

            return Ok(clustersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var clusterDTO = await _clusterService.GetClusterById(id);

            return Ok(clusterDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClusterViewModel body)
        {
            var user = HttpContext.Items["User"] as User;
            var clusterDTO = await _clusterService.UpdateCluster(id, body, user);

            return Ok(clusterDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _clusterService.DeleteCluster(id, user);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var clusterDTO = await _clusterService.RestoreCluster(id, user);

            return Ok(clusterDTO);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var clusterHistories = await _clusterService.GetClusterHistory(id);

            return Ok(clusterHistories);
        }
    }
}
