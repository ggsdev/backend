using AutoMapper;
using Newtonsoft.Json;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Completions.Interfaces;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Dtos;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.SystemHistories.Dtos.HierarchyDtos;
using PRIO.src.Shared.SystemHistories.Infra.EF.Models;
using PRIO.src.Shared.SystemHistories.Infra.Http.Services;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.Http.Services
{
    public class WellService
    {
        private readonly IMapper _mapper;
        private readonly IFieldRepository _fieldRepository;
        private readonly IWellRepository _wellRepository;
        private readonly ICompletionRepository _completionRepository;
        private readonly SystemHistoryService _systemHistoryService;
        private readonly string _tableName = HistoryColumns.TableWells;

        public WellService(IMapper mapper, IFieldRepository fieldRepository, SystemHistoryService systemHistoryService, IWellRepository wellRepository, ICompletionRepository completionRepositor)
        {
            _mapper = mapper;
            _fieldRepository = fieldRepository;
            _wellRepository = wellRepository;
            _completionRepository = completionRepositor;
            _systemHistoryService = systemHistoryService;
        }

        public async Task<CreateUpdateWellDTO> CreateWell(CreateWellViewModel body, User user)
        {
            var wellExistingCode = await _wellRepository.GetByCode(body.CodWellAnp);
            if (wellExistingCode is not null)
                throw new ConflictException(ErrorMessages.CodAlreadyExists<Well>());

            var field = await _fieldRepository.GetOnlyField(body.FieldId);

            if (field is null)
                throw new NotFoundException(ErrorMessages.NotFound<Field>());

            if (field.IsActive is false)
                throw new NotFoundException("Campo não está ativo.");

            var wellId = Guid.NewGuid();

            var well = new Well
            {
                Id = wellId,
                CodWell = body.CodWell is not null ? body.CodWell : GenerateCode.Generate(body.Name),
                Name = body.Name,
                WellOperatorName = body.WellOperatorName,
                CodWellAnp = body.CodWellAnp,
                CategoryAnp = body.CategoryAnp,
                CategoryReclassificationAnp = body.CategoryReclassificationAnp,
                CategoryOperator = body.CategoryOperator,
                StatusOperator = body.StatusOperator,
                Type = body.Type,
                WaterDepth = body.WaterDepth,
                TopOfPerforated = body.TopOfPerforated,
                BaseOfPerforated = body.BaseOfPerforated,
                ArtificialLift = body.ArtificialLift,
                Latitude4C = body.Latitude4C,
                Longitude4C = body.Longitude4C,
                LongitudeDD = body.LongitudeDD,
                LatitudeDD = body.LatitudeDD,
                DatumHorizontal = body.DatumHorizontal,
                TypeBaseCoordinate = body.TypeBaseCoordinate,
                CoordX = body.CoordX,
                CoordY = body.CoordY,
                Description = body.Description,
                Field = field,
                User = user,
                IsActive = body.IsActive is not null ? body.IsActive.Value : true,
            };

            await _wellRepository.AddAsync(well);

            await _systemHistoryService
                .Create<Well, WellHistoryDTO>(_tableName, user, wellId, well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }

        public async Task<List<WellDTO>> GetWells()
        {
            var wells = await _wellRepository.GetAsync();

            var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);
            return wellsDTO;
        }

        public async Task<WellDTO> GetWellById(Guid id)
        {
            var well = await _wellRepository.GetByIdAsync(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var wellDTO = _mapper.Map<Well, WellDTO>(well);
            return wellDTO;
        }
        public async Task<CreateUpdateWellDTO> UpdateWell(UpdateWellViewModel body, Guid id, User user)
        {
            var well = await _wellRepository.GetByIdWithFieldAndCompletions(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false)
                throw new ConflictException("Poço não está ativo.");

            if (well.Completions.Count > 0)
                if (body.CodWellAnp is not null)
                    if (body.CodWellAnp != well.CodWellAnp)
                        throw new ConflictException("Código do campo não pode ser alterado.");

            if (body.CodWellAnp is not null)
            {
                var wellInDatabase = await _wellRepository.GetByCode(body.CodWellAnp);
                if (wellInDatabase is not null)
                    throw new ConflictException(ErrorMessages.CodAlreadyExists<Well>());

            }
            var beforeChangesWell = _mapper.Map<WellHistoryDTO>(well);

            var updatedProperties = UpdateFields.CompareUpdateReturnOnlyUpdated(well, body);

            if (updatedProperties.Any() is false && (well.Field?.Id == body.FieldId || body.FieldId is null))
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<Well>());

            if (body.FieldId is not null && well.Field?.Id != body.FieldId)
            {
                var fieldInDatabase = await _fieldRepository.GetOnlyField(body.FieldId);

                if (fieldInDatabase is null)
                    throw new NotFoundException(ErrorMessages.NotFound<Field>());

                well.Field = fieldInDatabase;
                updatedProperties[nameof(WellHistoryDTO.fieldId)] = fieldInDatabase.Id;
            }

            await _systemHistoryService
                .Update(_tableName, user, updatedProperties, well.Id, well, beforeChangesWell);

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }

        public async Task DeleteWell(Guid id, User user)
        {
            var well = await _wellRepository.GetOnlyWellAsync(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is false)
                throw new BadRequestException(ErrorMessages.InactiveAlready<Well>());

            var propertiesUpdated = new
            {
                IsActive = false,
                DeletedAt = DateTime.UtcNow,
            };

            if (well.Completions is not null)
                foreach (var completion in well.Completions)
                {
                    if (completion.IsActive is true)
                    {
                        var completionPropertiesToUpdate = new
                        {
                            IsActive = false,
                            DeletedAt = DateTime.UtcNow,
                        };

                        var completionUpdatedProperties = UpdateFields
                        .CompareUpdateReturnOnlyUpdated(completion, completionPropertiesToUpdate);

                        await _systemHistoryService
                            .Delete<Completion, CompletionHistoryDTO>(HistoryColumns.TableReservoirs, user, completionUpdatedProperties, completion.Id, completion);

                        _completionRepository.Delete(completion);
                    }
                }
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(well, propertiesUpdated);

            await _systemHistoryService
                .Delete<Well, WellHistoryDTO>(_tableName, user, updatedProperties, well.Id, well);

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();
        }

        public async Task<CreateUpdateWellDTO> RestoreWell(Guid id, User user)
        {
            var well = await _wellRepository.GetWithUserAsync(id);

            if (well is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            if (well.IsActive is true)
                throw new BadRequestException(ErrorMessages.ActiveAlready<Well>());

            var propertiesUpdated = new
            {
                IsActive = true,
                DeletedAt = (DateTime?)null,
            };
            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(well, propertiesUpdated);

            await _systemHistoryService
                .Restore<Well, WellHistoryDTO>(_tableName, user, updatedProperties, well.Id, well);

            _wellRepository.Update(well);

            await _wellRepository.SaveChangesAsync();

            var wellDTO = _mapper.Map<Well, CreateUpdateWellDTO>(well);
            return wellDTO;
        }


        public async Task<List<SystemHistory>> GetWellHistory(Guid id)
        {
            var wellHistories = await _systemHistoryService.GetAll(id);

            if (wellHistories is null)
                throw new NotFoundException(ErrorMessages.NotFound<Well>());

            foreach (var history in wellHistories)
            {
                history.PreviousData = history.PreviousData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.PreviousData.ToString()!) : null;

                history.CurrentData = history.CurrentData is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.CurrentData.ToString()!) : null;

                history.FieldsChanged = history.FieldsChanged is not null ? JsonConvert.DeserializeObject<Dictionary<string, object>>(history.FieldsChanged.ToString()!) : null;
            }

            return wellHistories;
        }


        //public async Task<PaginatedDataDTO<WellDTO>> GetWells(int pageNumber, int pageSize, string requestUrl)
        //{
        //    var totalCount = await _context.Wells.CountAsync();

        //    var wells = await _context.Wells
        //            .Include(x => x.User)
        //            .Include(x => x.Completions)
        //            .Include(x => x.Field)
        //            .ThenInclude(f => f.Installation)
        //            .ThenInclude(i => i.Cluster)
        //            .OrderBy(x => x.Id)
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToListAsync();

        //    var wellsDTO = _mapper.Map<List<Well>, List<WellDTO>>(wells);

        //    var paginatedData = new PaginatedDataDTO<WellDTO>
        //    {
        //        Count = wellsDTO.Count,
        //        Data = wellsDTO
        //    };

        //    var previousPageNumber = pageNumber > 1 ? pageNumber - 1 : 0;
        //    var nextPageNumber = pageNumber * pageSize < totalCount ? pageNumber + 1 : 0;

        //    var uriBuilder = new UriBuilder(requestUrl);
        //    var queryParams = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        //    if (previousPageNumber > 0)
        //    {
        //        queryParams.Set("pageNumber", previousPageNumber.ToString());
        //        uriBuilder.Query = queryParams.ToString();
        //        paginatedData.PreviousPage = uriBuilder.ToString();
        //    }

        //    if (nextPageNumber > 0)
        //    {
        //        queryParams.Set("pageNumber", nextPageNumber.ToString());
        //        uriBuilder.Query = queryParams.ToString();
        //        paginatedData.NextPage = uriBuilder.ToString();
        //    }

        //    return paginatedData;
        //}

    }
}
