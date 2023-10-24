using Microsoft.AspNetCore.Mvc;

namespace PRIO.src.Shared
{
    [ApiController]
    [Route("mock/pi")]
    public class GetPIMocksController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string date)
        {
            await GetPIMocks.ExecuteAsync(date);
            return NoContent();
        }

    }
}
