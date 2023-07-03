﻿using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Services
{
    public class InstallationService
    {
        private readonly IMapper _mapper;
        private readonly IInstallationRepository _installationRepository;
        private readonly IClusterRepository _clusterRespository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableInstallations;

        public InstallationService(IMapper mapper, IInstallationRepository installationRepository, IClusterRepository clusterRepository, SystemHistoryService systemHistoryService)
        {
            _mapper = mapper;
            _installationRepository = installationRepository;
            _clusterRespository = clusterRepository;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<CreateUpdateInstallationDTO> CreateInstallation(CreateInstallationViewModel body, User user)
        {

            var installationExistingCode = await _installationRepository
                .GetByCod(body.CodInstallationAnp);

            if (installationExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Installation>());

            var clusterInDatabase = await _clusterRespository
               .GetClusterByIdAsync(body.ClusterId);

            if (clusterInDatabase is null)
                throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

            var installationId = Guid.NewGuid();

            var installation = new Installation
            {
                Id = installationId,
                Name = body.Name,
                Description = body.Description,
                UepCod = body.UepCod,
                UepName = body.UepName,
                CodInstallationAnp = body.CodInstallationAnp,
                GasSafetyBurnVolume = body.GasSafetyBurnVolume,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _installationRepository.AddAsync(installation);

            await _systemHistoryService
                .Create<Installation, InstallationHistoryDTO>(_tableName, user, installationId, installation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<List<InstallationDTO>> GetInstallations()
        {
            var installations = await _installationRepository
                .GetAsync();

            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return installationsDTO;
        }

        public async Task<InstallationWithFieldsEquipmentsDTO> GetInstallationById(Guid id)
        {
            var installation = await _installationRepository.GetByIdWithFieldsMeasuringPointsAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            var installationDTO = _mapper.Map<Installation, InstallationWithFieldsEquipmentsDTO>(installation);

            return installationDTO;
        }

        public async Task<CreateUpdateInstallationDTO> UpdateInstallation(UpdateInstallationViewModel body, Guid id, User user)
        {
            var installation = await _installationRepository
                .GetByIdWithFieldsMeasuringPointsAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());


            if (installation.Fields.Count > 0)
                if (body.CodInstallationAnp is not null)
                    if (body.CodInstallationAnp != installation.CodInstallationAnp)
                        throw new ConflictException("Código da instalação não pode ser alterado.");

            var beforeChangesInstallation = _mapper.Map<InstallationHistoryDTO>(installation);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(installation, body);

            if (updatedProperties.Any() is false && (body.ClusterId is null || body.ClusterId == installation.Cluster?.Id))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Installation>());

            if (body.ClusterId is not null && installation.Cluster?.Id != body.ClusterId)
            {
                var clusterInDatabase = await _clusterRespository.GetClusterByIdAsync(body.ClusterId);

                if (clusterInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Cluster>());

                installation.Cluster = clusterInDatabase;
                updatedProperties[nameof(InstallationHistoryDTO.clusterId)] = clusterInDatabase.Id;
            }

            _installationRepository.Update(installation);

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, installation.Id, installation, beforeChangesInstallation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task DeleteInstallation(Guid id, User user)
        {
            var installation = await _installationRepository
                .GetByIdAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Installation>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(installation, propertiesUpdated);

            await _systemHistoryService
                .Delete<Installation, InstallationHistoryDTO>(_tableName, user, updatedProperties, installation.Id, installation);

            _installationRepository.Update(installation);

            await _installationRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateInstallationDTO> RestoreInstallation(Guid id, User user)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation is null)
                throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            if (installation.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Installation>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(installation, propertiesUpdated);

            await _systemHistoryService
                .Restore<Installation, InstallationHistoryDTO>(_tableName, user, updatedProperties, installation.Id, installation);

            _installationRepository.Update(installation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<List<SystemHistory>> GetInstallationHistory(Guid id)
        {
            var installationHistories = await _systemHistoryService
                .GetAll(id) ?? throw new NotFoundException(ErrorMessages.NotFound<Installation>());

            foreach (var history in installationHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ?
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return installationHistories;
        }
    }
}
