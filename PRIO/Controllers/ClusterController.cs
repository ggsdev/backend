using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.ClusterDTOS;
using PRIO.Filters;
using PRIO.Models.Clusters;
using PRIO.Models.Users;
using PRIO.Utils;
using PRIO.ViewModels.Clusters;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("clusters")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ClusterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ClusterController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClusterViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync((x) => x.CodCluster == body.CodCluster);
            if (clusterInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Cluster with code: {body.CodCluster} already exists."
                });

            var cluster = new Cluster
            {
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
                CodCluster = body.CodCluster is not null ? body.CodCluster : GenerateCode.Generate(body.Name)
            };

            await _context.Clusters.AddAsync(cluster);

            var clusterHistory = new ClusterHistory
            {
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                User = user,
                CodCluster = cluster.CodCluster,
                TypeOperation = TypeOperation.Create,

                Cluster = cluster,
            };

            await _context.ClustersHistories.AddAsync(clusterHistory);
            await _context.SaveChangesAsync();

            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            return Created($"clusters/{cluster.Id}", clusterDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clusters = await _context.Clusters.Include(x => x.Installations).Include(x => x.User).ToListAsync();
            var clustersDTO = _mapper.Map<List<Cluster>, List<ClusterDTO>>(clusters);
            return Ok(clustersDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });
            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusterDTO);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClusterViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var cluster = await _context.Clusters.Include(x => x.User).Include(x => x.ClusterHistories).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            var lastClusterHistory = cluster.ClusterHistories?.LastOrDefault();
            var isUserUpdatingSomething = (body.Name is not null && cluster.Name != body.Name) ||
                (body.Description is not null && cluster.Description != body.Description) ||
                (body.CodCluster is not null && cluster.CodCluster != body.CodCluster);

            if (isUserUpdatingSomething)
            {
                var clusterHistory = new ClusterHistory
                {
                    Name = body.Name ?? cluster.Name,
                    NameOld = (body.Name == cluster.Name) ? lastClusterHistory?.NameOld : cluster.Name,
                    CodCluster = body.CodCluster ?? cluster.CodCluster,
                    CodClusterOld = (body.CodCluster == cluster.CodCluster) ? lastClusterHistory?.CodClusterOld : cluster.CodCluster,
                    Description = body.Description ?? cluster.Description,
                    DescriptionOld = (body.Description == cluster.Description) ? lastClusterHistory?.DescriptionOld : cluster.Description,
                    IsActiveOld = (body.IsActive == cluster.IsActive) ? lastClusterHistory?.IsActiveOld : cluster.IsActive,
                    TypeOperation = TypeOperation.Update,
                    User = user,
                    Cluster = cluster
                };

                cluster.Name ??= body.Name;
                cluster.Description ??= body.Description;
                cluster.CodCluster ??= body.CodCluster;

                _context.UpdateRange(cluster, clusterHistory);
                await _context.SaveChangesAsync();
            }

            var clusternDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusternDTO);
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var cluster = await _context.Clusters.Include(x => x.User).Include(x => x.ClusterHistories).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null || cluster.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or active already"
                });

            var lastClusterHistory = cluster.ClusterHistories?.LastOrDefault();

            var clusterHistory = new ClusterHistory
            {
                Name = cluster.Name,
                NameOld = lastClusterHistory?.NameOld,
                CodCluster = cluster.CodCluster,
                CodClusterOld = lastClusterHistory?.CodClusterOld,
                Description = cluster.Description,
                DescriptionOld = lastClusterHistory?.DescriptionOld,
                IsActiveOld = lastClusterHistory?.IsActiveOld,
                TypeOperation = TypeOperation.Restore,

                User = user,
                Cluster = cluster,
            };
            await _context.AddAsync(clusterHistory);

            cluster.IsActive = true;
            cluster.DeletedAt = null;

            _context.Update(cluster);
            await _context.SaveChangesAsync();

            var clusternDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusternDTO);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var cluster = await _context.Clusters.Include(x => x.ClusterHistories).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null || !cluster.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or inactive already"
                });

            var lastClusterHistory = cluster.ClusterHistories?.LastOrDefault();

            var clusterHistory = new ClusterHistory
            {
                Name = cluster.Name,
                NameOld = lastClusterHistory?.NameOld,
                CodCluster = cluster.CodCluster,
                CodClusterOld = lastClusterHistory?.CodClusterOld,
                Description = cluster.Description,
                DescriptionOld = lastClusterHistory?.DescriptionOld,
                IsActive = false,
                IsActiveOld = lastClusterHistory?.IsActiveOld,
                TypeOperation = TypeOperation.Delete,
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
        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        {
            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            var clusterHistories = await _context.ClustersHistories
                                                    .Include(x => x.User)
                                                    .Include(x => x.Cluster)
                                                    .Where(x => x.Cluster.Id == id)
                                                    .OrderByDescending(x => x.CreatedAt)
                                                    .ToListAsync();

            var clusterHistoriesDTO = _mapper.Map<List<ClusterHistory>, List<ClusterHistoryDTO>>(clusterHistories);

            return Ok(clusterHistoriesDTO);
        }
    }
}
