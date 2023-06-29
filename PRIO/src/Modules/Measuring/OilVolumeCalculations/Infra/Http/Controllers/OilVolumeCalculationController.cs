using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels;
using PRIO.src.Shared.Errors;
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

        [HttpPost]
        public async Task<OilVolumeCalculationDTO?> Create([FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Where(x => x.Id == body.InstallationId).FirstOrDefaultAsync() ?? throw new NotFoundException("Installation is not found.");
            if (installationInDatabase.OilVolumeCalculation != null)
                throw new ConflictException("Oil Volume Calculations is already register.");

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment section is not found.");
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == tog.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment TOG is not found.");
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == dor.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment DOR is not found.");
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == drain.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment Drain is not found.");
                }

            var createOilVolumeCalculation = new OilVolumeCalculation
            {
                Id = Guid.NewGuid(),
                Installation = installationInDatabase
            };
            _context.OilVolumeCalculations.Add(createOilVolumeCalculation);

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync();

                    var createSection = new Section
                    {
                        Id = Guid.NewGuid(),
                        OilVolumeCalculation = createOilVolumeCalculation,
                        MeasuringPoint = MeasuringPoint
                    };
                    _context.Sections.Add(createSection);
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == tog.MeasuringPointId).FirstOrDefaultAsync();

                    var createTog = new TOGRecoveredOil
                    {
                        Id = Guid.NewGuid(),
                        OilVolumeCalculation = createOilVolumeCalculation,
                        MeasuringPoint = MeasuringPoint
                    };
                    _context.TOGRecoveredOils.Add(createTog);
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == dor.MeasuringPointId).FirstOrDefaultAsync();

                    var createDor = new DOR
                    {
                        Id = Guid.NewGuid(),
                        OilVolumeCalculation = createOilVolumeCalculation,
                        MeasuringPoint = MeasuringPoint
                    };
                    _context.DORs.Add(createDor);
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == drain.MeasuringPointId).FirstOrDefaultAsync();

                    var createDrain = new DrainVolume
                    {
                        Id = Guid.NewGuid(),
                        OilVolumeCalculation = createOilVolumeCalculation,
                        MeasuringPoint = MeasuringPoint
                    };
                    _context.DrainVolumes.Add(createDrain);
                }

            await _context.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(createOilVolumeCalculation);

            return OilVolumeCalculationDTO;
        }
    }
}
