using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS.HierarchyDTOS.CompletionDTOS;
using PRIO.Exceptions;
using PRIO.Models.HierarchyModels;
using PRIO.Models.UserControlAccessModels;
using PRIO.Utils;
using PRIO.ViewModels.Completions;

namespace PRIO.Services.HierarchyServices
{
    public class CompletionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompletionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompletionDTO> CreateCompletion(CreateCompletionViewModel body, User user)
        {
            var well = await _context.Wells
              .Include(x => x.Field)
              .FirstOrDefaultAsync(x => x.Id == body.WellId);

            if (well is null)
                throw new NotFoundException($"Well with id: {body.WellId} not found");

            var reservoir = await _context.Reservoirs
                .Include(x => x.Zone)
                .ThenInclude(z => z.Field)
                .FirstOrDefaultAsync(x => x.Id == body.ReservoirId);

            if (reservoir is null)
                throw new NotFoundException($"Reservoir with id: {body.ReservoirId} not found");

            if (reservoir.Zone?.Field?.Id != well.Field?.Id)
                throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

            var completion = await _context.Completions.FirstOrDefaultAsync(x => x.Name == $"{well.Name}_{reservoir.Zone.CodZone}");

            if (completion is not null)
                throw new ConflictException($"Completion with name: {well.Name}_{reservoir.Zone?.CodZone} already exists.");

            var completionName = $"{well.Name}_{reservoir.Zone?.CodZone}";

            completion = new Completion
            {
                Name = completionName,
                CodCompletion = body.CodCompletion is not null ? body.CodCompletion : GenerateCode.Generate(completionName),
                Description = body.Description,
                User = user,
                Well = well,
                Reservoir = reservoir,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };
            await _context.Completions.AddAsync(completion);

            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);

            return completionDTO;
        }

        public async Task<List<CompletionDTO>> GetCompletions()
        {
            var completions = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .Include(x => x.User)
                .ToListAsync();

            var completionsDTO = _mapper.Map<List<Completion>, List<CompletionDTO>>(completions);

            return completionsDTO;
        }

        public async Task<CompletionDTO> GetCompletionById(Guid id)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task<CompletionDTO> UpdateCompletion(UpdateCompletionViewModel body, Guid id, User user)
        {
            var completion = await _context.Completions
               .Include(x => x.Well)
               .Include(x => x.Reservoir)
               .ThenInclude(x => x.Zone)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null)
                throw new NotFoundException("Completion not found");

            var well = await _context.Wells
                .Include(x => x.Field)
                .FirstOrDefaultAsync(x => x.Id == body.WellId);

            var reservoir = await _context.Reservoirs
                    .Include(x => x.Zone)
                    .ThenInclude(z => z.Field)
                    .FirstOrDefaultAsync(z => z.Id == body.ReservoirId);

            if (body.WellId is not null)
            {
                if (well is null)
                    throw new NotFoundException("Well not found");

                if (reservoir is null && body.ReservoirId is not null)
                    throw new NotFoundException("Reservoir not found");

                if (reservoir is not null && well.Field?.Id != reservoir.Zone?.Field?.Id)
                    throw new ConflictException($"Well: {well.Name} and Reservoir: {reservoir.Name} doesn't belong to the same Field");

                completion.Name = $"{well.Name}_{completion.Reservoir?.Zone?.CodZone}";
                completion.Well = well;
            }

            if (body.ReservoirId is not null)
            {
                if (reservoir is null)

                    throw new NotFoundException("Reservoir not found");


                if (well is null && body.WellId is not null)
                    throw new NotFoundException("Well not found");

                if (well is not null && reservoir.Zone?.Field?.Id != well.Field?.Id)
                    throw new ConflictException($"Reservoir: {reservoir.Name} and Well: {well.Name} doesn't belong to the same Field");

                completion.Name = $"{completion.Well?.Name}_{reservoir.Zone?.CodZone}";
                completion.Reservoir = reservoir;
            }


            completion.Description = body.Description is not null ? body.Description : completion.Description;
            completion.CodCompletion = body.CodCompletion is not null ? body.CodCompletion : completion.CodCompletion;

            _context.Completions.Update(completion);

            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }

        public async Task DeleteCompletion(Guid id, User user)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || !completion.IsActive)
                throw new NotFoundException("Completion not found or inactive already");

            completion.IsActive = false;
            completion.DeletedAt = DateTime.UtcNow;

            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();
        }

        public async Task<CompletionDTO> RestoreCompletion(Guid id, User user)
        {
            var completion = await _context.Completions
                .Include(x => x.Well)
                .Include(x => x.Reservoir)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (completion is null || completion.IsActive is true)
                throw new NotFoundException("Completion not found or inactive already");

            completion.IsActive = true;
            completion.DeletedAt = null;

            _context.Completions.Update(completion);
            await _context.SaveChangesAsync();

            var completionDTO = _mapper.Map<Completion, CompletionDTO>(completion);
            return completionDTO;
        }
    }
}
