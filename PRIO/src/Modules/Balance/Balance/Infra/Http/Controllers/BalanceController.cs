using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Balance.Balance.Infra.Http.Services;
using PRIO.src.Shared.Errors;
using System.Globalization;

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

        [HttpGet("balances/{balanceId}/parameters")]
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

        [HttpGet("balances")]
        public async Task<IActionResult> GetBalancesByDate(string dateBalance, Guid uepId)
        {
            if (!DateTime.TryParseExact(dateBalance, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new BadRequestException("O formato da data deve ser dd-MM-yyyy");

            var data = await _service.GetByDateAndUepId(date, uepId);

            return Ok(data);
        }
    }
}
