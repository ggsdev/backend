using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models.Installations;
using PRIO.ViewModels.Installations;
using PRIO.ViewModels.Zones;
using System.Security.Policy;

namespace PRIO.Controllers
{
    [ApiController]
    public class InstallationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InstallationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost("installations")]
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

            await _context.AddAsync(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Created($"installations/{installation.Id}", installationDTO);
        }

        [HttpGet("installations")]
        public async Task<IActionResult> Get()
        {
            var installations = await _context.Installations.Include(x => x.Fields).Include(x => x.User).ToListAsync();
            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return Ok(installationsDTO);
        }

        [HttpGet("installations/{installationId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid installationId)
        {

            var installation = await _context.Installations.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });
            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Ok(installationDTO);
        }

        [HttpPatch("installations/{installationId}")]
        public async Task<IActionResult> Update([FromRoute] Guid installationId, [FromBody] UpdateInstallationViewModel body)
        {
            var installation = await _context.Installations.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            if (body.ClusterId is not null)
            {
                var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);

                if (clusterInDatabase is null)
                    return NotFound(new ErrorResponseDTO
                    {
                        Message = "Cluster not found"
                    });

                installation.Cluster = clusterInDatabase is not null ? clusterInDatabase : installation.Cluster;

            }

            installation.Name = body.Name is not null ? body.Name : installation.Name;
            installation.Description = body.Description is not null ? body.Description : installation.Description;
            installation.CodInstallation = body.CodInstallation is not null ? body.CodInstallation : installation.CodInstallation;


            _context.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Ok(installationDTO);

        }
        [HttpDelete("installations/{installationId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid installationId)
        {
            var installation = await _context.Installations.FirstOrDefaultAsync(x => x.Id == installationId);
            if (installation is null || !installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or inactive already"
                });

            installation.IsActive = false;
            installation.DeletedAt = DateTime.UtcNow;

            _context.Update(installation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
