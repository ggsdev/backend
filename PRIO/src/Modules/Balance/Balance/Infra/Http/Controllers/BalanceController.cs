using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Balance.Balance.Infra.Http.Services;
using PRIO.src.Modules.Balance.Balance.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;
using System.Globalization;

namespace PRIO.src.Modules.Balance.Balance.Infra.Http.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(AuthorizationFilter))]
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

        //[HttpGet("balances/{balanceId}")]
        //public async Task<IActionResult> GetBalanceDatas([FromRoute] Guid balanceId)
        //{
        //    var data = await _service.GetDatasByBalanceId(balanceId);
        //    return Ok(data);
        //}

        //[HttpPatch("balances/{balanceId}")]
        //public async Task<IActionResult> UpdateBalanceDatas([FromRoute] Guid balanceId, [FromBody] UpdateListValuesViewModel values)
        //{
        //    var data = await _service.UpdateOperationalParameters(balanceId, values);
        //    return Ok(data);
        //}

        [HttpGet("ueps{id}/balances")]
        public async Task<IActionResult> GetBalancesByDate(string dateBalance, Guid id)
        {
            if (!DateTime.TryParseExact(dateBalance, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new BadRequestException("O formato da data deve ser dd-MM-yyyy");

            var data = await _service.GetByDateAndUepId(date, id);

            return Ok(data);
        }

        [HttpGet("balances")]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAllBalances();

            return Ok(data);
        }

        [HttpGet("balances/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.GetByUepBalanceId(id);

            return Ok(data);
        }

        [HttpPost("balances/{id}")]
        public async Task<IActionResult> CreateBalance(ManualValuesBalanceViewModel body, Guid id)
        {
            var data = await _service.InsertManualValuesBalance(body, id);

            return Ok(data);
        }

        [HttpPatch("balances/{id}")]
        public async Task<IActionResult> UpdateFieldBalance(UpdateManualValuesViewModel body, Guid id)
        {
            var data = await _service.UpdateBalance(body, id);

            return Ok(data);
        }
    }
}
