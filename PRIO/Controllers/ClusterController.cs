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
            var clusters = await context.Clusters.Include(x => x.Fields).ToListAsync();
            return Ok(clusters);
        }
    }
}
