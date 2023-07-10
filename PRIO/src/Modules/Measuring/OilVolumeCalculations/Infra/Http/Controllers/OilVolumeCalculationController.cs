using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Dtos;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using PRIO.src.Shared.Infra.Http.Filters;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.Http.Controllers
{
    [ApiController]
    [Route("calculation")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class OilVolumeCalculationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public OilVolumeCalculationController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("oil")]
        public async Task<OilVolumeCalculationDTO?> Create([FromBody] CreateOilVolumeCalculationViewModel body)
        {
            Console.WriteLine(HttpContext.Items["User"]);
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

        [HttpGet("oil/{installationId}")]
        public async Task<OilVolumeCalculationDTO?> Get([FromRoute] Guid installationId)
        {
            var OilVolumeCalculation = await _context.OilVolumeCalculations.Include(x => x.Installation)
                .Include(x => x.DrainVolumes)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.DORs)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.Sections)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.TOGRecoveredOils)
                .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.Id == installationId)
                .FirstOrDefaultAsync();

            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(OilVolumeCalculation);
            return OilVolumeCalculationDTO;

        }

        [HttpGet("oil/{installationId}/equation")]
        public async Task<object> GetEquation([FromRoute] Guid installationId)
        {
            var OilVolumeCalculation = await _context.OilVolumeCalculations.Include(x => x.Installation)
                .Include(x => x.DrainVolumes)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.DORs)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.Sections)
                .ThenInclude(x => x.MeasuringPoint)
                .Include(x => x.TOGRecoveredOils)
                .ThenInclude(x => x.MeasuringPoint)
                .Where(x => x.Installation.Id == installationId)
                .FirstOrDefaultAsync();

            string? equation = "(";

            if (OilVolumeCalculation.Sections is not null && OilVolumeCalculation.Sections.Count > 0)
            {
                int? sectionCount = OilVolumeCalculation.Sections.Count;

                for (var i = 0; i < sectionCount; i++)
                {
                    if (OilVolumeCalculation.Sections[i] is not null)
                    {
                        equation += $"{OilVolumeCalculation.Sections[i].MeasuringPoint.Name} * (1 - BSW {OilVolumeCalculation.Sections[i].MeasuringPoint.Name})";

                        if (i + 1 != sectionCount)
                        {
                            equation += " + ";
                        }
                    }
                }
            }

            if (OilVolumeCalculation.TOGRecoveredOils is not null && OilVolumeCalculation.TOGRecoveredOils.Count > 0)
            {
                if (!string.IsNullOrEmpty(equation) && equation[^1] != '(')
                {
                    equation += " + ";
                }

                int? togsCount = OilVolumeCalculation.TOGRecoveredOils.Count;

                for (var i = 0; i < togsCount; i++)
                {
                    if (OilVolumeCalculation.TOGRecoveredOils[i] is not null)
                    {
                        equation += $"{OilVolumeCalculation.TOGRecoveredOils[i].MeasuringPoint.Name}";

                        if (i + 1 != togsCount)
                        {
                            equation += " + ";
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DrainVolumes is not null && OilVolumeCalculation.DrainVolumes.Count > 0)
            {
                if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                {
                    equation += " + ";
                }

                int? drainCount = OilVolumeCalculation.DrainVolumes.Count;

                for (var i = 0; i < drainCount; i++)
                {
                    if (OilVolumeCalculation.DrainVolumes[i] is not null)
                    {
                        equation += $"{OilVolumeCalculation.DrainVolumes[i].MeasuringPoint.Name}";

                        if (i + 1 != drainCount)
                        {
                            equation += " + ";
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DORs is not null && OilVolumeCalculation.DORs.Count > 0)
            {
                if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                {
                    equation += " + ";
                }

                int? dorCount = OilVolumeCalculation.DORs.Count;

                for (var i = 0; i < dorCount; i++)
                {
                    if (OilVolumeCalculation.DORs[i] is not null)
                    {
                        equation += $"{OilVolumeCalculation.DORs[i].MeasuringPoint.Name} * (1 - BSW {OilVolumeCalculation.Sections[i].MeasuringPoint.Name})";

                        if (i + 1 != dorCount)
                        {
                            equation += " + ";
                        }
                    }
                }
            }

            var returno = new { equation = equation };
            return returno;
        }
    }
}
