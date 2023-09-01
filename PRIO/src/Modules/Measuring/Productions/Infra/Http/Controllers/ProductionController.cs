using Microsoft.AspNetCore.Mvc;
using PRIO.src.Modules.Measuring.Productions.Infra.Http.Services;
using PRIO.src.Modules.Measuring.Productions.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.Http.Filters;
using System.Globalization;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Controllers
{
    [ApiController]
    [Route("productions")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class ProductionController : ControllerBase
    {
        private readonly ProductionService _productionService;

        public ProductionController(ProductionService service)
        {
            _productionService = service;
        }


        [HttpGet("total-daily")]
        public async Task<IActionResult> GetByDate([FromQuery] string date)
        {
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                throw new BadRequestException("Invalid date format. The date should be in the format 'dd/MM/yyyy'.");
            }

            var production = await _productionService.GetByDate(parsedDate);

            return Ok(production);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productions = await _productionService.GetAllProductions();
            return Ok(productions);

        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeleted()
        {
            var productions = await _productionService
                .GetDeletedProductions();

            return Ok(productions);

        }

        [HttpGet("{id}/files")]
        public async Task<IActionResult> DownloadFiles([FromRoute] Guid id)
        {
            var files = await _productionService
                .DownloadAllProductionFiles(id);

            return Ok(files);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduction([FromRoute] Guid id)
        {
            await _productionService
                .DeleteProduction(id);

            return NoContent();
        }

        [HttpPatch("{productionId}/gasDetailed")]
        public async Task<IActionResult> UpdateDetailedGas([FromRoute] Guid productionId, UpdateDetailedGasViewModel body)
        {
            var data = await _productionService.UpdateDetailedGas(productionId, body);

            return Ok(data);
        }
    }
}
