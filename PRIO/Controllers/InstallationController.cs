using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Installations;

namespace PRIO.Controllers
{
    [ApiController]
    public class InstallationController : ControllerBase
    {
        [HttpPost("installations")]
        public async Task<IActionResult> Create([FromBody] CreateInstallationViewModel body, [FromServices] DataContext context)
        {
            var installationInDatabase = await context.Installations.FirstOrDefaultAsync(x => x.CodInstallation == body.CodInstallation);
            if (installationInDatabase is not null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = $"Installation with code: {body.CodInstallation} already exists, try another code."
                });

            var clusterFound = await context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);
            if (clusterFound is not null) 
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Cluster is not found"
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);
            if (user is not null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"User is not found"
                });

            var installation = new Installation
            {
                Name = body.Name,
                Description = body.Description,
                CodInstallation = body.CodInstallation,
                Cluster = clusterFound,
                User = user
            };

            await context.Installations.AddAsync(installation);
            await context.SaveChangesAsync();
            return Created($"installations/{installation.Id}", installation);
        }

        [HttpGet("installations")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var installations = await context.Installations.ToListAsync();
            return Ok(installations);
        }

        [HttpGet("installations/{installationId}")]
        public async Task<IActionResult> Get([FromRoute] string installationId, [FromServices] DataContext context)
        {
            var installation = await context.Installations.FirstOrDefaultAsync(x => x.Id.Equals(installationId));
            return Ok(installation);
        }
    }
}
