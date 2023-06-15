using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            var clusterId = Guid.NewGuid();
            var cluster = new Cluster
            {
                Id = clusterId,
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
                CreatedBy = user?.Id,
                TableItemId = clusterId,
                UpdatedData = cluster,
            };

            await _context.SystemHistories.AddAsync(history);

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

            var cluster = await _context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (cluster is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            var beforeCluster = _mapper.Map<Cluster>(cluster);
            var updatedProperties = ControllerUtils.CompareAndUpdateProperties(cluster, body);

            if (!updatedProperties.Any())
                return BadRequest(new ErrorResponseDTO
                {
                    Message = $"{cluster.Name} already has these values, try to other values."
                });

            var changedFieldsToObject = ControllerUtils.DictionaryToObject(updatedProperties);

            _context.Clusters.Update(cluster);
            var firstHistory = await _context.SystemHistories.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCluster,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                TypeOperation = HistoryColumns.Update,
                FieldsChanged = changedFieldsToObject,
                UpdatedData = cluster,
                PreviousData = beforeCluster,
            };

            await _context.SystemHistories.AddAsync(history);
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

            var firstHistory = await _context.SystemHistories.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();
            var lastHistory = await _context.SystemHistories.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            cluster.IsActive = true;
            cluster.DeletedAt = null;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCluster,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                TypeOperation = HistoryColumns.Restore,
                UpdatedData = cluster,
                PreviousData = lastHistory?.PreviousData,
                FieldsChanged = new
                {
                    cluster.IsActive,
                    cluster.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

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

            var firstHistory = await _context.SystemHistories.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();
            var lastHistory = await _context.SystemHistories.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();

            cluster.IsActive = false;
            cluster.DeletedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCluster,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = cluster.Id,
                TypeOperation = HistoryColumns.Delete,
                UpdatedData = cluster,
                PreviousData = lastHistory?.PreviousData,
                FieldsChanged = new
                {
                    cluster.IsActive,
                    cluster.DeletedAt,
                }
            };

            await _context.SystemHistories.AddAsync(history);

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

            var clusterHistories = await _context.SystemHistories
                   .Where(x => x.TableItemId == id)
                   .OrderByDescending(x => x.CreatedAt)
                   .ToListAsync();

            foreach (var history in clusterHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()) : null;

                history.UpdatedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(history.UpdatedData.ToString());

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()) : null;
            }

            return Ok(clusterHistories);
        }
    }
}
