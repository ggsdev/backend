﻿using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Interfaces;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Repositories
{
    public class OilVolumeCalculationRepository : IOilVolumeCalculationRepository
    {
        private readonly DataContext _context;
        public OilVolumeCalculationRepository(DataContext context)
        {
            _context = context;
        }

        #region OilVolumeCalculation
        public async Task<OilVolumeCalculation?> AddOilVolumeCalculationAsync(Installation installation)
        {
            var createOilVolumeCalculation = new OilVolumeCalculation
            {
                Id = Guid.NewGuid(),
                Installation = installation
            };
            _context.OilVolumeCalculations.Add(createOilVolumeCalculation);

            return createOilVolumeCalculation;
        }
        public async Task<OilVolumeCalculation?> GetOilVolumeCalculationByInstallationId(Guid id)
        {
            var oilVolumeCalculation = await _context.OilVolumeCalculations.Include(x => x.Installation)
                .Include(x => x.DrainVolumes)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.DORs)
                    .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.Sections)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.TOGRecoveredOils)
                .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.Id == id)
                .FirstOrDefaultAsync();

            return oilVolumeCalculation;
        }
        public async Task<List<OilVolumeCalculation?>> GetAllOils()
        {
            var oilVolumeCalculation = await _context.OilVolumeCalculations.Include(x => x.Installation)
                .Include(x => x.DrainVolumes)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.DORs)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.Sections)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.TOGRecoveredOils)
                .ThenInclude(x => x.MeasuringPoint)
                .ToListAsync();

            return oilVolumeCalculation;
        }

        public async Task<OilVolumeCalculation?> GetOilVolumeCalculationByInstallationUEP(string uepCode)
        {
            return await _context.OilVolumeCalculations.Include(x => x.Installation)
                .Include(x => x.DrainVolumes)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.DORs)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.Sections)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.TOGRecoveredOils)
                .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.UepCod == uepCode)
                .FirstOrDefaultAsync();
        }
        public async Task<OilVolumeCalculation?> GetOilVolumeCalculationById(Guid id)
        {
            var oilVolumeCalculation = await _context.OilVolumeCalculations
                .Include(x => x.Sections)
                .Include(x => x.TOGRecoveredOils)
                .Include(x => x.DrainVolumes)
                .Include(x => x.DORs)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return oilVolumeCalculation;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Section
        public async Task<Section?> GetSectionByMeasuringPointIdAsync(Guid? id)
        {

            var sectionFound = await _context.Sections.Include(x => x.OilVolumeCalculation).ThenInclude(x => x.Installation).Include(x => x.MeasuringPoint).Where(x => x.MeasuringPoint.Id == id).FirstOrDefaultAsync();
            return sectionFound;
        }
        public async Task<Section?> GetSectionByIdAsync(Guid? id)
        {

            var sectionFound = await _context.Sections.Where(x => x.Id == id).FirstOrDefaultAsync();
            return sectionFound;
        }
        public async Task UpdateSection(Section section)
        {
            _context.Sections.Update(section);
        }
        public async Task RemoveSectionRange(List<Section> sections)
        {
            _context.Sections.RemoveRange(sections);
        }
        public async Task<Section?> GetSectionOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var sectionFound = await _context.Sections
               .Include(x => x.OilVolumeCalculation)
               .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
               .Where(x => !x.OilVolumeCalculation.Id.Equals(oilCalculationId))
               .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
               .FirstOrDefaultAsync();
            return sectionFound;
        }
        public async Task<Section?> GetSectionInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var sectionFound = await _context.Sections
               .Include(x => x.OilVolumeCalculation)
               .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
               .Where(x => x.OilVolumeCalculation.Id.Equals(oilCalculationId))
               .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
               .FirstOrDefaultAsync();
            return sectionFound;
        }
        public async Task<Section?> AddSection(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, CreateSectionViewModel section)
        {

            var createSection = new Section
            {
                Id = Guid.NewGuid(),
                StaticLocalMeasuringPoint = section.StaticMeasuringPointName,
                IsApplicable = section.IsApplicable,
                OilVolumeCalculation = oilVolumeCalculation,
                MeasuringPoint = measuringPoint
            };
            _context.Sections.Add(createSection);

            return createSection;
        }
        #endregion

        #region TOG
        public async Task<TOGRecoveredOil?> GetTOGByMeasuringPointIdAsync(Guid? id)
        {

            var togFound = await _context.TOGRecoveredOils
                         .Include(x => x.OilVolumeCalculation)
                         .ThenInclude(x => x.Installation)
            .Include(x => x.MeasuringPoint)
                         .Where(x => x.MeasuringPoint.Id == id)
                         .FirstOrDefaultAsync();
            return togFound;
        }
        public async Task<TOGRecoveredOil?> GetTOGByIdAsync(Guid? id)
        {
            var togFound = await _context.TOGRecoveredOils.Where(x => x.Id == id).FirstOrDefaultAsync();
            return togFound;
        }
        public async Task UpdateTOG(TOGRecoveredOil tog)
        {
            _context.TOGRecoveredOils.Update(tog);
        }
        public async Task RemoveTOGRange(List<TOGRecoveredOil> togs)
        {
            _context.TOGRecoveredOils.RemoveRange(togs);
        }
        public async Task<TOGRecoveredOil?> GetTOGOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var togFound = await _context.TOGRecoveredOils
                                   .Include(x => x.OilVolumeCalculation)
                                   .ThenInclude(x => x.Installation)
                                   .Include(x => x.MeasuringPoint)
                                   .Where(x => !x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                                   .FirstOrDefaultAsync();
            return togFound;
        }
        public async Task<TOGRecoveredOil?> GetTOGInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var togFound = await _context.TOGRecoveredOils
                                   .Include(x => x.OilVolumeCalculation)
                                   .ThenInclude(x => x.Installation)
                                   .Include(x => x.MeasuringPoint)
                                   .Where(x => x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                                   .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                                   .FirstOrDefaultAsync();
            return togFound;
        }

        public async Task<TOGRecoveredOil?> AddTOG(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, CreateTOGRecoverOilViewModel tog)
        {

            var createTOG = new TOGRecoveredOil
            {
                Id = Guid.NewGuid(),
                StaticLocalMeasuringPoint = tog.StaticMeasuringPointName,
                IsApplicable = tog.IsApplicable,
                OilVolumeCalculation = oilVolumeCalculation,
                MeasuringPoint = measuringPoint
            };
            _context.TOGRecoveredOils.Add(createTOG);

            return createTOG;
        }
        #endregion

        #region DOR
        public async Task<DOR?> GetDORByMeasuringPointIdAsync(Guid? id)
        {
            var dorFound = await _context.DORs
                       .Include(x => x.OilVolumeCalculation)
                       .ThenInclude(x => x.Installation)
                       .Include(x => x.MeasuringPoint)
                       .Where(x => x.MeasuringPoint.Id == id)
                       .FirstOrDefaultAsync();
            return dorFound;
        }
        public async Task<DOR?> GetDORByIdAsync(Guid? id)
        {
            var dorFound = await _context.DORs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return dorFound;
        }
        public async Task UpdateDOR(DOR dor)
        {
            _context.DORs.Update(dor);
        }
        public async Task RemoveDORRange(List<DOR> dors)
        {
            _context.DORs.RemoveRange(dors);
        }
        public async Task<DOR?> GetDOROtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var dorFound = await _context.DORs
                .Include(x => x.OilVolumeCalculation)
                .ThenInclude(x => x.Installation)
                .Include(x => x.MeasuringPoint)
                .Where(x => !x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                .FirstOrDefaultAsync();
            return dorFound;
        }
        public async Task<DOR?> GetDORInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var dorFound = await _context.DORs
                .Include(x => x.OilVolumeCalculation)
                .ThenInclude(x => x.Installation)
                .Include(x => x.MeasuringPoint)
                .Where(x => x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                .FirstOrDefaultAsync();
            return dorFound;
        }

        public async Task<DOR?> AddDOR(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, CreateDorViewModel dor)
        {

            var createDOR = new DOR
            {
                Id = Guid.NewGuid(),
                StaticLocalMeasuringPoint = dor.StaticMeasuringPointName,
                IsApplicable = dor.IsApplicable,
                OilVolumeCalculation = oilVolumeCalculation,
                MeasuringPoint = measuringPoint
            };
            _context.DORs.Add(createDOR);

            return createDOR;
        }
        #endregion

        #region Drain
        public async Task<DrainVolume?> GetDrainByMeasuringPointIdAsync(Guid? id)
        {
            var drainFound = await _context.DrainVolumes
               .Include(x => x.OilVolumeCalculation)
               .ThenInclude(x => x.Installation)
               .Include(x => x.MeasuringPoint)
               .Where(x => x.MeasuringPoint.Id == id)
               .FirstOrDefaultAsync();
            return drainFound;
        }
        public async Task<DrainVolume?> GetDrainByIdAsync(Guid? id)
        {
            var foundDrain = await _context.DrainVolumes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return foundDrain;
        }
        public async Task UpdateDrain(DrainVolume drain)
        {
            _context.DrainVolumes.Update(drain);
        }
        public async Task RemoveDrainRange(List<DrainVolume> drains)
        {
            _context.DrainVolumes.RemoveRange(drains);
        }

        public async Task<DrainVolume?> GetDrainOtherInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var drainFound = await _context.DrainVolumes
                .Include(x => x.OilVolumeCalculation)
                .ThenInclude(x => x.Installation)
                .Include(x => x.MeasuringPoint)
                .Where(x => !x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                .FirstOrDefaultAsync();
            return drainFound;
        }
        public async Task<DrainVolume?> GetDrainInstallationByIdAsync(Guid? oilCalculationId, Guid mpointId)
        {
            var drainFound = await _context.DrainVolumes
                .Include(x => x.OilVolumeCalculation)
                .ThenInclude(x => x.Installation)
                .Include(x => x.MeasuringPoint)
                .Where(x => x.OilVolumeCalculation.Id.Equals(oilCalculationId))
                .Where(x => x.MeasuringPoint.Id.Equals(mpointId))
                .FirstOrDefaultAsync();
            return drainFound;
        }

        public async Task<DrainVolume?> AddDrain(OilVolumeCalculation oilVolumeCalculation, MeasuringPoint measuringPoint, CreateDrainVolumeViewModel drain)
        {

            var createSection = new DrainVolume
            {
                Id = Guid.NewGuid(),
                StaticLocalMeasuringPoint = drain.StaticMeasuringPointName,
                IsApplicable = drain.IsApplicable,
                OilVolumeCalculation = oilVolumeCalculation,
                MeasuringPoint = measuringPoint
            };
            _context.DrainVolumes.Add(createSection);

            return createSection;
        }
        #endregion
    }
}
