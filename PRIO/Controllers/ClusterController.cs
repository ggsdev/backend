using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Clusters;
using PRIO.ViewModels.Installations;

namespace PRIO.Controllers
{
    [ApiController]
    public class ClusterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ClusterController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost("clusters")]
        public async Task<IActionResult> Create([FromBody] CreateClusterViewModel body)
        {
            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync((x) => x.CodCluster == body.CodCluster);
            if (clusterInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Cluster with code: {body.CodCluster} already exists."
                });


            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
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
                User = user,
            };
            

            await _context.Clusters.AddAsync(cluster);
            await _context.SaveChangesAsync();



            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return Created($"clusters/{cluster.Id}", clusterDTO);
        }


        [HttpGet("clusters")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var clusters = await _context.Clusters.Include(x => x.Installations).Include(x => x.User).ToListAsync();
            var clustersDTO = _mapper.Map<List<Cluster>, List<ClusterDTO>>(clusters);
            return Ok(clustersDTO);
        }  
        
        [HttpGet("clusters/{clusterId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid clusterId)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });
            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusterDTO);
        }

        [HttpPatch("clusters/{clusterId}")]
        public async Task<IActionResult> Update([FromRoute] Guid clusterId, [FromBody] UpdateClusterViewModel body)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            cluster.Name = body.Name is not null ? body.Name : cluster.Name;
            cluster.Description = body.Description is not null ? body.Description : cluster.Description;
            cluster.CodCluster = body.CodCluster is not null ? body.CodCluster : cluster.CodCluster;


            _context.Update(cluster);
            await _context.SaveChangesAsync();

            var clusternDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusternDTO);
        }

        [HttpDelete("clusters/{clusterId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid clusterId)
        {
            var cluster = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null || !cluster.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or inactive already"
                });

            cluster.IsActive = false;
            cluster.DeletedAt = DateTime.UtcNow;

            _context.Update(cluster);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
