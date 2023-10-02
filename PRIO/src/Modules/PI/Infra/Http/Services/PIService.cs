using AutoMapper;

using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.PI.Dtos;
using PRIO.src.Modules.PI.Infra.EF.Models;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Infra.EF;
using System.Text.Json;

using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;



namespace PRIO.src.Modules.PI.Infra.Http.Services
{
    public class PIService
    {
        private readonly IPIRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWellRepository _wellRepository;

        public PIService(IPIRepository repository, IInstallationRepository installationRepository, IMapper mapper, IWellRepository wellRepository)
        {
            _repository = repository;
            _installationRepository = installationRepository;
            _mapper = mapper;
            _wellRepository = wellRepository;
        }

        public async Task<List<UepAttributesWellsDto>> GetDataByUep()
        {
            var ueps = await _installationRepository
                .GetUEPsAsync();

            var result = new List<UepAttributesWellsDto>();

            foreach (var uep in ueps)
            {
                var installations = await _installationRepository
                    .GetInstallationChildrenOfUEP(uep.UepCod);

                var installationsDto = new List<InstallationWithAttributesDTO>();

                foreach (var installation in installations)
                {
                    var attributesList = new List<AttributeDTO>();

                    foreach (var field in installation.Fields)
                    {
                        foreach (var well in field.Wells)
                        {
                            var attributesOfWell = await _repository.GetTagsByWellName(well.Name, well.WellOperatorName);

                            foreach (var attribute in attributesOfWell)
                            {

                                attributesList.Add(new AttributeDTO
                                {
                                    Id = attribute.Id,
                                    CategoryOperator = well.CategoryOperator,
                                    CreatedAt = attribute.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                                    Field = field.Name,
                                    Status = attribute.IsActive,
                                    Operational = attribute.IsOperating,
                                    Parameter = attribute.Element.Name,
                                    Tag = attribute.Name,
                                    WellName = well.Name,
                                    GroupParameter = attribute.Element.CategoryParameter,
                                });
                            }
                        }
                    }

                    var installationDto = _mapper.Map<InstallationWithAttributesDTO>(installation);
                    installationDto.Attributes = attributesList;

                    installationsDto.Add(installationDto);
                }

                result.Add(new UepAttributesWellsDto
                {
                    Installations = installationsDto,
                    UepId = uep.Id,
                    UepName = uep.Name
                });
            }


            return result;
        }

        public async Task<List<AttributeDTO>> GetTagsByWellName(string wellName, string wellOperatorName)
        {
            var attributesOfWell = await _repository.GetTagsByWellName(wellName, wellOperatorName);
            var attributesList = new List<AttributeDTO>();

            foreach (var attr in attributesOfWell)
            {

                var well = await _wellRepository
                    .GetByNameOrOperator(wellName, wellOperatorName);

                attributesList.Add(new AttributeDTO
                {
                    WellName = well.Name,
                    CategoryOperator = well.CategoryOperator,
                    Field = well.Field.Name,
                    GroupParameter = attr.Element.CategoryParameter,
                    Parameter = attr.Element.Parameter,
                    Operational = attr.IsOperating,
                    Status = attr.IsActive,
                    Id = attr.Id,
                    CreatedAt = attr.CreatedAt.ToString("dd/MM/yyyy"),
                    Tag = attr.Name
                });

            }

            return attributesList;
        }


        public async Task<List<HistoryValueDTO>> GetHistoryByDate(string date)
        {
            if (date == null)
                throw new ConflictException("Data não informada.");

            var checkDate = DateTime.TryParse(date, out DateTime day);
            if (checkDate is false)
                throw new ConflictException("Data não é válida.");

            var dateToday = DateTime.UtcNow.AddHours(-3).Date;
            if (dateToday <= day)
                throw new NotFoundException("Downtime não foi fechado para esse dia.");

            var GetValuesByDate = await _repository.GetValuesByDate(dateToday.Date);
            List<HistoryValueDTO> listValues = new List<HistoryValueDTO>();

            foreach (var value in GetValuesByDate)
            {
                var well = await _wellRepository.GetByNameOrOperatorName(value.Attribute.WellName);
                var data = new HistoryValueDTO
                {
                    TAG = value.Attribute.Name,
                    Date = value.Date,
                    Value = value.Amount,
                    Well = value.Attribute.WellName,
                    Field = well.Field.Name,
                    Installation = well.Field.Installation.Name
                };
                listValues.Add(data);
            }
            return listValues;
        }
      
        //public async Task<AttributeDTO> CreateTag(CreateTagViewModel body)
        //{
        //    var createdTag = new Attribute
        //    {
        //        Id = Guid.NewGuid(),




        //    };



        //}


        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }


    }
}
