using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Balance.Balance.Infra.Http.Services;

namespace PRIO.src.Modules.Balance.Balance.Infra.Http.Controllers
{
    public class BalanceController : ControllerBase
    {
        private readonly BalanceService _service;

        public BalanceController(BalanceService balanceService)
        {
            _service = balanceService;
        }

        [HttpGet("installations/{installationId}/balances")]
        public async Task<IActionResult> GetParameters([FromRoute] Guid installationId)
        {
            var data = await _service.GetBalancesByInstallationId(installationId);
            return Ok(data);
        }
        [HttpGet("balances/{balanceId}")]
        public async Task<IActionResult> GetBalanceDatas([FromRoute] Guid balanceId)
        {
            var data = await _service.GetDatasByBalanceId(balanceId);
            return Ok(data);
        }
        //[HttpPatch("balances/{balanceId}")]
        //public async Task<IActionResult> UpdateBalanceDatas([FromRoute] Guid balanceId, [FromBody] UpdateListValuesViewModel values)
        //{
        //    var data = await _service.UpdateOperationalParameters(balanceId, values);
        //    return Ok(data);
        //}
    }
}
