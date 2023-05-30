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
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Installation with code: {body.CodInstallation} already exists, try another code."
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            var installation = new Installation
            {
                Name = body.Name,
                Description = body.Description,
                CodInstallation = body.CodInstallation,
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
    }
}
