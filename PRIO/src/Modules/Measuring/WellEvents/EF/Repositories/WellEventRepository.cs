﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;
using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Repositories
{
    public class WellEventRepository : IWellEventRepository
    {
        private readonly DataContext _context;
        public WellEventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<WellEvent?> GetById(Guid id)
        {
            return await _context.WellEvents
                .Include(x => x.EventReasons)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(WellEvent wellEvent)
        {
            await _context.WellEvents.AddAsync(wellEvent);
        }

        public void Update(WellEvent wellEvent)
        {
            _context.WellEvents.Update(wellEvent);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}