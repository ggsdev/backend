using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Controllers
{
    [ApiController]
    [Route("calculation")]
    public class OilVolumeCalculationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public OilVolumeCalculationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpPost]
        //public async Task<OilVolumeCalculationDTO?> Create([FromBody] CreateOilVolumeCalculationViewModel body)
        //{
        //    if (HttpContext.Items["User"] is not User user)
        //        throw new UnauthorizedAccessException("User not identified, please login first");

        //    var installationInDatabase = await _context.Installations.Where(x => x.Id == body.InstallationId).FirstOrDefaultAsync() ?? throw new NotFoundException("Installation is not found.");
        //    if (installationInDatabase.OilVolumeCalculation != null)
        //        throw new ConflictException("Oil Volume Calculations is already register.");

        //    if (body.Sections is not null)
        //        foreach (var section in body.Sections)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == section.EquipmentId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment section is not found.");
        //        }

        //    if (body.TOGs is not null)
        //        foreach (var tog in body.TOGs)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == tog.EquipmentId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment TOG is not found.");
        //        }

        //    if (body.DORs is not null)
        //        foreach (var dor in body.DORs)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == dor.EquipmentId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment DOR is not found.");
        //        }

        //    if (body.Drains is not null)
        //        foreach (var drain in body.Drains)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == drain.EquipmentId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment Drain is not found.");
        //        }

        //    var createOilVolumeCalculation = new OilVolumeCalculation
        //    {
        //        Id = Guid.NewGuid(),
        //        Installation = installationInDatabase
        //    };
        //    _context.OilVolumeCalculations.Add(createOilVolumeCalculation);

        //    if (body.Sections is not null)
        //        foreach (var section in body.Sections)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == section.EquipmentId).FirstOrDefaultAsync();

        //            var createSection = new Section
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = section.Name,
        //                OilVolumeCalculation = createOilVolumeCalculation,
        //                Equipment = equipment
        //            };
        //            _context.Sections.Add(createSection);
        //        }

        //    if (body.TOGs is not null)
        //        foreach (var tog in body.TOGs)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == tog.EquipmentId).FirstOrDefaultAsync();

        //            var createTog = new TOGRecoveredOil
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = tog.Name,
        //                OilVolumeCalculation = createOilVolumeCalculation,
        //                Equipment = equipment
        //            };
        //            _context.TOGRecoveredOils.Add(createTog);
        //        }

        //    if (body.DORs is not null)
        //        foreach (var dor in body.DORs)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == dor.EquipmentId).FirstOrDefaultAsync();

        //            var createDor = new DOR
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = dor.Name,
        //                OilVolumeCalculation = createOilVolumeCalculation,
        //                Equipment = equipment
        //            };
        //            _context.DORs.Add(createDor);
        //        }

        //    if (body.Drains is not null)
        //        foreach (var drain in body.Drains)
        //        {
        //            var equipment = await _context.MeasuringEquipments.Where(x => x.Id == drain.EquipmentId).FirstOrDefaultAsync();

        //            var createDrain = new DrainVolume
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = drain.Name,
        //                OilVolumeCalculation = createOilVolumeCalculation,
        //                Equipment = equipment
        //            };
        //            _context.DrainVolumes.Add(createDrain);
        //        }

        //    await _context.SaveChangesAsync();
        //    var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(createOilVolumeCalculation);

        //    return OilVolumeCalculationDTO;
        //}
    }
}
