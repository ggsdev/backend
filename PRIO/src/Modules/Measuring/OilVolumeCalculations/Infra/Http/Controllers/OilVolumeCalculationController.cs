using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
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
        public OilVolumeCalculationController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                return Unauthorized(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var installationInDatabase = await _context.Installations.Where(x => x.Id == body.InstallationId).FirstOrDefaultAsync();
            if (installationInDatabase == null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });
            if (installationInDatabase.OilVolumeCalculation != null)
                return Conflict(new ErrorResponseDTO
                {
                    Message = "User not identified, please login first"
                });

            var createOilVolumeCalculation = new OilVolumeCalculation
            {
                Id = Guid.NewGuid(),
                Installation = installationInDatabase
            };
            _context.OilVolumeCalculations.Add(createOilVolumeCalculation);

            if (body.sections is not null)
                foreach (var section in body.sections)
                {
                    var equipment = await _context.MeasuringEquipments.Where(x => x.Id == section.EquipmentId).FirstOrDefaultAsync();

                    var createSection = new Section
                    {
                        Id = Guid.NewGuid(),
                        Name = section.Name,
                        OilVolumeCalculation = createOilVolumeCalculation,
                        Equipment = equipment
                    };
                    _context.Sections.Add(createSection);
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var equipment = await _context.MeasuringEquipments.Where(x => x.Id == tog.EquipmentId).FirstOrDefaultAsync();

                    var createTog = new TOGRecoveredOil
                    {
                        Id = Guid.NewGuid(),
                        Name = tog.Name,
                        OilVolumeCalculation = createOilVolumeCalculation,
                        Equipment = equipment
                    };
                    _context.TOGRecoveredOils.Add(createTog);
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var equipment = await _context.MeasuringEquipments.Where(x => x.Id == dor.EquipmentId).FirstOrDefaultAsync();

                    var createDor = new DOR
                    {
                        Id = Guid.NewGuid(),
                        Name = dor.Name,
                        OilVolumeCalculation = createOilVolumeCalculation,
                        Equipment = equipment
                    };
                    _context.DORs.Add(createDor);
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var equipment = await _context.MeasuringEquipments.Where(x => x.Id == drain.EquipmentId).FirstOrDefaultAsync();

                    var createDrain = new DrainVolume
                    {
                        Id = Guid.NewGuid(),
                        Name = drain.Name,
                        OilVolumeCalculation = createOilVolumeCalculation,
                        Equipment = equipment
                    };
                    _context.DrainVolumes.Add(createDrain);
                }


        }
    }
}
