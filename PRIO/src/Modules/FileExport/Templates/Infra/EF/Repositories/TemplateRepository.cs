using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;
using PRIO.src.Modules.FileExport.Templates.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.FileExport.Templates.Infra.EF.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly DataContext _context;

        public TemplateRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Template>> GetAll()
        {
            return await _context.Templates
                .Select(x => new Template
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FileExtension = x.FileExtension,
                    TypeFile = x.TypeFile,
                    Group = x.Group,
                })
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<Template?> GetById(Guid? id)
        {
            return await _context.Templates
                .Select(x => new Template
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FileExtension = x.FileExtension,
                    TypeFile = x.TypeFile,
                    Group = x.Group,
                    FileContent = x.FileContent
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Template?> GetByType(TypeFile type)
        {
            return await _context.Templates
                .Select(x => new Template
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    FileExtension = x.FileExtension,
                    TypeFile = x.TypeFile,
                    Group = x.Group,
                    FileContent = x.FileContent
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TypeFile == type);
        }
    }
}
