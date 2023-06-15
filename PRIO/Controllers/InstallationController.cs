﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.GlobalDTOS;
using PRIO.DTOS.HierarchyDTOS.InstallationDTOS;
using PRIO.Filters;
using PRIO.Models;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Installations;

namespace PRIO.Controllers
{
    [ApiController]
    [Route("installations")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class InstallationController : BaseApiController
    {
        public InstallationController(DataContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInstallationViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);
            if (clusterInDatabase is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = $"Cluster not found"
                });

            var installationId = Guid.NewGuid();

            var installation = new Installation
            {
                Id = installationId,
                Name = body.Name,
                Description = body.Description,
                CodInstallationUep = body.CodInstallationUep,
                Cluster = clusterInDatabase,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _context.Installations.AddAsync(installation);

            var history = new SystemHistory
            {
                Table = HistoryColumns.TableCluster,
                CreatedBy = user?.Id,
                TableItemId = installationId,
                UpdatedData = installation,
            };

            await _context.SystemHistories.AddAsync(history);

            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return Created($"installations/{installation.Id}", installationDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var installations = await _context.Installations.Include(x => x.Fields).Include(x => x.User).ToListAsync();
            var installationsDTO = _mapper.Map<List<Installation>, List<InstallationDTO>>(installations);
            return Ok(installationsDTO);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var installation = await _context.Installations
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var installationDTO = _mapper.Map<Installation, InstallationDTO>(installation);

            return Ok(installationDTO);
        }

        //[HttpGet("{id:Guid}/history")]
        //public async Task<IActionResult> GetHistoryById([FromRoute] Guid id)
        //{
        //    var installationHistories = await _context.InstallationHistories
        //        .Include(x => x.User)
        //        .Include(x => x.Cluster)
        //        .Include(x => x.Installation)
        //        .Where(x => x.Installation.Id == id)
        //        .OrderByDescending(x => x.CreatedAt)
        //        .ToListAsync();

        //    if (installationHistories is null)
        //        return NotFound(new ErrorResponseDTO
        //        {
        //            Message = "Installation not found"
        //        });

        //    var installationHistoriesDTO = _mapper.Map<List<InstallationHistory>, List<InstallationHistoryDTO>>(installationHistories);
        //    return Ok(installationHistoriesDTO);
        //}

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateInstallationViewModel body)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found"
                });

            var clusterInDatabase = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == body.ClusterId);

            var beforeInstallation = _mapper.Map<Installation>(installation);
            var updatedProperties = ControllerUtils.CompareAndUpdateInstallation(installation, body);
            if (body.ClusterId is not null && clusterInDatabase is null && body.ClusterId != clusterInDatabase?.Id)
            {

                installation.Cluster = clusterInDatabase;
                updatedProperties[nameof(Installation.Cluster)] = clusterInDatabase;

                return NotFound(new ErrorResponseDTO
                {
                    Message = "Cluster not found"
                });
            }


            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);
            return Ok(installationDTO);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null || !installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or inactive already"
                });

            installation.IsActive = false;
            installation.DeletedAt = DateTime.UtcNow;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:Guid}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var user = HttpContext.Items["User"] as User;

            var installation = await _context.Installations.Include(x => x.Cluster).FirstOrDefaultAsync(x => x.Id == id);
            if (installation is null || installation.IsActive)
                return NotFound(new ErrorResponseDTO
                {
                    Message = "Installation not found or active already"
                });

            installation.IsActive = true;
            installation.DeletedAt = null;

            _context.Installations.Update(installation);
            await _context.SaveChangesAsync();

            var installationDTO = _mapper.Map<Installation, CreateUpdateInstallationDTO>(installation);

            return Ok(installationDTO);
        }
    }
}
