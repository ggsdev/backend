using AutoMapper;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.PI.Dtos;
using PRIO.src.Modules.PI.Interfaces;

namespace PRIO.src.Modules.PI.Infra.Http.Services
{
    public class PIService
    {
        IPIRepository _repository;
        IMapper _mapper;
        IInstallationRepository _installationRepository;
        IWellRepository _wellRepository;

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
