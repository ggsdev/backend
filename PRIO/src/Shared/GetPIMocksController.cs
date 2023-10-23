using Microsoft.AspNetCore.Mvc;

namespace PRIO.src.Shared
{
    [ApiController]
    [Route("mock/pi")]
    public class GetPIMocksController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await GetPIMocks.ExecuteAsync();
            return NoContent();
        }

    }
}
