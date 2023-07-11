using dotenv.net;
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
            var env = DotEnv.Read();
            var versionEnv = env.TryGetValue("SOFTWARE_VERSION", out var version);

            if (versionEnv is false)
                version = "0.0.0";

            try
            {
                var safe = await _dbContext.Database.CanConnectAsync();
                if (safe is false)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Problema ao se conectar com o banco de dados", Version = version, ConnectedToDatabase = false });

                return Ok(new { Message = "API saudável", Version = version, ConnectedToDatabase = true });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Problema ao se conectar com o banco de dados", Version = version, ConnectedToDatabase = false });
            }
        }
    }
}
