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
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Where(x => x.Id == body.InstallationId).FirstOrDefaultAsync() ?? throw new NotFoundException("Installation is not found.");
            if (installationInDatabase.OilVolumeCalculation != null)
                throw new ConflictException("Oil Volume Calculations is already register.");

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipamento de tramo não encontrado.");
                    var sectionFound = await _context.Sections
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id == section.MeasuringPointId)
                        .FirstOrDefaultAsync();
                    if (sectionFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {sectionFound.OilVolumeCalculation.Installation.Name}");
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == tog.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipamento TOG não encontrado.");
                    var togFound = await _context.TOGRecoveredOils
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id == tog.MeasuringPointId)
                        .FirstOrDefaultAsync();
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {togFound.OilVolumeCalculation.Installation.Name}");
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == dor.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipamento DOR não encontrado");
                    var dorFound = await _context.DORs
                       .Include(x => x.OilVolumeCalculation)
                       .ThenInclude(x => x.Installation)
                       .Include(x => x.MeasuringPoint)
                       .Where(x => x.MeasuringPoint.Id == dor.MeasuringPointId)
                       .FirstOrDefaultAsync();
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {dorFound.OilVolumeCalculation.Installation.Name}");
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == drain.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipamento Dreno não encontrado.");
                    var drainFound = await _context.DrainVolumes
                       .Include(x => x.OilVolumeCalculation)
                       .ThenInclude(x => x.Installation)
                       .Include(x => x.MeasuringPoint)
                       .Where(x => x.MeasuringPoint.Id == drain.MeasuringPointId)
                       .FirstOrDefaultAsync();
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento já possui relação com o cálculo da instalação {drainFound.OilVolumeCalculation.Installation.Name}");
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
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Where(x => x.Id == installationId).FirstOrDefaultAsync();

            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");

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
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Where(x => x.Id == installationId).FirstOrDefaultAsync();

            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");

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

            string? equation = "";

            if (OilVolumeCalculation.Sections is not null && OilVolumeCalculation.Sections.Count > 0)
            {
                var countSectionsActives = OilVolumeCalculation.Sections.Where(x => x.IsActive == true).ToList();

                for (var i = 0; i < countSectionsActives.Count; i++)
                {
                    if (countSectionsActives[i] is not null)
                    {
                        equation += $"{countSectionsActives[i].MeasuringPoint.Name} * (1 - BSW {countSectionsActives[i].MeasuringPoint.Name})";

                        if (i + 1 != countSectionsActives.Count)
                        {
                            equation += " + ";
                        }

                    }
                }
            }

            if (OilVolumeCalculation.TOGRecoveredOils is not null && OilVolumeCalculation.TOGRecoveredOils.Count > 0)
            {
                var countTogsActives = OilVolumeCalculation.TOGRecoveredOils.Where(x => x.IsActive == true).ToList();
                if (countTogsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(')
                    {
                        equation += " + ";
                    }

                    int? togsCount = OilVolumeCalculation.TOGRecoveredOils.Count;

                    for (var i = 0; i < countTogsActives.Count; i++)
                    {
                        if (countTogsActives[i] is not null)
                        {
                            equation += $"{countTogsActives[i].MeasuringPoint.Name}";

                            if (i + 1 != countTogsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DrainVolumes is not null && OilVolumeCalculation.DrainVolumes.Count > 0)
            {
                var countDrainsActives = OilVolumeCalculation.DrainVolumes.Where(x => x.IsActive == true).ToList();
                if (countDrainsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                    {
                        equation += " + ";
                    }

                    int? drainCount = OilVolumeCalculation.DrainVolumes.Count;

                    for (var i = 0; i < countDrainsActives.Count; i++)
                    {
                        if (countDrainsActives[i] is not null)
                        {
                            equation += $"{countDrainsActives[i].MeasuringPoint.Name}";

                            if (i + 1 != countDrainsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                }
            }

            if (OilVolumeCalculation.DORs is not null && OilVolumeCalculation.DORs.Count > 0)
            {
                var countDorsActives = OilVolumeCalculation.DORs.Where(x => x.IsActive == true).ToList();
                if (countDorsActives.Count > 0)
                {
                    if (!string.IsNullOrEmpty(equation) && equation[^1] != '(' && equation[^1] != '+')
                    {
                        equation += " - (";
                    }

                    for (var i = 0; i < countDorsActives.Count; i++)
                    {
                        if (OilVolumeCalculation.DORs[i] is not null)
                        {
                            equation += $"{countDorsActives[i].MeasuringPoint.Name} * (1 - BSW {countDorsActives[i].MeasuringPoint.Name})";

                            if (i + 1 != countDorsActives.Count)
                            {
                                equation += " + ";
                            }
                        }
                    }
                    equation += ")";
                }
            }

            var returno = new { equation = equation };
            return returno;
        }

        [HttpPatch("oil/{installationId}")]
        public async Task<object> UpdateOilCalculation([FromRoute] Guid installationId, [FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Include(x => x.OilVolumeCalculation).Where(x => x.Id == installationId).FirstOrDefaultAsync();
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");


            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var measuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment section is not found.");
                    var sectionFound = await _context.Sections
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(section.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (sectionFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({sectionFound.OilVolumeCalculation.Installation.Name}).");
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var measuringPoint = await _context.MeasuringPoints.Where(x => x.Id == tog.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment TOG is not found.");
                    var togFound = await _context.TOGRecoveredOils
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(tog.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({togFound.OilVolumeCalculation.Installation.Name}).");
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var measuringPoint = await _context.MeasuringPoints.Where(x => x.Id == dor.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment DOR is not found.");
                    var dorFound = await _context.DORs
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(dor.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({dorFound.OilVolumeCalculation.Installation.Name}).");
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var measuringPoint = await _context.MeasuringPoints.Where(x => x.Id == drain.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment Drain is not found.");
                    var drainFound = await _context.DrainVolumes
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(drain.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento para ser atualizado possui relação com outra instalação ({drainFound.OilVolumeCalculation.Installation.Name}).");
                }

            var oilCalculationInDatabase = await _context.OilVolumeCalculations
                .Include(x => x.Sections)
                .Include(x => x.TOGRecoveredOils)
                .Include(x => x.DrainVolumes)
                .Include(x => x.DORs)
                .Where(x => x.Id == installationInDatabase.OilVolumeCalculation.Id).FirstOrDefaultAsync();
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (oilCalculationInDatabase.Sections.Any())
            {
                _context.Sections.RemoveRange(oilCalculationInDatabase.Sections);
            }

            if (oilCalculationInDatabase.TOGRecoveredOils.Any())
            {
                _context.TOGRecoveredOils.RemoveRange(oilCalculationInDatabase.TOGRecoveredOils);
            }

            if (oilCalculationInDatabase.DrainVolumes.Any())
            {
                _context.DrainVolumes.RemoveRange(oilCalculationInDatabase.DrainVolumes);
            }

            if (oilCalculationInDatabase.DORs.Any())
            {
                _context.DORs.RemoveRange(oilCalculationInDatabase.DORs);
            }

            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync();

                    var createSection = new Section
                    {
                        Id = Guid.NewGuid(),
                        OilVolumeCalculation = oilCalculationInDatabase,
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
                        OilVolumeCalculation = oilCalculationInDatabase,
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
                        OilVolumeCalculation = oilCalculationInDatabase,
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
                        OilVolumeCalculation = oilCalculationInDatabase,
                        MeasuringPoint = MeasuringPoint
                    };
                    _context.DrainVolumes.Add(createDrain);
                }

            await _context.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilCalculationInDatabase);

            return OilVolumeCalculationDTO;
        }

        [HttpPatch("oil/{installationId}/refresh")]
        public async Task<object> Refresh([FromRoute] Guid installationId)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Include(x => x.OilVolumeCalculation).Where(x => x.Id == installationId).FirstOrDefaultAsync();
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            var oilCalculationInDatabase = await _context.OilVolumeCalculations
                .Include(x => x.Sections)
                .Include(x => x.TOGRecoveredOils)
                .Include(x => x.DrainVolumes)
                .Include(x => x.DORs)
                .Where(x => x.Id == installationInDatabase.OilVolumeCalculation.Id).FirstOrDefaultAsync();
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (oilCalculationInDatabase.Sections.Any())
            {
                foreach (var section in oilCalculationInDatabase.Sections)
                {
                    var foundSection = await _context.Sections.Where(x => x.Id == section.Id).FirstOrDefaultAsync();
                    foundSection.DeletedAt = null;
                    foundSection.IsActive = true;

                    _context.Sections.Update(foundSection);
                }

            }

            if (oilCalculationInDatabase.TOGRecoveredOils.Any())
            {
                foreach (var tog in oilCalculationInDatabase.TOGRecoveredOils)
                {
                    var foundTOG = await _context.TOGRecoveredOils.Where(x => x.Id == tog.Id).FirstOrDefaultAsync();
                    foundTOG.DeletedAt = null;
                    foundTOG.IsActive = true;

                    _context.TOGRecoveredOils.Update(foundTOG);
                }
            }

            if (oilCalculationInDatabase.DrainVolumes.Any())
            {
                foreach (var drain in oilCalculationInDatabase.DrainVolumes)
                {
                    var foundDrain = await _context.DrainVolumes.Where(x => x.Id == drain.Id).FirstOrDefaultAsync();
                    foundDrain.DeletedAt = null;
                    foundDrain.IsActive = true;

                    _context.DrainVolumes.Update(foundDrain);
                }
            }

            if (oilCalculationInDatabase.DORs.Any())
            {
                foreach (var dor in oilCalculationInDatabase.DORs)
                {
                    var foundDOR = await _context.DORs.Where(x => x.Id == dor.Id).FirstOrDefaultAsync();
                    foundDOR.DeletedAt = null;
                    foundDOR.IsActive = true;

                    _context.DORs.Update(foundDOR);
                }
            }

            await _context.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilCalculationInDatabase);
            return OilVolumeCalculationDTO;
        }

        [HttpDelete("oil/{installationId}")]
        public async Task<object> DeleteOilCalculation([FromRoute] Guid installationId, [FromBody] CreateOilVolumeCalculationViewModel body)
        {
            if (HttpContext.Items["User"] is not User user)
                throw new UnauthorizedAccessException("User not identified, please login first");

            var installationInDatabase = await _context.Installations.Include(x => x.OilVolumeCalculation).Where(x => x.Id == installationId).FirstOrDefaultAsync();
            if (installationInDatabase == null)
                throw new NotFoundException("Instalação não encontrada");
            if (installationInDatabase.OilVolumeCalculation is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");


            if (body.Sections is not null)
                foreach (var section in body.Sections)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == section.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment section is not found.");
                    var sectionFound = await _context.Sections
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(section.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (sectionFound is not null)
                    {
                        throw new ConflictException($"Equipamento para ser deletado possui relação com outra instalação ({sectionFound.OilVolumeCalculation.Installation.Name}).");
                    }

                    var sectionFoundSame = await _context.Sections
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(section.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (sectionFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.TOGs is not null)
                foreach (var tog in body.TOGs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == tog.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment TOG is not found.");
                    var togFound = await _context.TOGRecoveredOils
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(tog.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (togFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({togFound.OilVolumeCalculation.Installation.Name}).");

                    var togFoundSame = await _context.TOGRecoveredOils
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(tog.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (togFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.DORs is not null)
                foreach (var dor in body.DORs)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == dor.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment DOR is not found.");
                    var dorFound = await _context.DORs
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(dor.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (dorFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({dorFound.OilVolumeCalculation.Installation.Name}).");

                    var dorFoundSame = await _context.DORs
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(dor.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (dorFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            if (body.Drains is not null)
                foreach (var drain in body.Drains)
                {
                    var MeasuringPoint = await _context.MeasuringPoints.Where(x => x.Id == drain.MeasuringPointId).FirstOrDefaultAsync() ?? throw new NotFoundException("Equipment Drain is not found.");
                    var drainFound = await _context.DrainVolumes
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => !x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(drain.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (drainFound is not null)
                        throw new ConflictException($"Equipamento pra ser deletado possui relação com outra instalação ({drainFound.OilVolumeCalculation.Installation.Name}).");

                    var drainFoundSame = await _context.DrainVolumes
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.OilVolumeCalculation.Id.Equals(installationInDatabase.OilVolumeCalculation.Id))
                        .Where(x => x.MeasuringPoint.Id.Equals(drain.MeasuringPointId))
                        .FirstOrDefaultAsync();
                    if (drainFoundSame is null)
                    {
                        throw new NotFoundException($"Equipamento não possui relação nesta instalação ({installationInDatabase.Name}).");
                    }
                }

            var oilCalculationInDatabase = await _context.OilVolumeCalculations
                .Include(x => x.Sections)
                .Include(x => x.TOGRecoveredOils)
                .Include(x => x.DrainVolumes)
                .Include(x => x.DORs)
                .Where(x => x.Id == installationInDatabase.OilVolumeCalculation.Id).FirstOrDefaultAsync();
            if (oilCalculationInDatabase is null)
                throw new NotFoundException("Intalação não possui cálculo para ser atualizado");

            if (body.Sections.Any())
            {
                foreach (var section in body.Sections)
                {
                    var sectionFound = await _context.Sections
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id.Equals(section.MeasuringPointId))
                        .FirstOrDefaultAsync();

                    sectionFound.IsActive = false;
                    sectionFound.DeletedAt = DateTime.UtcNow;

                    _context.Sections.Update(sectionFound);
                }
            }

            if (body.TOGs.Any())
            {
                foreach (var tog in body.TOGs)
                {
                    var togFound = await _context.TOGRecoveredOils
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id.Equals(tog.MeasuringPointId))
                        .FirstOrDefaultAsync();

                    togFound.IsActive = false;
                    togFound.DeletedAt = DateTime.UtcNow;

                    _context.TOGRecoveredOils.Update(togFound);
                }
            }

            if (body.Drains.Any())
            {
                foreach (var drain in body.Drains)
                {
                    var drainFound = await _context.DrainVolumes
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id.Equals(drain.MeasuringPointId))
                        .FirstOrDefaultAsync();

                    drainFound.IsActive = false;
                    drainFound.DeletedAt = DateTime.UtcNow;

                    _context.DrainVolumes.Update(drainFound);
                }
            }

            if (body.DORs.Any())
            {
                foreach (var dor in body.DORs)
                {
                    var dorFound = await _context.DORs
                        .Include(x => x.OilVolumeCalculation)
                        .ThenInclude(x => x.Installation)
                        .Include(x => x.MeasuringPoint)
                        .Where(x => x.MeasuringPoint.Id.Equals(dor.MeasuringPointId))
                        .FirstOrDefaultAsync();

                    dorFound.IsActive = false;
                    dorFound.DeletedAt = DateTime.UtcNow;

                    _context.DORs.Update(dorFound);
                }
            }

            await _context.SaveChangesAsync();
            var OilVolumeCalculationDTO = _mapper.Map<OilVolumeCalculation, OilVolumeCalculationDTO>(oilCalculationInDatabase);

            return NoContent();
        }
    }
}
