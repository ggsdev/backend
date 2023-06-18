using Microsoft.AspNetCore.Mvc;
using PRIO.DTOS.GlobalDTOS;
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
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var clusterDTO = await _clusterServices.CreateCluster(body, user);

            return Created($"clusters/{clusterDTO.Id}", clusterDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clustersDTO = await _clusterServices.GetClusters();

            return Ok(clustersDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var clusterDTO = await _clusterServices.GetClusterById(id);

            return Ok(clusterDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClusterViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var clusterDTO = await _clusterServices.UpdateCluster(id, body, user);

            return Ok(clusterDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            await _clusterServices.DeleteCluster(id, user);

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var clusterDTO = await _clusterServices.RestoreCluster(id, user);

            return Ok(clusterDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var clusterHistories = await _clusterServices.GetClusterHistory(id);

            return Ok(clusterHistories);
        }
    }
}
