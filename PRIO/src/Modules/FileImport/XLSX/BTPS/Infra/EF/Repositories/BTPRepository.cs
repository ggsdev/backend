using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;
using PRIO.src.Shared.Infra.EF;
using System.Globalization;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Repositories
{
    public class BTPRepository : IBTPRepository
    {
        private readonly DataContext _context;

        public BTPRepository(DataContext context)
        {
            _context = context;
        }

        #region BTP
        public async Task<List<BTP>> GetAllBTPsAsync()
        {
            return await _context.BTPs.ToListAsync();
        }
        public async Task AddBTPAsync(BTP btp)
        {
            await _context.BTPs.AddAsync(btp);
        }
        public async Task<Auxiliary?> GetTypeAsync(string type)
        {
            return await _context.Auxiliaries.Where(x => x.Table == "Teste" && x.Select == "Tipo de Teste" && x.Option == type).FirstOrDefaultAsync();
        }
        public async Task<List<BTP>> GetAllBTPsByTypeAsync(string type)
        {
            return await _context.BTPs.Where(x => x.Type == type).ToListAsync();
        }
        public async Task<BTP?> GetByIdAsync(Guid id)
        {
            return await _context.BTPs.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<BTP?> GetByNameOrContent(string name, string content)
        {
            return _context.BTPs.Where(x => x.Name == name).AsEnumerable().FirstOrDefault(x => string.Equals(x.FileContent, content, StringComparison.Ordinal));
        }
        #endregion

        #region BTPData
        public async Task<List<BTPData>> GetAllBTPsDataAsync()
        {
            return await _context.BTPDatas.Include(x => x.Well).Include(x => x.BTPBase64).ThenInclude(x => x.User).ToListAsync();
        }
        public async Task<List<BTPData>?> ListBTPSDataActiveByWellId(Guid wellId)
        {
            var data = await _context.BTPDatas
                      .Include(x => x.Well)
                      .Where(x => x.Well.Id == wellId)
                      .ToListAsync();

            var sortedData = data
                .OrderByDescending(x => DateTime.ParseExact(x.ApplicationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                .ToList();

            return sortedData;
        }
        public async Task<BTPData?> GetByWellAndDateXls(Guid wellId, string dateXls)
        {
            return await _context.BTPDatas.Include(x => x.Well).Where(x => x.Well.Id == wellId && x.FinalDate == dateXls).FirstOrDefaultAsync();
        }
        public async Task<BTPData?> GetByWellAndLastDate(Guid wellId, string FinalDate)
        {
            return await _context.BTPDatas.Include(x => x.Well).Where(x => x.Well.Id == wellId && x.FinalApplicationDate == FinalDate).FirstOrDefaultAsync();
        }
        public async Task<BTPData?> GetByWellAndApplicationDateXls(Guid wellId, string appDateXls)
        {
            return await _context.BTPDatas.Include(x => x.Well).Where(x => x.Well.Id == wellId && x.ApplicationDate == appDateXls).FirstOrDefaultAsync();
        }
        public async Task<List<BTPData>> GetAllBTPsDataByWellIdAsync(Guid wellId)
        {
            return await _context.BTPDatas.Include(x => x.Well).Where(x => x.Well.Id == wellId).Include(x => x.BTPBase64).ThenInclude(x => x.User).ToListAsync();
        }
        public async Task<BTPData?> GetAllBTPsDataByDataIdAsync(Guid dataId)
        {
            return await _context.BTPDatas.Include(x => x.Well).Include(x => x.BTPBase64).ThenInclude(x => x.User).Where(x => x.Id == dataId).FirstOrDefaultAsync();
        }
        public async Task<BTPData?> GetByDataIdAsync(Guid id)
        {
            return await _context.BTPDatas.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<BTPData?> GetByDateAsync(string date, Guid wellId)
        {
            return await _context.BTPDatas
                .Include(x => x.BTPBase64)
                .ThenInclude(x => x.User)
                .Include(x => x.Well)
                .Where(x => x.ApplicationDate == date)
                .Where(x => x.Well.Id == wellId)
                .FirstOrDefaultAsync();
        }
        public async Task AddBTPAsync(BTPData data)
        {
            await _context.BTPDatas.AddAsync(data);
        }
        #endregion

        #region Base64
        public async Task AddBTPBase64Async(BTPBase64 data)
        {
            await _context.BTPBases64.AddAsync(data);
        }
        #endregion

        #region Validate
        public async Task AddBTPValidateAsync(ValidateBTP data)
        {
            await _context.Validates.AddAsync(data);
        }
        public async Task RemoveValidate(ValidateBTP validate)
        {
            _context.Validates.Remove(validate);
        }
        public async Task<ValidateBTP?> GetValidate(Guid WellId, Guid BTPId, Guid ContentId, Guid DataId)
        {
            return await _context.Validates
                .Where(x => x.WellId == WellId)
                .Where(x => x.BTPId == BTPId)
                .Where(x => x.ContentId == ContentId)
                .Where(x => x.DataId == DataId)
                .FirstOrDefaultAsync();
        }
        #endregion

        //public async Task<List<BTPData>> SumFluidTotalPotencialByFieldId(Guid fieldId, string fluid)
        //{
        //    switch (fluid)
        //    {
        //        case "oil":
        //            return await _context.BTPDatas
        //      .Include(x => x.Well)
        //    .ThenInclude(x => x.Field)
        //    .Where(x => x.Well.Field.Id == fieldId && x.IsValid)
        //      //.Where(x => (x.FinalDate == null && DateTime.Parse(x.ApplicationDate) <= productionDate.Date) || (x.FinalDate != null &&
        //      //    DateTime.Parse(x.FinalDate) >= productionDate.Date &&
        //      //    DateTime.Parse(x.ApplicationDate) <= productionDate.Date))
        //      //.SumAsync(x => x.PotencialOil);
        //      .ToListAsync();

        //        case "gas":
        //            return await _context.BTPDatas
        //       .Include(x => x.Well)
        //    .ThenInclude(x => x.Field)
        //    .Where(x => x.Well.Field.Id == fieldId && x.IsValid)
        //       //.Where(x => (x.FinalDate == null && DateTime.Parse(x.ApplicationDate) <= productionDate.Date) || (x.FinalDate != null &&
        //       //    DateTime.Parse(x.FinalDate) >= productionDate.Date &&
        //       //    DateTime.Parse(x.ApplicationDate) <= productionDate.Date))
        //       //.SumAsync(x => x.PotencialGas);
        //       .ToListAsync();

        //        case "water":
        //            return await _context.BTPDatas
        //      .Include(x => x.Well)
        //    .ThenInclude(x => x.Field)
        //    .Where(x => x.Well.Field.Id == fieldId && x.IsValid)
        //      //.Where(x => (x.FinalDate == null && DateTime.Parse(x.ApplicationDate) <= productionDate.Date) || (x.FinalDate != null &&
        //      //    DateTime.Parse(x.FinalDate) >= productionDate.Date &&
        //      //    DateTime.Parse(x.ApplicationDate) <= productionDate.Date))
        //      //.SumAsync(x => x.PotencialWater);
        //      .ToListAsync();

        //        default:
        //            throw new BadRequestException("water, oil, gas");
        //    }
        //}

        public async Task<List<BTPData>> GetBtpDatasByFieldId(Guid fieldId)
        {
            var btps = await _context.BTPDatas
        .Include(x => x.Well)
            .ThenInclude(x => x.Field)
            .Where(x => x.Well.Field.Id == fieldId && x.IsValid)
            //.Where(x => (x.FinalDate == null && DateTime.Parse(x.ApplicationDate) <= productionDate.Date) || (x.FinalDate != null &&
            //    DateTime.Parse(x.FinalDate) >= productionDate.Date &&
            //    DateTime.Parse(x.ApplicationDate) <= productionDate.Date))
        .ToListAsync();

            return btps;
        }

        public void Update(BTPData data)
        {
            _context.BTPDatas.Update(data);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
