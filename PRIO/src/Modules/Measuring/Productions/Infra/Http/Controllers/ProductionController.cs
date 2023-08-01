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


        [HttpGet]
        public async Task<IActionResult> GetByDate([FromBody] GetProductionByDateViewModel body)
        {
            if (!DateTime.TryParseExact(body.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                throw new BadRequestException("Formato de data inválido. Formato correto: 'dd/MM/yyyy'.");

            var production = await _productionService.GetByDate(date);

            return Ok(production);
        }

    }
}
