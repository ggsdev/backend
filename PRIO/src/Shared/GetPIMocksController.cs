using Microsoft.AspNetCore.Mvc;

namespace PRIO.src.Shared
{
    [ApiController]
    [Route("mock/pi")]
    public class GetPIMocksController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(string dateBeginning, string dateEnd)
        {
            await GetPIMocks.ExecuteAsync(dateBeginning, dateEnd);
            return NoContent();
        }

    }
}
