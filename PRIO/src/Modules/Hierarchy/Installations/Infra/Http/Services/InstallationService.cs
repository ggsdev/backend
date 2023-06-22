using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Interfaces;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.Http.Services
{
    public class InstallationService
    {
        private readonly IMapper _mapper;
        private readonly IInstallationRepository _installationRepository;
        private readonly ISystemHistoryRepository _systemHistoryRepository;
        private readonly IClusterRepository _clusterRespository;

        public InstallationService(IMapper mapper, IInstallationRepository installationRepository, ISystemHistoryRepository systemHistoryRepository, IClusterRepository clusterRepository)
        {
            _mapper = mapper;
            _installationRepository = installationRepository;
            _systemHistoryRepository = systemHistoryRepository;
            _clusterRespository = clusterRepository;
        }

        public async Task<CreateUpdateInstallationDTO> CreateInstallation(CreateInstallationViewModel body, User user)
        {
            var clusterInDatabase = await _clusterRespository
               .GetClusterByIdAsync(body.ClusterId);

            if (clusterInDatabase is null)
                throw new NotFoundException("Cluster not found");

            var installationId = Guid.NewGuid();

            var installation = new Installation
            {
                Id = installationId,
                Name = body.Name,
                Description = body.Description,
                UepCod = body.UepCod,
                CodInstallation = body.CodInstallation,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _installationRepository.AddAsync(installation);

            var currentData = _mapper.Map<Installation, InstallationHistoryDTO>(installation);
            currentData.createdAt = DateTime.UtcNow;
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableInstallations,
                TypeOperation = HistoryColumns.Create,
                CreatedBy = user?.Id,
                TableItemId = installationId,
                CurrentData = currentData,
            };

            await _systemHistoryRepository.AddAsync(history);

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

        public async Task<InstallationDTO> GetInstallationById(Guid id)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation is null)
                throw new NotFoundException("Installation not found");

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<CreateUpdateInstallationDTO> UpdateInstallation(UpdateInstallationViewModel body, Guid id, User user)
        {
            var installation = await _installationRepository
                .GetByIdAsync(id);

            if (installation is null)
                throw new NotFoundException("Installation not found");

            var beforeChangesInstallation = _mapper.Map<InstallationHistoryDTO>(installation);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(installation, body);

            if (updatedProperties.Any() is false && installation.Cluster?.Id == body.ClusterId)
                throw new BadRequestException("This installation already has these values, try to update to other values.");

            if (body?.ClusterId is not null)
            {
                var clusterInDatabase = await _clusterRespository.GetClusterByIdAsync(body.ClusterId);

                if (clusterInDatabase is null)
                    throw new NotFoundException("Cluster not found");

                installation.Cluster = clusterInDatabase;
                updatedProperties[nameof(InstallationHistoryDTO.clusterId)] = clusterInDatabase.Id;
            }

            _installationRepository.Update(installation);

            var firstHistory = await _systemHistoryRepository.GetFirst(id);

            var changedFields = UpdateFields.DictionaryToObject(updatedProperties);

            var currentData = _mapper.Map<Installation, InstallationHistoryDTO>(installation);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableInstallations,
                TypeOperation = HistoryColumns.Update,
                CreatedBy = firstHistory?.CreatedBy,
                UpdatedBy = user?.Id,
                TableItemId = installation.Id,
                FieldsChanged = changedFields,
                CurrentData = currentData,
                PreviousData = beforeChangesInstallation,
            };

            await _systemHistoryRepository.AddAsync(history);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task DeleteInstallation(Guid id, User user)
        {
            var installation = await _installationRepository
                .GetByIdAsync(id);

            if (installation is null || installation.IsActive is false)
                throw new NotFoundException("Installation not found or inactive already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            installation.IsActive = false;
            installation.DeletedAt = DateTime.UtcNow;

            var currentData = _mapper.Map<Installation, InstallationHistoryDTO>(installation);
            currentData.updatedAt = (DateTime)installation.DeletedAt;
            currentData.deletedAt = installation.DeletedAt;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableInstallations,
                TypeOperation = HistoryColumns.Delete,
                CreatedBy = installation.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = installation.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    installation.IsActive,
                    installation.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _installationRepository.Update(installation);

            await _installationRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateInstallationDTO> RestoreInstallation(Guid id, User user)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation is null || installation.IsActive is true)
                throw new NotFoundException("Installation not found or active already");

            var lastHistory = await _systemHistoryRepository.GetLast(id);

            installation.IsActive = true;
            installation.DeletedAt = null;

            var currentData = _mapper.Map<Installation, InstallationHistoryDTO>(installation);
            currentData.updatedAt = DateTime.UtcNow;

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableInstallations,
                TypeOperation = HistoryColumns.Restore,
                CreatedBy = installation.User?.Id,
                UpdatedBy = user?.Id,
                TableItemId = installation.Id,
                CurrentData = currentData,
                PreviousData = lastHistory?.CurrentData,
                FieldsChanged = new
                {
                    installation.IsActive,
                    installation.DeletedAt,
                }
            };

            await _systemHistoryRepository.AddAsync(history);

            _installationRepository.Update(installation);

            await _installationRepository.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<List<SystemHistory>> GetInstallationHistory(Guid id, User user)
        {
            var installationHistories = await _systemHistoryRepository.GetAll(id);

            if (installationHistories is null)
                throw new NotFoundException("Installation not found");

            foreach (var history in installationHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return installationHistories;
        }
    }
}
