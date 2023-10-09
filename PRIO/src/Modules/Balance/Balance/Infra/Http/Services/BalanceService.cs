﻿using AutoMapper;
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
                        DischargedSurface = fieldBalance.DischargedSurface is not null ? Math.Round(fieldBalance.DischargedSurface.Value, 5) : null,
                        DateBalance = fieldBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                        TotalWaterCaptured = fieldBalance.TotalWaterCaptured is not null ? Math.Round(fieldBalance.TotalWaterCaptured.Value, 5) : null,
                        TotalWaterDisposal = fieldBalance.TotalWaterDisposal is not null ? Math.Round(fieldBalance.TotalWaterDisposal.Value, 5) : null,
                        TotalWaterInjected = fieldBalance.TotalWaterInjected is not null ? Math.Round(fieldBalance.TotalWaterInjected.Value, 5) : null,
                        TotalWaterInjectedRS = fieldBalance.TotalWaterInjectedRS is not null ? Math.Round(fieldBalance.TotalWaterInjectedRS.Value, 5) : null,
                        TotalWaterProduced = fieldBalance.TotalWaterProduced,
                        TotalWaterReceived = fieldBalance.TotalWaterReceived is not null ? Math.Round(fieldBalance.TotalWaterReceived.Value, 5) : null,
                        TotalWaterTransferred = fieldBalance.TotalWaterTransferred is not null ? Math.Round(fieldBalance.TotalWaterTransferred.Value, 5) : null
                    });
                }
            }

            var status = true;

            foreach (var installationBalance in uepBalance.InstallationsBalance)
            {
                foreach (var fieldBalance in installationBalance.BalanceFields)
                {
                    if (!fieldBalance.Status)
                    {
                        status = false;
                        break;
                    }

                }

                if (!status)
                {
                    break;
                }

            }

            var result = new BalanceByDateDto
            {
                DischargedSurface = uepBalance.DischargedSurface is not null ? Math.Round(uepBalance.DischargedSurface.Value, 5) : null,
                DateBalance = uepBalance.MeasurementAt.ToString("dd/MMM/yyyy"),
                TotalWaterCaptured = uepBalance.TotalWaterCaptured is not null ? Math.Round(uepBalance.TotalWaterCaptured.Value, 5) : null,
                TotalWaterDisposal = uepBalance.TotalWaterDisposal is not null ? Math.Round(uepBalance.TotalWaterDisposal.Value, 5) : null,
                TotalWaterInjected = uepBalance.TotalWaterInjected is not null ? Math.Round(uepBalance.TotalWaterInjected.Value, 5) : null,
                TotalWaterInjectedRS = uepBalance.TotalWaterInjectedRS is not null ? Math.Round(uepBalance.TotalWaterInjectedRS.Value, 5) : null,
                TotalWaterProduced = uepBalance.TotalWaterProduced is not null ? Math.Round(uepBalance.TotalWaterProduced.Value, 5) : null,
                TotalWaterReceived = uepBalance.TotalWaterReceived is not null ? Math.Round(uepBalance.TotalWaterReceived.Value, 5) : null,
                TotalWaterTransferred = uepBalance.TotalWaterTransferred is not null ? Math.Round(uepBalance.TotalWaterTransferred.Value, 5) : null,
                UepBalanceId = uepBalance.Id,
                StatusBalance = status,
                FieldBalances = fieldBalances
            };

            return result;
        }
    }
}
