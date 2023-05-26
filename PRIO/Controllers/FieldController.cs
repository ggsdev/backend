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
            var userId = (Guid)HttpContext.Items["Id"]!;
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == userId);

            if (cluster is null)
                return BadRequest(new ErrorResponseDTO
                {
                    Message = $"Cluster not found with the provided id: {body.ClusterId}"
                });

            var field = new Field
            {
                Name = body.Name,
                Cluster = cluster,
                User = user,
                //Description = body.Description is not null ? body.Description : null,
                Basin = body.Basin,
                Location = body.Location,
                State = body.State,
                CodField = body.CodField,
            };

            await context.Fields.AddAsync(field);
            await context.SaveChangesAsync();


            return Created($"fields/{field.Id}", field);

        }
    }
}
