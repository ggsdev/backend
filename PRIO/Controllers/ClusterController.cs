using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.ClusterDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
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

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCluster,
                CreatedBy = user,
                TableItemId = cluster.Id,

                UpdatedData = cluster,
            };

            Console.WriteLine(cluster.Name);
            Console.WriteLine(history.Table);

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            //var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);

            //return Created($"clusters/{cluster.Id}", clusterDTO);
            return Ok();
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

            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            cluster.Name = body.Name is not null ? body.Name : cluster.Name;
            cluster.Description = body.Description is not null ? body.Description : cluster.Description;
            cluster.CodCluster = body.CodCluster is not null ? body.CodCluster : cluster.CodCluster;

            _context.UpdateRange(cluster);
            await _context.SaveChangesAsync();


            var clusterDTO = _mapper.Map<Cluster, ClusterDTO>(cluster);
            return Ok(clusterDTO);
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null || cluster.IsActive is true)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or active already"
                });

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

            var cluster = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null || !cluster.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found or inactive already"
                });

            cluster.IsActive = false;
            cluster.DeletedAt = DateTime.UtcNow;

            _context.Clusters.Update(cluster);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        //[HttpGet("{id:Guid}/history")]
        //public async Task<IActionResult> GetHistory([FromRoute] Guid id)
        //{
        //    var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        //    if (cluster is null)
        //        return NotFound(new ErrorResponseDTO
        //        {
        //            Message = "Cluster not found"
        //        });

        //    var clusterHistories = await _context.ClustersHistories
        //                                            .Include(x => x.User)
        //                                            .Include(x => x.Cluster)
        //                                            .Where(x => x.Cluster.Id == id)
        //                                            .OrderByDescending(x => x.CreatedAt)
        //                                            .ToListAsync();

        //    var clusterHistoriesDTO = _mapper.Map<List<ClusterHistory>, List<ClusterHistoryDTO>>(clusterHistories);

        //    return Ok(clusterHistoriesDTO);
        //}
    }
}
