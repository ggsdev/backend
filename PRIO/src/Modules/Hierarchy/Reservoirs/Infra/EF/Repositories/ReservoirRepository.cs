﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Reservoirs.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Reservoirs.Infra.EF.Repositories
{
    public class ReservoirRepository : IReservoirRepository
    {
        private readonly DataContext _context;
        public ReservoirRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Reservoir?> GetByIdAsync(Guid? id)
        {
            return await _context.Reservoirs
               .Include(x => x.User)
               .Include(x => x.Zone)
               .ThenInclude(x => x!.Field)
               .ThenInclude(x => x!.Installation)
               .ThenInclude(x => x!.Cluster)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Reservoir?> GetByNameAsync(string? name)
        {
            return await _context.Reservoirs
               .Include(x => x.User)
               .Include(x => x.Zone)
               .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Reservoir?> GetWithZoneAsync(Guid? id)
        {
            return await _context.Reservoirs
                  .Include(x => x.Zone)
                  .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Reservoir?> GetWithZoneFieldAsync(Guid? id)
        {
            return await _context.Reservoirs
                  .Include(x => x.Zone)
                .ThenInclude(z => z.Field)
                  .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Reservoir?> GetReservoirAndChildren(Guid? id)
        {
            return await _context.Reservoirs
                                .Include(r => r.Completions)
                                    .ThenInclude(c => c.Well)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Reservoir?> GetByIdWithCompletionsAsync(Guid? id)
        {
            return await _context.Reservoirs
                .Include(x => x.User)
                .Include(x => x.Zone)
                .Include(x => x.Completions)
                .ThenInclude(c => c.Well)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Reservoir?> GetOnlyReservoirAsync(Guid? id)
        {
            return await _context.Reservoirs
                  .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Reservoir reservoir)
        {
            await _context.Reservoirs.AddAsync(reservoir);
        }

        public async Task<List<Reservoir>> GetAsync(User user)
        {
            List<Guid> installationIds = user.InstallationsAccess
                 .Select(installationAccess => installationAccess.Installation.Id)
                 .ToList();

            if (user.Type == "Master")
            {
                return await _context.Reservoirs
                   .Include(x => x.User)
                   .Include(x => x.Zone)
                   .ThenInclude(x => x!.Field)
                   .ThenInclude(x => x!.Installation)
                   .ThenInclude(x => x!.Cluster)
                   .ToListAsync();
            }
            else
            {
                return await _context.Reservoirs
                   .Include(x => x.User)
                   .Include(x => x.Zone)
                   .ThenInclude(x => x!.Field)
                   .ThenInclude(x => x!.Installation)
                   .ThenInclude(x => x!.Cluster)
                    .Where(x => installationIds.Contains(x.Zone.Field.Installation.Id)).ToListAsync();
            }
        }

        public void Update(Reservoir reservoir)
        {
            _context.Reservoirs.Update(reservoir);
        }

        public void Delete(Reservoir reservoir)
        {
            _context.Reservoirs.Update(reservoir);
        }

        public void Restore(Reservoir reservoir)
        {
            _context.Reservoirs.Update(reservoir);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
