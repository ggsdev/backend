using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.FileImport.XML.FileContent._039;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Repositories
{
    public class InstallationRepository : IInstallationRepository
    {
        private readonly DataContext _context;
        public InstallationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Installation?> GetInstallationMeasurement039ByUepAndAnpCodAsync(DADOS_BASICOS_039 basicData, string acronym)
        {
            switch (acronym)
            {
                case "039":
                    {
                        return await _context.Installations
                         .FirstOrDefaultAsync(x => x.UepCod == basicData.DHA_COD_INSTALACAO_039 && x.CodInstallationAnp == basicData.DHA_COD_INSTALACAO_039);
                    }

                case "001":
                    return await _context.Installations
                        .FirstOrDefaultAsync(x => x.UepCod == basicData.DHA_COD_INSTALACAO_039 && x.CodInstallationAnp == basicData.DHA_COD_INSTALACAO_039);

            }

            return await _context.Installations
                        .FirstOrDefaultAsync(x => x.UepCod == basicData.DHA_COD_INSTALACAO_039 && x.CodInstallationAnp == basicData.DHA_COD_INSTALACAO_039);
        }

        public async Task<Installation?> GetByIdAsync(Guid? id)
        {
            return await _context.Installations
                .Include(x => x.User)
                .Include(x => x.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Installation?> GetByCod(string? cod)
        {
            return await _context.Installations
              .FirstOrDefaultAsync(x => x.CodInstallationAnp == cod);
        }

        public async Task<Installation?> GetByIdWithFieldsMeasuringPointsAsync(Guid? id)
        {
            return await _context.Installations
                .Include(x => x.Fields)
                .Include(x => x.MeasuringPoints)
                .Include(x => x.Cluster)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Installation installation)
        {
            await _context.Installations.AddAsync(installation);
        }

        public async Task<List<Installation>> GetAsync()
        {
            return await _context.Installations
                .Include(x => x.Cluster)
                .Include(x => x.User)
                .ToListAsync();
        }

        public void Update(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public void Delete(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public void Restore(Installation installation)
        {
            _context.Installations.Update(installation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
