using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Clusters;

namespace PRIO.Controllers
{
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ClusterController(IMapper mapper) {
            _mapper = mapper;
        }

        [HttpPost("clusters")]
        public async Task<IActionResult> Create([FromBody] CreateClusterViewModel body, [FromServices] DataContext context)
        {
            var checkClusterInDatabase = await context.Clusters.FirstOrDefaultAsync((x) => x.CodCluster == body.CodCluster);

            if (checkClusterInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Cluster with code: {body.CodCluster} already exists."
                });


            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var cluster = new Cluster
            {
                Name = body.Name,
                CodCluster = body.CodCluster,
                Description = body.Description is not null ? body.Description : null,
                User = user!,
            };

            await context.Clusters.AddAsync(cluster);
            await context.SaveChangesAsync();

            return Created($"clusters/{cluster.Id}", cluster);
        }


        [HttpGet("clusters")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var clusters = await context.Clusters.Include(x => x.Installations).ToListAsync();
            return Ok(clusters);
        }  
        
        [HttpGet("clusters/{clusterId}")]
        public async Task<IActionResult> Get([FromRoute] string clusterId , [FromServices] DataContext context)
        {
            var cluster = await context.Clusters.FirstOrDefaultAsync(x => x.Id.Equals(clusterId));
            return Ok(cluster);
        }

        [HttpPatch]
        [Route("clusters/{clusterId}")]
        public async Task<IActionResult> UpdateCluster([FromRoute] Guid clusterId, [FromBody] UpdateClusterViewModel body, [FromServices] DataContext context)
        {
            var cluster = await context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Cluster with ID: {clusterId} not found."
                });
            }
            Console.WriteLine(cluster);
           
            return NoContent();
        }
    }
}
