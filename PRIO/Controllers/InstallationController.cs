using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.InstallationDTOS;
using PRIO.Models.Installations;
using PRIO.Utils;
using PRIO.ViewModels.Installations;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("installations")]
    public class InstallationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InstallationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInstallationViewModel body)
        {
            var installationInDatabase = await _context.Installations.FirstOrDefaultAsync(x => x.CodInstallation == body.CodInstallation);
            if (installationInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Installation with code: {body.CodInstallation} already exists, try another code."
                });

            var clusterFound = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);
            if (clusterFound is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Cluster is not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var installation = new Installation
            {
                Name = body.Name,
                Description = body.Description is not null ? body.Description : null,
                CodInstallation = body.CodInstallation,
                Cluster = clusterFound,
                User = user
            };

            await _context.Installations.AddAsync(installation);

            var installationHistory = new InstallationHistory
            {
                Cluster = clusterFound,

                Name = installation.Name,

                ClusterName = clusterFound.Name,

                CodInstallation = installation.CodInstallation,

                Description = installation.Description,

                User = user,
                Installation = installation,
                Type = TypeOperation.Create
            };

            await _context.InstallationHistories.AddAsync(installationHistory);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Created($"installations/{installation.Id}", installationDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var installations = await _context.Installations.Include(x => x.InstallationHistories).Include(x => x.Fields).Include(x => x.User).ToListAsync();
            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return Ok(installationsDTO);
        }

        [HttpGet("{installationId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid installationId)
        {
            var installation = await _context.Installations
                .Include(x => x.User)
                .Include(x => x.InstallationHistories)
                .FirstOrDefaultAsync(x => x.Id == installationId);

            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            installationDTO.InstallationHistories = installationDTO.InstallationHistories.OrderByDescending(fh => fh.CreatedAt).ToList();

            return Ok(installationDTO);
        }

        [HttpPatch("{installationId}")]
        public async Task<IActionResult> Update([FromRoute] Guid installationId, [FromBody] UpdateInstallationViewModel body)
        {
            var installation = await _context.Installations.Include(x => x.User).Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);

            var installationHistory = new InstallationHistory
            {
                Name = body.Name is not null ? body.Name : installation.Name,
                NameOld = installation.Name,

                Cluster = clusterInDatabase is not null ? clusterInDatabase : installation.Cluster,
                ClusterOldId = installation.Cluster.Id,

                ClusterName = clusterInDatabase is not null ? clusterInDatabase.Name : installation.Cluster.Name,
                ClusterNameOld = installation.Cluster.Name,

                CodInstallation = installation.CodInstallation,
                CodInstallationOld = installation.CodInstallation,

                Description = body.Description is not null ? body.Description : installation.Description,
                DescriptionOld = installation.Description,

                User = user,

                Installation = installation,

                Type = TypeOperation.Update
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            if (body.ClusterId is not null && clusterInDatabase is null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });
            }

            installation.Name = body.Name is not null ? body.Name : installation.Name;
            installation.Description = body.Description is not null ? body.Description : installation.Description;
            installation.CodInstallation = body.CodInstallation is not null ? body.CodInstallation : installation.CodInstallation;
            installation.Cluster = clusterInDatabase is not null ? clusterInDatabase : installation.Cluster;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);
            return Ok(installationDTO);

        }
        [HttpDelete("{installationId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid installationId)
        {
            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null || !installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or inactive already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var installationHistory = new InstallationHistory
            {
                Name = installation.Name,
                NameOld = installation.Name,

                Cluster = installation.Cluster,
                ClusterOldId = installation.Cluster.Id,

                CodInstallation = installation.CodInstallation,
                CodInstallationOld = installation.CodInstallation,

                Description = installation.Description,
                DescriptionOld = installation.Description,

                User = user,

                IsActive = false,
                IsActiveOld = installation.IsActive,

                Installation = installation,

                Type = TypeOperation.Delete
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            installation.IsActive = false;
            installation.DeletedAt = DateTime.UtcNow;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{installationId}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid installationId)
        {
            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null || installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or active already"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await _context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User not found"
                });

            var installationHistory = new InstallationHistory
            {
                Name = installation.Name,
                NameOld = installation.Name,

                Cluster = installation.Cluster,
                ClusterOldId = installation.Cluster.Id,

                CodInstallation = installation.CodInstallation,
                CodInstallationOld = installation.CodInstallation,

                Description = installation.Description,
                DescriptionOld = installation.Description,

                User = user,

                Installation = installation,

                IsActive = true,
                IsActiveOld = installation.IsActive,

                Type = TypeOperation.Restore
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            installation.IsActive = true;
            installation.DeletedAt = null;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Ok(installationDTO);
        }
    }
}
