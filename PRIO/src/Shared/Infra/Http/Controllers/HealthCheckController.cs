using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Shared.Infra.Http.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public HealthCheckController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHealth()
        {
            try
            {
                await _dbContext.Database.CanConnectAsync();
                return Ok(new { Message = "API is healthy" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Problem when connecting to database: " + ex.Message });
            }
        }
    }
}
