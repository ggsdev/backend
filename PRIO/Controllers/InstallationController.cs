using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.DTOS.InstallationDTOS;
using PRIO.Filters;
using PRIO.Models.Installations;
using PRIO.Models.Users;
using PRIO.Utils;
using PRIO.ViewModels.Installations;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("installations")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class InstallationController : BaseApiController
    {
        public InstallationController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInstallationViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);
            if (clusterInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Cluster not found"
                });

            var installation = new Installation
            {
                Name = body.Name,
                Description = body.Description,
                CodInstallationUep = body.CodInstallationUep,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Installations.AddAsync(installation);

            var installationHistory = new InstallationHistory
            {
                Cluster = clusterInDatabase,

                Name = installation.Name,

                CodInstallationUep = installation.CodInstallationUep,

                Description = installation.Description,

                User = user,
                Installation = installation,
                TypeOperation = TypeOperation.Create
            };

            await _context.InstallationHistories.AddAsync(installationHistory);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return Created($"installations/{installation.Id}", installationDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var installations = await _context.Installations.Include(x => x.Fields).Include(x => x.User).ToListAsync();
            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return Ok(installationsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var installation = await _context.Installations
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Ok(installationDTO);
        }

        [HttpGet("{id:Guid}/history")]
        public async Task<IActionResult> GetHistoryById([FromRoute] Guid id)
        {
            var installationHistories = await _context.InstallationHistories
                .Include(x => x.User)
                .Include(x => x.Cluster)
                .Include(x => x.Installation)
                .Where(x => x.Installation.Id == id)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            if (installationHistories is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var installationHistoriesDTO = _mapper.Map<List<InstallationHistory>, List<InstallationHistoryDTO>>(installationHistories);
            return Ok(installationHistoriesDTO);
        }


        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateInstallationViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);

            var installationHistory = new InstallationHistory
            {
                Name = body.Name is not null ? body.Name : installation.Name,
                NameOld = installation.Name,

                CodInstallationUep = body.CodInstallationUep is not null ? installation.CodInstallationUep : installation.CodInstallationUep,
                CodInstallationUepOld = installation.CodInstallationUep,

                Description = body.Description is not null ? body.Description : installation.Description,
                DescriptionOld = installation.Description,

                Cluster = clusterInDatabase is not null ? clusterInDatabase : installation.Cluster,
                ClusterOldId = installation.Cluster?.Id,

                User = user,

                Installation = installation,

                TypeOperation = TypeOperation.Update
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            if (body.ClusterId is not null && clusterInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });

            installation.CodInstallationUep = body.CodInstallationUep is not null ? body.CodInstallationUep : installation.CodInstallationUep;
            installation.Name = body.Name is not null ? body.Name : installation.Name;
            installation.Description = body.Description is not null ? body.Description : installation.Description;
            installation.Cluster = clusterInDatabase is not null ? clusterInDatabase : installation.Cluster;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);
            return Ok(installationDTO);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null || !installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or inactive already"
                });

            var installationHistory = new InstallationHistory
            {
                Name = installation.Name,
                NameOld = installation.Name,

                Cluster = installation.Cluster,
                ClusterOldId = installation.Cluster?.Id,

                CodInstallationUep = installation.CodInstallationUep,
                CodInstallationUepOld = installation.CodInstallationUep,

                Description = installation.Description,
                DescriptionOld = installation.Description,

                User = user,

                IsActive = false,
                IsActiveOld = installation.IsActive,

                Installation = installation,

                TypeOperation = TypeOperation.Delete
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            installation.IsActive = false;
            installation.DeletedAt = DateTime.UtcNow;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null || installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or active already"
                });

            var installationHistory = new InstallationHistory
            {
                Name = installation.Name,
                NameOld = installation.Name,

                Cluster = installation.Cluster,
                ClusterOldId = installation.Cluster?.Id,

                CodInstallationUep = installation.CodInstallationUep,
                CodInstallationUepOld = installation.CodInstallationUep,

                Description = installation.Description,
                DescriptionOld = installation.Description,

                User = user,

                Installation = installation,

                IsActive = true,
                IsActiveOld = installation.IsActive,

                TypeOperation = TypeOperation.Restore
            };

            await _context.InstallationHistories.AddAsync(installationHistory);

            installation.IsActive = true;
            installation.DeletedAt = null;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return Ok(installationDTO);
        }
    }
}
