using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Dtos;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Balance.Balance.ViewModels;
using PRIO.src.Modules.Balance.Injection.Dtos;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.Balance.Injection.Interfaces;
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
        private readonly IInjectionRepository _injectionRepository;
        private readonly IMapper _mapper;
        public BalanceService(IInstallationRepository installationRepository, IBalanceRepository balanceRepository, IMapper mapper, IFieldRepository fieldRepository, IInjectionRepository injectionRepository)
        {
            _installationRepository = installationRepository;
            _fieldRepository = fieldRepository;
            _balanceRepository = balanceRepository;
            _injectionRepository = injectionRepository;
            _mapper = mapper;
            _injectionRepository = injectionRepository;
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

        public async Task<FieldBalanceDto> ConfirmBalance(Guid id)
        {
            var balance = await _balanceRepository.GetBalanceById(id);
            if (balance == null) throw new NotFoundException("Balanço não encontrado.");
            if (balance.IsParameterized)
                throw new ConflictException("Dados Operacionais já foram confirmados.");

            balance.IsParameterized = true;
            _balanceRepository.UpdateFieldBalance(balance);
            await _balanceRepository.Save();

            var FieldBalanceDTO = _mapper.Map<FieldsBalance, FieldBalanceWithParameterDTO>(balance);
            return FieldBalanceDTO;
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
            BuidAssigned(FieldDTO.Wells);

            return FieldDTO;
        }
        public async Task<WellSensorDTO> UpdateOperationalParameters(Guid balanceId, UpdateSensorDTO value)
        {
            var sensor = await _injectionRepository.GetSensorById(balanceId);
            if (sensor == null) throw new NotFoundException("Sensor não encontrado.");

            sensor.AssignedValue = value.Value;
            _injectionRepository.UpdateSensor(sensor);

            var sensorDTO = _mapper.Map<WellSensor, WellSensorDTO>(sensor);

            await _injectionRepository.Save();

            return sensorDTO;
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

        static void BuidAssigned(List<WellsWithInjectionsValuesDTO> wells)
        {
            foreach (var well in wells)
            {
                Console.WriteLine(well);
                foreach (var wellValue in well.WellsValues)
                {
                    Console.WriteLine(wellValue);
                    if (wellValue.InjectionGasWell is not null)
                    {
                        wellValue.Value.AssignedValue = wellValue.InjectionGasWell.AssignedValue;
                        wellValue.Value.AssignedId = wellValue.InjectionGasWell.Id;
                    }
                    else if (wellValue.InjectionWaterWell is not null)
                    {
                        wellValue.Value.AssignedValue = wellValue.InjectionWaterWell.AssignedValue;
                        wellValue.Value.AssignedId = wellValue.InjectionWaterWell.Id;
                    }
                    else if (wellValue.WellSensor is not null)
                    {
                        wellValue.Value.AssignedValue = wellValue.WellSensor.AssignedValue;
                        wellValue.Value.AssignedId = wellValue.WellSensor.Id;
                    }
                }
            }
        }
        public async Task<BalanceByDateDto> GetByDateAndUepId(DateTime dateBalance, Guid uepId)
        {
            var uepBalance = await _balanceRepository
                .GetUepBalance(uepId, dateBalance)
                ?? throw new NotFoundException("Produção do dia não foi fechada ainda.");

            _ = await _injectionRepository
                .GetWaterGasFieldInjectionByDate(dateBalance)
                ?? throw new BadRequestException("Injeção deve ser feita antes da criação do balanço.");

            if (uepBalance.Status)
                throw new ConflictException("Balanço do dia já foi criado.");

            var fieldBalances = new List<FieldBalanceDto>();

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var fieldBalance in installationBalance.BalanceFields)
                {
                    fieldBalances.Add(new FieldBalanceDto
                    {
                        FieldName = fieldBalance.Field.Name!,
                        FieldBalanceId = fieldBalance.Id,
                        DischargedSurface = Math.Round(fieldBalance.DischargedSurface, 5),
                        DateBalance = fieldBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                        TotalWaterCaptured = Math.Round(fieldBalance.TotalWaterCaptured, 5),
                        TotalWaterDisposal = Math.Round(fieldBalance.TotalWaterDisposal, 5),
                        TotalWaterInjected = Math.Round(fieldBalance.TotalWaterInjected, 5),
                        TotalWaterInjectedRS = Math.Round(fieldBalance.TotalWaterInjectedRS, 5),
                        TotalWaterProduced = Math.Round(fieldBalance.TotalWaterProduced, 5),
                        TotalWaterReceived = Math.Round(fieldBalance.TotalWaterReceived, 5),
                        TotalWaterTransferred = Math.Round(fieldBalance.TotalWaterTransferred, 5)
                    });
                }
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
                StatusBalance = uepBalance.Status,
                FieldBalances = fieldBalances,
                UepName = uepBalance.Uep.Name,
            };

            return result;
        }

        public async Task<BalanceByDateDto> InsertManualValuesBalance(ManualValuesBalanceViewModel body)
        {
            var uepBalance = await _balanceRepository
                .GetUepBalanceById(body.UepBalanceId)
                ?? throw new NotFoundException("Balanço da uep não encontrado.");

            if (body.FieldsBalances.Any() is false)
                throw new BadRequestException("'FieldsBalances' não pode estar vazio.");

            var fieldBalancesToUpdate = new List<FieldsBalance>();
            var fieldBalancesDto = new List<FieldBalanceDto>();

            var resultBalance = 0m;

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var bodyBalanceField in body.FieldsBalances)
                {
                    var balanceToUpdate = installationBalance.BalanceFields
                        .FirstOrDefault(x => x.Id == bodyBalanceField.FieldBalanceId)
                        ?? throw new NotFoundException("Balanço de campo não encontrado");

                    resultBalance += balanceToUpdate.TotalWaterProduced;
                    resultBalance -= balanceToUpdate.TotalWaterInjectedRS;
                    resultBalance -= balanceToUpdate.TotalWaterDisposal;
                    resultBalance += balanceToUpdate.TotalWaterReceived;
                    resultBalance += balanceToUpdate.TotalWaterCaptured;
                    resultBalance -= balanceToUpdate.DischargedSurface;
                    resultBalance -= balanceToUpdate.TotalWaterTransferred;

                    balanceToUpdate.Status = true;
                    balanceToUpdate.TotalWaterReceived = bodyBalanceField.TotalWaterReceived;
                    balanceToUpdate.DischargedSurface = bodyBalanceField.DischargedSurface;
                    balanceToUpdate.TotalWaterTransferred = bodyBalanceField.TotalWaterTransferred;
                    balanceToUpdate.TotalWaterCaptured = bodyBalanceField.TotalWaterCaptured;

                    fieldBalancesToUpdate.Add(balanceToUpdate);

                    fieldBalancesDto.Add(new FieldBalanceDto
                    {
                        FieldName = balanceToUpdate.Field.Name!,
                        FieldBalanceId = balanceToUpdate.Id,
                        DischargedSurface = Math.Round(balanceToUpdate.DischargedSurface, 5),
                        DateBalance = balanceToUpdate.MeasurementAt.ToString("dd/MMM/yyyy"),
                        TotalWaterCaptured = Math.Round(balanceToUpdate.TotalWaterCaptured, 5),
                        TotalWaterDisposal = Math.Round(balanceToUpdate.TotalWaterDisposal, 5),
                        TotalWaterInjected = Math.Round(balanceToUpdate.TotalWaterInjected, 5),
                        TotalWaterInjectedRS = Math.Round(balanceToUpdate.TotalWaterInjectedRS, 5),
                        TotalWaterProduced = Math.Round(balanceToUpdate.TotalWaterProduced, 5),
                        TotalWaterReceived = Math.Round(balanceToUpdate.TotalWaterReceived, 5),
                        TotalWaterTransferred = Math.Round(balanceToUpdate.TotalWaterTransferred, 5)
                    });
                }
            }

            if (resultBalance != 0m)
                throw new ConflictException($"Resultado do balanço deve ser zero. Valor final: {resultBalance}.");

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

            uepBalance.Status = status;

            _balanceRepository.UpdateUepBalance(uepBalance);

            _balanceRepository.UpdateRangeFieldBalances(fieldBalancesToUpdate);

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
                StatusBalance = uepBalance.Status,
                FieldBalances = fieldBalancesDto,
                UepName = uepBalance.Uep.Name,
            };

            await _balanceRepository.Save();

            return result;

        }

        public async Task<List<BalanceDto>> GetAllBalances()
        {
            var uepBalances = await _balanceRepository
                .GetAllBalances();

            var balancesDto = new List<BalanceDto>();

            foreach (var uepBalance in uepBalances)
            {
                balancesDto.Add(new BalanceDto
                    (
                        uepBalance.Id,
                        uepBalance.Uep.Name,
                        uepBalance.Uep.Id,
                        uepBalance.Status,
                        uepBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                        Math.Round(uepBalance.TotalWaterProduced, 5),
                        Math.Round(uepBalance.TotalWaterInjected, 5),
                        Math.Round(uepBalance.TotalWaterInjectedRS, 5),
                        Math.Round(uepBalance.TotalWaterDisposal, 5),
                        Math.Round(uepBalance.TotalWaterReceived, 5),
                        Math.Round(uepBalance.TotalWaterCaptured, 5),
                        Math.Round(uepBalance.DischargedSurface, 5),
                        Math.Round(uepBalance.TotalWaterTransferred, 5)
                    ));
            }


            return balancesDto;
        }

        public async Task<BalanceByDateDto> GetByUepBalanceId(Guid uepBalanceId)
        {
            var uepBalance = await _balanceRepository
                .GetUepBalanceById(uepBalanceId)
            ?? throw new NotFoundException("Balanço da uep não encontrado");

            var fieldBalancesDto = new List<FieldBalanceDto>();

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var fieldBalance in installationBalance.BalanceFields)
                {
                    fieldBalancesDto.Add(new FieldBalanceDto
                    {
                        FieldName = fieldBalance.Field.Name!,
                        FieldBalanceId = fieldBalance.Id,
                        DischargedSurface = Math.Round(fieldBalance.DischargedSurface, 5),
                        DateBalance = fieldBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                        TotalWaterCaptured = Math.Round(fieldBalance.TotalWaterCaptured, 5),
                        TotalWaterDisposal = Math.Round(fieldBalance.TotalWaterDisposal, 5),
                        TotalWaterInjected = Math.Round(fieldBalance.TotalWaterInjected, 5),
                        TotalWaterInjectedRS = Math.Round(fieldBalance.TotalWaterInjectedRS, 5),
                        TotalWaterProduced = Math.Round(fieldBalance.TotalWaterProduced, 5),
                        TotalWaterReceived = Math.Round(fieldBalance.TotalWaterReceived, 5),
                        TotalWaterTransferred = Math.Round(fieldBalance.TotalWaterTransferred, 5)
                    });
                }
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
                StatusBalance = uepBalance.Status,
                FieldBalances = fieldBalancesDto,
                UepName = uepBalance.Uep.Name,
            };

            return result;
        }

        public async Task<List<FieldBalanceDto>> UpdateBalance(UpdateManualValuesViewModel body, Guid fieldBalanceId)
        {
            var fieldBalancesToUpdate = new List<FieldsBalance>();
            var fieldBalancesDto = new List<FieldBalanceDto>();

            var resultBalance = 0m;

            foreach (var bodyBalanceField in body.FieldsBalances)
            {
                var balanceToUpdate = await _balanceRepository
                    .GetBalanceById(fieldBalanceId)
                ?? throw new NotFoundException("Balanço de campo não encontrado.");

                resultBalance += balanceToUpdate.TotalWaterProduced;
                resultBalance -= balanceToUpdate.TotalWaterInjectedRS;
                resultBalance -= balanceToUpdate.TotalWaterDisposal;
                resultBalance += balanceToUpdate.TotalWaterReceived;
                resultBalance += balanceToUpdate.TotalWaterCaptured;
                resultBalance -= balanceToUpdate.DischargedSurface;
                resultBalance -= balanceToUpdate.TotalWaterTransferred;

                balanceToUpdate.Status = true;
                balanceToUpdate.TotalWaterReceived = bodyBalanceField.TotalWaterReceived is not null ? bodyBalanceField.TotalWaterReceived.Value : balanceToUpdate.TotalWaterReceived;
                balanceToUpdate.DischargedSurface = bodyBalanceField.DischargedSurface is not null ? bodyBalanceField.DischargedSurface.Value : balanceToUpdate.DischargedSurface;
                balanceToUpdate.TotalWaterTransferred = bodyBalanceField.TotalWaterTransferred is not null ? bodyBalanceField.TotalWaterTransferred.Value : balanceToUpdate.TotalWaterTransferred;
                balanceToUpdate.TotalWaterCaptured = bodyBalanceField.TotalWaterCaptured is not null ? bodyBalanceField.TotalWaterCaptured.Value : balanceToUpdate.TotalWaterCaptured;

                fieldBalancesToUpdate.Add(balanceToUpdate);

                fieldBalancesDto.Add(new FieldBalanceDto
                {
                    FieldName = balanceToUpdate.Field.Name!,
                    FieldBalanceId = balanceToUpdate.Id,
                    DischargedSurface = Math.Round(balanceToUpdate.DischargedSurface, 5),
                    DateBalance = balanceToUpdate.MeasurementAt.ToString("dd/MMM/yyyy"),
                    TotalWaterCaptured = Math.Round(balanceToUpdate.TotalWaterCaptured, 5),
                    TotalWaterDisposal = Math.Round(balanceToUpdate.TotalWaterDisposal, 5),
                    TotalWaterInjected = Math.Round(balanceToUpdate.TotalWaterInjected, 5),
                    TotalWaterInjectedRS = Math.Round(balanceToUpdate.TotalWaterInjectedRS, 5),
                    TotalWaterProduced = Math.Round(balanceToUpdate.TotalWaterProduced, 5),
                    TotalWaterReceived = Math.Round(balanceToUpdate.TotalWaterReceived, 5),
                    TotalWaterTransferred = Math.Round(balanceToUpdate.TotalWaterTransferred, 5)
                });
            }

            if (resultBalance != 0m)
                throw new ConflictException($"Resultado do balanço deve ser zero. Valor final: {resultBalance}.");

            _balanceRepository.UpdateRangeFieldBalances(fieldBalancesToUpdate);

            await _balanceRepository.Save();

            return fieldBalancesDto;
        }
    }
}
