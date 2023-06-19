using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.InstallationDTOS;
using PRIO.DTOS.HistoryDTOS;
using PRIO.Exceptions;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Installations;

namespace PRIO.Services.HierarchyServices
{
    public class InstallationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InstallationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateUpdateInstallationDTO> CreateInstallation(CreateInstallationViewModel body, User user)
        {
            var clusterInDatabase = await _context.Clusters
               .FirstOrDefaultAsync(x => x.Id == body.ClusterId);

            if (clusterInDatabase is null)
                throw new NotFoundException("Cluster not found");

            var installationId = Guid.NewGuid();

            var installation = new Installation
            {
                Id = installationId,
                Name = body.Name,
                Description = body.Description,
                CodInstallationUep = body.CodInstallationUep,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Installations.AddAsync(installation);

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

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<List<InstallationDTO>> GetInstallations()
        {
            var installations = await _context.Installations
                .Include(x => x.Cluster)
                .Include(x => x.User)
                .ToListAsync();

            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return installationsDTO;
        }

        public async Task<InstallationDTO> GetInstallationById(Guid id)
        {
            var installation = await _context.Installations
                 .Include(x => x.User)
                 .Include(x => x.Cluster)
                 .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null)
                throw new NotFoundException("Installation not found");

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<CreateUpdateInstallationDTO> UpdateInstallation(UpdateInstallationViewModel body, Guid id, User user)
        {
            var installation = await _context.Installations
                .Include(x => x.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null)
                throw new NotFoundException("Installation not found");

            var beforeChangesInstallation = _mapper.Map<InstallationHistoryDTO>(installation);

            var updatedProperties = UpdateFields.CompareAndUpdateInstallation(installation, body);

            if (updatedProperties.Any() is false && installation.Cluster?.Id == body.ClusterId)
                throw new BadRequestException("This installation already has these values, try to update to other values.");

            if (body.ClusterId is not null)
            {
                var clusterInDatabase = await _context.Clusters
              .FirstOrDefaultAsync(x => x.Id == body.ClusterId);

                if (clusterInDatabase is null)
                    throw new NotFoundException("Cluster not found");

                installation.Cluster = clusterInDatabase;
                updatedProperties[nameof(InstallationHistoryDTO.clusterId)] = clusterInDatabase.Id;
            }

            _context.Installations.Update(installation);

            var firstHistory = await _context.SystemHistories
               .OrderBy(x => x.CreatedAt)
               .Where(x => x.TableItemId == id)
               .FirstOrDefaultAsync();

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

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task DeleteInstallation(Guid id)
        {
            var installation = await _context.Installations
                .Include(x => x.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null || installation.IsActive is false)
                throw new NotFoundException("Installation not found or inactive already");

            var lastHistory = await _context.SystemHistories
                .OrderBy(x => x.CreatedAt)
                .Where(x => x.TableItemId == installation.Id)
                .LastOrDefaultAsync();

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
            await _context.SystemHistories.AddAsync(history);

            _context.Installations.Update(installation);

            await _context.SaveChangesAsync();
        }

        public async Task<CreateUpdateInstallationDTO> RestoreInstallation(Guid id, User user)
        {
            var installation = await _context.Installations
               .Include(x => x.User)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null || installation.IsActive is true)
                throw new NotFoundException("Installation not found or active already");

            var lastHistory = await _context.SystemHistories
               .Where(x => x.TableItemId == installation.Id)
               .OrderBy(x => x.CreatedAt)
               .LastOrDefaultAsync();

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

            await _context.SystemHistories.AddAsync(history);

            _context.Installations.Update(installation);

            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return installationDTO;
        }

        public async Task<List<SystemHistory>> GetInstallationHistory(Guid id, User user)
        {
            var installationHistories = await _context.SystemHistories
                 .Where(x => x.TableItemId == id)
                 .OrderByDescending(x => x.CreatedAt)
                 .ToListAsync();

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
