using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.ViewModels.Fields;

namespace PRIO.Controllers
{
    [ApiController]
    public class FieldController : ControllerBase
    {
        [HttpPost("fields")]
        public async Task<IActionResult> Create([FromBody] CreateFieldViewModel body, [FromServices] DataContext context)
        {
            var cluster = await context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);
            if (cluster is null)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = $"Cluster not found with the provided id: {body.ClusterId}"
                });

            var fieldInDatabase = await context.Fields.FirstOrDefaultAsync(x => x.CodField == body.CodField);
            if (fieldInDatabase is not null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Field with code: {body.CodField} already exists, try another code."
                });

            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            var field = new Field
            {
                Name = body.Name,
                User = user,
                Description = body.Description is not null ? body.Description : null,
                Basin = body.Basin,
                Location = body.Location,
                State = body.State,
                CodField = body.CodField,
            };

            await context.Fields.AddAsync(field);
            await context.SaveChangesAsync();


            return Created($"fields/{field.Id}", field);

        }

        [HttpGet("fields")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var fields = await context.Fields.Include(x => x.Reservoirs).ToListAsync();
            return Ok(fields);
        }
    }
}
