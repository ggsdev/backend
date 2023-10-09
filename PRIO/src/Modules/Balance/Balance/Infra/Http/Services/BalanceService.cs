﻿using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Dtos;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Errors;

namespace PRIO.src.Modules.Balance.Balance.Infra.Http.Services
{
    public class BalanceService
    {
        private readonly IInstallationRepository _installationRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;
        public BalanceService(IInstallationRepository installationRepository, IBalanceRepository balanceRepository, IMapper mapper)
        {
            _installationRepository = installationRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
        }

        public async Task<List<FieldsBalance>> GetBalancesByInstallationId(Guid installationId)
        {
            var intallations = await _installationRepository.GetInstallationByIdWithField(installationId);
            if (intallations == null)
                throw new NotFoundException("Instalação não encontrada");

            List<Guid> fieldIds = intallations.Fields.Select(f => f.Id).ToList();
            var fieldsBalances = await _balanceRepository.GetBalances(fieldIds);

            return fieldsBalances;
        }
        public async Task<FieldWithInjectionsValuesDTO> GetDatasByBalanceId(Guid balanceId)
        {
            var balance = await _balanceRepository.GetBalanceById(balanceId);
            if (balance == null) throw new NotFoundException("Balanço não encontrado.");

            Console.WriteLine(balance.MeasurementAt);
            var balanceDatas = await _balanceRepository.GetDatasByBalanceId(balance.FieldProduction.FieldId);
            if (balanceDatas == null)
                throw new NotFoundException("Dados não encontrados.");

            BuildDataBalance(balanceDatas, balance.MeasurementAt);
            var FieldDTO = _mapper.Map<Field, FieldWithInjectionsValuesDTO>(balanceDatas);

            return FieldDTO;
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
    }
}
