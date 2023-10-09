﻿using AutoMapper;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.PI.Dtos;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Modules.PI.ViewModels;
using PRIO.src.Shared.Errors;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PRIO.src.Modules.PI.Infra.Http.Services
{
    public class PIService
    {
        private readonly IPIRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWellRepository _wellRepository;
        private readonly IInstallationRepository _installationRepository;

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
                    var attributesList = new List<AttributeReturnDTO>();

                    foreach (var field in installation.Fields)
                    {
                        foreach (var well in field.Wells)
                        {
                            var attributesOfWell = await _repository.GetTagsByWellName(well.Name, well.WellOperatorName);

                            foreach (var attribute in attributesOfWell)
                            {

                                attributesList.Add(new AttributeReturnDTO
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

        public async Task<List<AttributeReturnDTO>> GetTagsByWellName(string wellName, string wellOperatorName)
        {
            var attributesOfWell = await _repository
                .GetTagsByWellName(wellName, wellOperatorName);

            var attributesList = new List<AttributeReturnDTO>();

            foreach (var attr in attributesOfWell)
            {
                var well = await _wellRepository
                    .GetByNameOrOperator(wellName, wellOperatorName);

                attributesList.Add(new AttributeReturnDTO
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

            var GetValuesByDate = await _repository.GetValuesByDate(day.Date);

            List<HistoryValueDTO> listValues = new();

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

        public async Task<List<AttributeDTO>> GetAttributesByWell(Guid wellId)
        {
            var well = await _wellRepository.GetByIdAsync(wellId) ?? throw new NotFoundException("Poço não encontrado.");

            var Tags = await _repository.GetTagsByWellName(well.Name, well.WellOperatorName);

            var TagsDTO = _mapper.Map<List<EF.Models.Attribute>, List<AttributeDTO>>(Tags);

            return TagsDTO;
        }

        public async Task<AttributeReturnDTO> CreateTag(CreateTagViewModel body)
        {
            var well = await _wellRepository.GetByIdAsync(body.WellId)
                ?? throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var tagExists = await _repository
                .AnyTag(body.TagName);

            if (tagExists)
                throw new ConflictException($"Já existe uma tag cadastrada com o nome: '{body.TagName}'.");

            var elementInDatabase = await _repository
                .GetElementByParameter(body.Parameter)
                ?? throw new ConflictException($"Parâmetro: '{body.Parameter}' não encontrado.");
            try
            {

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using HttpClient client = new(handler);
                var username = "svc-pi-frade";
                var password = "S6_5q2C?=%ff";

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var atributesRoute = elementInDatabase.AttributesRoute;

                var response = await client
                    .GetAsync(atributesRoute);

                if (response.IsSuccessStatusCode is false)
                    throw new BadRequestException("Conexão com PI falhou.");

                var jsonContent = await response.Content.ReadAsStringAsync();

                var elementObject = JsonSerializer.Deserialize<ItemsElementJson>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new BadRequestException("Não foi possível deserializar o json");

                var attribute = elementObject.Items
                    .FirstOrDefault(x => x.Name.ToUpper().Trim().Contains(body.TagName.ToUpper().Trim()))
                    ?? throw new NotFoundException($"Tag: {body.TagName} não encontrada no PI.");

                var createdTag = new EF.Models.Attribute
                {
                    Id = Guid.NewGuid(),
                    IsActive = body.StatusTag,
                    IsOperating = body.Operational,
                    Name = body.TagName,
                    WellName = well.Name,
                    Element = elementInDatabase,
                    Description = body.Description,
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    WebId = attribute.WebId,
                    PIId = attribute.Id,
                    SelfRoute = attribute.Links.Self,
                    ValueRoute = attribute.Links.Value,
                };

                await _repository.AddTag(createdTag);

                var attributeDto = new AttributeReturnDTO
                {
                    WellName = well.Name,
                    CategoryOperator = well.CategoryOperator,
                    Field = well.Field.Name,
                    GroupParameter = createdTag.Element.CategoryParameter,
                    Parameter = createdTag.Element.Parameter,
                    Operational = createdTag.IsOperating,
                    Status = createdTag.IsActive,
                    Id = createdTag.Id,
                    CreatedAt = createdTag.CreatedAt.ToString("dd/MM/yyyy"),
                    Tag = createdTag.Name,

                };

                await _repository.SaveChanges();

                return attributeDto;
            }
            catch (HttpRequestException)
            {
                throw new BadRequestException("Erro ao se conectar ao PI");
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Aconteceu algo de errado: {ex.Message}");
            }
        }

        //public async Task<List<UepAndFieldsOperationalDTO>> GetFieldsOperationData()
        //{
        //    var ueps = await _installationRepository
        //       .GetUEPsAsync();

        //    var result = new List<UepAndFieldsOperationalDTO>();

        //    foreach (var uep in ueps)
        //    {
        //        var installations = await _installationRepository
        //            .GetInstallationChildrenOfUEP(uep.UepCod);

        //        var installationsDto = new List<InstallationWithFieldsOperationalDTO>();

        //        foreach (var installation in installations)
        //        {
        //            var fieldsList = new List<FieldWithOperationalData>();

        //            foreach (var field in installation.Fields)
        //            {
        //                var fieldsDTO = new FieldWithOperationalData(field.Id, field.IsActive, field.Name, field.CreatedAt.ToString("dd/MMM/yyyy", new CultureInfo("pt-BR")));

        //                fieldsList.Add(fieldsDTO);
        //            }

        //            var installationDTO = new InstallationWithFieldsOperationalDTO(installation.Id, installation.Name, installation.UepCod, installation.UepName, installation.CodInstallationAnp, installation.GasSafetyBurnVolume, installation.Description, installation.CreatedAt.ToString("dd/MM/yyyy"), installation.UpdatedAt, installation.IsActive, fieldsList);

        //            installationsDto.Add(installationDTO);
        //        }

        //        result.Add(new UepAndFieldsOperationalDTO
        //        (
        //            uep.Id,
        //            uep.Name,
        //            installationsDto
        //        ));
        //    }


        //    return result;
        //}

        public async Task GetOperationalParametersByFieldId(Guid fieldId)
        {



        }

        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}