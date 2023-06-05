using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ClusterDTOS;
using PRIO.Models.Clusters;
using PRIO.ViewModels.Clusters;

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

            var clusterHistory = new ClusterHistory
            {
                Name = body.Name,
                NameOld = null,
                CodClusterOld = null,
                CodCluster = body.CodCluster,
                DescriptionOld = null,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActiveOld = null,
                IsActive = true,
                TypeOperation = "CREATE",
                Cluster = cluster,
            };

            await _context.ClustersHistories.AddAsync(clusterHistory);
            await _context.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return Created($"clusters/{cluster.Id}", clusterDTO);
        }


        [HttpGet("clusters")]
        public async Task<IActionResult> Get()
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

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var clusterHistory = new ClusterHistory
            {
                Name = body.Name is not null ? body.Name : cluster.Name,
                NameOld = cluster.Name,
                CodCluster = body.CodCluster is not null ? body.CodCluster : cluster.CodCluster,
                CodClusterOld = cluster.CodCluster,
                Description = body.Description is not null ? body.Description : cluster.Description,
                DescriptionOld = cluster.Description,
                IsActive = true,
                IsActiveOld = cluster.IsActive,
                TypeOperation = "UPDATE",
                User = user,
                Cluster = cluster,
            };
            _context.Update(clusterHistory);

            cluster.Name = body.Name is not null ? body.Name : cluster.Name;
            cluster.Description = body.Description is not null ? body.Description : cluster.Description;
            cluster.CodCluster = body.CodCluster is not null ? body.CodCluster : cluster.CodCluster;

            _context.Update(cluster);
            await _context.SaveChangesAsync();

            var clusternDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusternDTO);
        }

        [HttpPatch("clusters/{clusterId}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid clusterId)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null || cluster.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or active already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var clusterHistory = new ClusterHistory
            {
                Name = cluster.Name,
                NameOld = cluster.Name,
                CodCluster = cluster.CodCluster,
                CodClusterOld = cluster.CodCluster,
                Description = cluster.Description,
                DescriptionOld = cluster.Description,
                IsActive = true,
                IsActiveOld = cluster.IsActive,
                TypeOperation = "RESTORE",
                User = user,
                Cluster = cluster,
            };
            _context.Update(clusterHistory);

            cluster.IsActive = true;
            cluster.DeletedAt = null;

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


            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });


            var clusterHistory = new ClusterHistory
            {
                Name = cluster.Name,
                NameOld = cluster.Name,
                CodCluster = cluster.CodCluster,
                CodClusterOld = cluster.CodCluster,
                Description = cluster.Description,
                DescriptionOld = cluster.Description,
                IsActive = false,
                IsActiveOld = cluster.IsActive,
                TypeOperation = "DELETE",
                User = user,
                Cluster = cluster,
            };
            _context.Update(clusterHistory);

            cluster.IsActive = false;
            cluster.DeletedAt = DateTime.UtcNow;

            _context.Clusters.Update(cluster);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("clusters/{clusterId}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid clusterId)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == clusterId);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            var clusterHistories = await _context.ClustersHistories.Include(x => x.User)
                                                      .Include(x => x.Cluster)
                                                      .Include(x => x.User)
                                                      .Where(x => x.Cluster.Id == clusterId)
                                                      .OrderByDescending(x => x.CreatedAt)
                                                      .ToListAsync();

            var clusterHistoriesDTO = _mapper.Map<List<ClusterHistory>, List<ClusterHistoryDTO>>(clusterHistories);

            return Ok(clusterHistoriesDTO);
        }
    }
}
