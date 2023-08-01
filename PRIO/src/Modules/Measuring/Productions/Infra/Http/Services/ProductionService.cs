using AutoMapper;
using PRIO.src.Modules.Measuring.Productions.Dtos;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.Measuring.Productions.Utils;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Measuring.Productions.Infra.Http.Services
{
    public class ProductionService
    {
        private readonly IProductionRepository _repository;
        private readonly IMapper _mapper;
        public ProductionService(IProductionRepository productionRepository, IMapper mapper)
        {
            _repository = productionRepository;
            _mapper = mapper;

        }

        public async Task<ProductionDto> GetByDate(DateTime date)
        {
            var production = await _repository.GetExistingByDate(date.ToString("dd/mm/yyyy"));
            if (production is null)
                throw new NotFoundException($"Produção na data: {date.ToString("dd/mm/yyyy")} não encontrada");

            var productionDto = new ProductionDto
            {
                TotalGasBBL = (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas * ProductionUtils.m3ToBBLConversionMultiplier : 0),
                TotalGasM3 = (production.GasDiferencial is not null ? production.GasDiferencial.TotalGas : 0) + (production.GasLinear is not null ? production.GasLinear.TotalGas : 0),

                TotalOilBBL = production.Oil is not null ? production.Oil.TotalOil : 0,
                TotalOilM3 = production.Oil is not null ? production.Oil.TotalOil * ProductionUtils.m3ToBBLConversionMultiplier : 0,
            };

            return productionDto;
        }
    }
}
