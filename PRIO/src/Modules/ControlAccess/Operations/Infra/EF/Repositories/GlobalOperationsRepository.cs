﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Operations.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Repositories
{
    public class GlobalOperationsRepository : IGlobalOperationsRepository
    {
        private readonly DataContext _context;
        public GlobalOperationsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<GlobalOperation> GetGlobalOperationByMetlhod(string operationName)
        {
            var foundOperation = await _context.GlobalOperations
                        .Where(x => x.Method == operationName)
                        .FirstOrDefaultAsync();

            return foundOperation;
        }
        public async Task<GlobalOperation> GetGlobalOperationById(Guid operationId)
        {
            var foundOperation = await _context.GlobalOperations.Where(x => x.Id == operationId).FirstOrDefaultAsync();

            return foundOperation;
        }

    }
}
