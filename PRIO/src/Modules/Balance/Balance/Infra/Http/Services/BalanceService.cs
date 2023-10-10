using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Dtos;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Balance.Balance.ViewModels;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Fields.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Balance.Balance.Infra.Http.Services
{
    public class BalanceService
    {
        private readonly IInstallationRepository _installationRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;
        public BalanceService(IInstallationRepository installationRepository, IBalanceRepository balanceRepository, IMapper mapper, IFieldRepository fieldRepository)
        {
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
        }

        public async Task<List<FieldsBalanceDTO>> GetBalancesByInstallationId(Guid installationId)
        {
            var intallations = await _installationRepository.GetInstallationByIdWithField(installationId);
            if (intallations == null)
                throw new NotFoundException("Instalação não encontrada");

            List<Guid> fieldIds = intallations.Fields.Select(f => f.Id).ToList();
            var fieldsBalances = await _balanceRepository.GetBalances(fieldIds);
            var fieldsBalanceDTO = _mapper.Map<List<FieldsBalance>, List<FieldsBalanceDTO>>(fieldsBalances);
            await InputFieldName(fieldsBalanceDTO);

            return fieldsBalanceDTO;
        }
        public async Task<FieldWithInjectionsValuesDTO> GetDatasByBalanceId(Guid balanceId)
        {
            var balance = await _balanceRepository.GetBalanceById(balanceId);
            if (balance == null) throw new NotFoundException("Balanço não encontrado.");

            var balanceDatas = await _balanceRepository.GetDatasByBalanceId(balance.FieldProduction.FieldId);
            if (balanceDatas == null)
                throw new NotFoundException("Dados não encontrados.");

            BuildDataBalance(balanceDatas, balance.MeasurementAt);
            var FieldDTO = _mapper.Map<Field, FieldWithInjectionsValuesDTO>(balanceDatas);
            FilterActiveManualConfigurations(FieldDTO.Wells);

            return FieldDTO;
        }
        public async Task UpdateOperationalParameters(Guid balanceId, UpdateListValuesViewModel values)
        {
            //var balance = await _balanceRepository.GetBalanceById(balanceId);
            //if (balance == null) throw new NotFoundException("Balanço não encontrado.");

            //var list = new List<InjectionWaterWellDTO>()
            //foreach (var value in values) { }
        }
        private async Task InputFieldName(List<FieldsBalanceDTO> fieldsBalanceDTO)
        {
            foreach (var fieldBalance in fieldsBalanceDTO)
            {
                var field = await _fieldRepository.GetOnlyField(fieldBalance.FieldId);

                fieldBalance.FieldName = field.Name;
            }
        }
        static void BuildDataBalance(Field balanceDatas, DateTime measuredAt)
        {
            if (balanceDatas != null)
            {
                if (balanceDatas.Wells is not null)
                    balanceDatas.Wells = balanceDatas.Wells.Where(w => w.WellsValues.Count > 0).ToList();

                foreach (var well in balanceDatas.Wells)
                {
                    well.WellsValues = well.WellsValues.Where(wv => wv.Value.Date.Date == measuredAt.Date).ToList();

                }
            }
        }
        static void FilterActiveManualConfigurations(List<WellsWithInjectionsValuesDTO> wells)
        {
            foreach (var well in wells)
            {
                if (well.CategoryOperator.ToUpper().Contains("PRODUTOR"))
                {
                    var countProductivity = well.ManualWellConfiguration.ProductivityIndex
                        .Where(bu => bu.IsActive)
                        .ToList();
                    var countBuildUp = well.ManualWellConfiguration.BuildUp
                        .Where(bu => bu.IsActive)
                        .ToList();

                    well.ManualWellConfiguration.ProductivityIndex = countProductivity.Count() == 0 ? well.ManualWellConfiguration.ProductivityIndex.ToList() : countProductivity;
                    well.ManualWellConfiguration.BuildUp = countBuildUp.Count() == 0 ? well.ManualWellConfiguration.BuildUp.ToList() : countBuildUp;

                    well.ManualWellConfiguration.InjectivityIndex = null;

                }
                else if (well.CategoryOperator.ToUpper().Contains("INJETOR"))
                {
                    var countInjectivity = well.ManualWellConfiguration.InjectivityIndex
                       .Where(bu => bu.IsActive)
                       .ToList();
                    var countBuildUp = well.ManualWellConfiguration.BuildUp
                        .Where(bu => bu.IsActive)
                        .ToList();

                    well.ManualWellConfiguration.InjectivityIndex = countInjectivity.Count() == 0 ? well.ManualWellConfiguration.InjectivityIndex.ToList() : countInjectivity;
                    well.ManualWellConfiguration.BuildUp = countBuildUp.Count() == 0 ? well.ManualWellConfiguration.BuildUp.ToList() : countBuildUp;

                    well.ManualWellConfiguration.ProductivityIndex = null;
                }
            }
        }

        public async Task<BalanceByDateDto> GetByDateAndUepId(DateTime dateBalance, Guid uepId)
        {
            var uepBalance = await _balanceRepository
                .GetUepBalance(uepId, dateBalance)
                ?? throw new NotFoundException("Balanço da UEP não encontrado.");

            var fieldBalances = new List<FieldBalanceDto>();

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var fieldBalance in installationBalance.BalanceFields)
                {
                    fieldBalances.Add(new FieldBalanceDto
                    {
                        FieldBalanceId = fieldBalance.Id,
                        DischargedSurface = Math.Round(fieldBalance.DischargedSurface, 5),
                        DateBalance = fieldBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                        TotalWaterCaptured = Math.Round(fieldBalance.TotalWaterCaptured, 5),
                        TotalWaterDisposal = Math.Round(fieldBalance.TotalWaterDisposal, 5),
                        TotalWaterInjected = Math.Round(fieldBalance.TotalWaterInjected, 5),
                        TotalWaterInjectedRS = Math.Round(fieldBalance.TotalWaterInjectedRS, 5),
                        TotalWaterProduced = fieldBalance.TotalWaterProduced,
                        TotalWaterReceived = Math.Round(fieldBalance.TotalWaterReceived, 5),
                        TotalWaterTransferred = Math.Round(fieldBalance.TotalWaterTransferred, 5)
                    });
                }
            }

            var status = true;

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var fieldBalance in installationBalance.BalanceFields)
                    if (!fieldBalance.Status)
                    {
                        status = false;
                        break;
                    }

                if (!status)
                    break;
            }

            var result = new BalanceByDateDto
            {
                DischargedSurface = Math.Round(uepBalance.DischargedSurface, 5),
                DateBalance = uepBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                TotalWaterCaptured = Math.Round(uepBalance.TotalWaterCaptured, 5),
                TotalWaterDisposal = Math.Round(uepBalance.TotalWaterDisposal, 5),
                TotalWaterInjected = Math.Round(uepBalance.TotalWaterInjected, 5),
                TotalWaterInjectedRS = Math.Round(uepBalance.TotalWaterInjectedRS, 5),
                TotalWaterProduced = Math.Round(uepBalance.TotalWaterProduced, 5),
                TotalWaterReceived = Math.Round(uepBalance.TotalWaterReceived, 5),
                TotalWaterTransferred = Math.Round(uepBalance.TotalWaterTransferred, 5),
                UepBalanceId = uepBalance.Id,
                StatusBalance = status,
                FieldBalances = fieldBalances
            };

            return result;
        }
    }
}
