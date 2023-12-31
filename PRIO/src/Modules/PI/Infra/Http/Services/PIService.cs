﻿using AutoMapper;
using dotenv.net;
using PRIO.src.Modules.Hierarchy.Installations.Dtos;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.PI.Dtos;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Modules.PI.ViewModels;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;
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
                                    WellId = well.Id,
                                    CategoryOperator = well.CategoryOperator,
                                    CreatedAt = attribute.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                                    Field = field.Name,
                                    Status = attribute.IsActive,
                                    Operational = attribute.IsOperating,
                                    Parameter = attribute.Element.Parameter,
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

        public async Task<AttributeReturnDTO> GetTagById(Guid id)
        {
            var tag = await _repository
                .GetById(id)
                ?? throw new NotFoundException("Tag não encontrada ou inativa.");

            var well = await _wellRepository
                .GetByNameOrOperatorName(tag.WellName);

            if (well is null || well.IsActive is false)
                throw new BadRequestException("Poço não encontrado ou inativo.");

            var tagDto = new AttributeReturnDTO
            {
                Id = tag.Id,
                CategoryOperator = well.CategoryOperator!,
                CreatedAt = tag.CreatedAt.ToString("dd/MMM/yyyy"),
                Field = well.Field.Name,
                GroupParameter = tag.Element.CategoryParameter,
                Operational = tag.IsOperating,
                Parameter = tag.Element.Parameter,
                Status = tag.IsActive,
                Tag = tag.Name,
                WellId = well.Id,
                WellName = well.Name!,
            };

            return tagDto;
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
                    WellId = well.Id,
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
                var wellNames = value.Attribute.WellName.Split(',');

                foreach (var wellInDatabase in wellNames)
                {
                    var well = await _wellRepository.GetByNameOrOperatorName(wellInDatabase);

                    var data = new HistoryValueDTO
                    {
                        TAG = value.Attribute.Name,
                        Date = value.Date,
                        Value = value.Amount,
                        Well = value.Attribute.WellName,
                        Field = well.Field.Name,
                        Installation = well.Field.Installation.Name,
                        IsCaptured = value.IsCaptured,
                    };
                    listValues.Add(data);
                }


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
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            if (body.CategoryParameter != PIConfig._flow && body.CategoryParameter != PIConfig._pressure)
                throw new BadRequestException("Categoria de paramêtro invalido.");

            if (body.CategoryParameter == PIConfig._pressure && PIConfig._pressureValues.Contains(body.Parameter) is false)
                throw new BadRequestException("Parâmetro de pressão inválido.");

            if (body.CategoryParameter == PIConfig._flow && PIConfig._flowValues.Contains(body.Parameter) is false)
                throw new BadRequestException("Parâmetro de vazão inválido.");

            if (body.StatusTag is false && body.Operational is true)
                throw new BadRequestException("Tag não pode estar inativa e operacional.");

            var well = await _wellRepository.GetByIdAsync(body.WellId)
                ?? throw new NotFoundException(ErrorMessages.NotFound<Well>());

            var tagExists = await _repository
                .AnyTag(body.TagName);

            if (tagExists)
                throw new ConflictException($"Já existe uma tag cadastrada com o nome: '{body.TagName}'.");

            var elementInDatabase = await _repository
                .GetElementByParameter(body.Parameter)
                ?? throw new ConflictException($"Parâmetro: '{body.Parameter}' não encontrado.");

            if (body.Operational is true)
            {
                var attributesOfWell = await _repository
                    .GetTagsByWellName(well.Name!, well.WellOperatorName!);

                foreach (var attributeOfWell in attributesOfWell)
                {
                    if (attributeOfWell.IsOperating)
                    {
                        if (body.Parameter == PIConfig._pdg2 && attributeOfWell.Element.Parameter == PIConfig._pdg1)
                            throw new ConflictException($"Somente um PDG pode estar operacional no poço, sensor PDG com tag: {attributeOfWell.Name} está operacional.");

                        else if (body.Parameter == PIConfig._pdg1 && attributeOfWell.Element.Parameter == PIConfig._pdg2)
                            throw new ConflictException($"Somente um PDG pode estar operacional no poço, sensor PDG com tag: {attributeOfWell.Name} está operacional.");
                    }
                }
            }

            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                using HttpClient client = new(handler);
                var username = PIConfig._user;
                var password = PIConfig._password;

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var attributeRoute = string.Empty;

                if (instance == PIConfig._forteInstance)
                    attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\prrj09srvvm02\\{body.TagName.Trim()}";

                if (instance == PIConfig._bravoInstance)
                    attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\PRRJTSRVVM06\\{body.TagName.Trim()}";

                if (instance == PIConfig._valenteInstance)
                    attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\BZFPSOPICOLLECTIVE\\{body.TagName.Trim()}";

                var response = await client
                    .GetAsync(attributeRoute);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Print(response.Content);
                    throw new BadRequestException("Algo deu errado.");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Print(response.Content);
                    throw new NotFoundException($"Tag: {body.TagName} não encontrada.");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();

                var attributeObject = JsonSerializer.Deserialize<PointJsonPIDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new BadRequestException("Formato do PI não corresponde ao esperado.");


                var createdTag = new EF.Models.Attribute
                {
                    Id = Guid.NewGuid(),
                    IsActive = body.StatusTag!.Value,
                    IsOperating = body.Operational!.Value,
                    Name = body.TagName,
                    WellName = well.Name!,
                    Element = elementInDatabase,
                    Description = body.Description,
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    WebId = attributeObject.WebId,
                    SelfRoute = attributeObject.Links.Self,
                    ValueRoute = attributeObject.Links.Value,
                    PIId = $"{attributeObject.Id}"
                };


                var attributeDto = new AttributeReturnDTO
                {
                    WellName = well.Name!,
                    WellId = well.Id,
                    CategoryOperator = well.CategoryOperator!,
                    Field = well.Field.Name,
                    GroupParameter = createdTag.Element.CategoryParameter,
                    Parameter = createdTag.Element.Parameter,
                    Operational = createdTag.IsOperating,
                    Status = createdTag.IsActive,
                    Id = createdTag.Id,
                    CreatedAt = createdTag.CreatedAt.ToString("dd/MM/yyyy"),
                    Tag = createdTag.Name,
                };

                await _repository.AddTag(createdTag);

                await _repository.SaveChanges();

                return attributeDto;
            }
            catch (HttpRequestException ex)
            {
                Print(ex);
                throw new BadRequestException("Erro ao se conectar ao PI");
            }
            catch (Exception ex)
            {
                Print(ex);
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


        public async Task<UpdateAttributeDto> UpdateById(Guid id, UpdateTagViewModel body)
        {
            var envVars = DotEnv.Read();
            var instance = envVars["INSTANCE"];

            if (body.Operational is true && body.StatusTag is false)
                throw new BadRequestException("Tag não pode ser operacional e estar inativa.");

            if (body.CategoryParameter != PIConfig._flow && body.CategoryParameter != PIConfig._pressure)
                throw new BadRequestException("Categoria de paramêtro invalido.");

            if (body.CategoryParameter is not null && body.Parameter is not null)
            {
                if (body.CategoryParameter == PIConfig._pressure && PIConfig._pressureValues.Contains(body.Parameter) is false)
                    throw new BadRequestException("Parâmetro de pressão inválido.");

                if (body.CategoryParameter == PIConfig._flow && PIConfig._flowValues.Contains(body.Parameter) is false)
                    throw new BadRequestException("Parâmetro de vazão inválido.");
            }

            var tag = await _repository
                .GetById(id)
                ?? throw new NotFoundException("Tag não encontrada ou inativa.");

            if (body.Parameter is not null && body.CategoryParameter is null)
            {
                if (tag.Element.CategoryParameter == PIConfig._pressure && PIConfig._pressureValues.Contains(body.Parameter) is false)
                    throw new BadRequestException("Parâmetro de pressão inválido.");

                if (tag.Element.CategoryParameter == PIConfig._flow && PIConfig._flowValues.Contains(body.Parameter) is false)
                    throw new BadRequestException("Parâmetro de vazão inválido.");
            }

            if (body.CategoryParameter is not null && body.Parameter is null)
            {
                if (body.CategoryParameter == PIConfig._pressure && PIConfig._pressureValues.Contains(tag.Element.Parameter) is false)
                    throw new BadRequestException("Categoria de parâmetro de pressão inválido.");

                if (body.CategoryParameter == PIConfig._flow && PIConfig._flowValues.Contains(tag.Element.Parameter) is false)
                    throw new BadRequestException("Categoria de parâmetro de vazão inválido.");
            }

            if (body.Operational is true && tag.IsActive is false)
                throw new BadRequestException("Tag não pode ser operacional e estar inativa.");

            if (body.TagName is not null)
            {
                var tagExists = await _repository
                    .AnyTag(body.TagName, tag.Id);

                if (tagExists)
                    throw new ConflictException($"Já existe uma tag cadastrada com o nome: '{body.TagName}'.");
            }

            if (body.Parameter is not null)
            {
                var elementInDatabase = await _repository
                .GetElementByParameter(body.Parameter)
                ?? throw new ConflictException($"Parâmetro: '{body.Parameter}' não encontrado.");

                tag.Element = elementInDatabase;
            }


            if (body.Operational is not null && tag.IsOperating != body.Operational)
            {
                var wellNames = tag.WellName
                    .Split(',');

                if (body.Operational is true && body.Parameter == PIConfig._pdg1 || body.Parameter == PIConfig._pdg2)
                    foreach (var wellName in wellNames)
                    {
                        var attributesOfWell = await _repository
                            .GetTagsByWellName(wellName, wellName);

                        foreach (var attributeOfWell in attributesOfWell)
                        {

                            if (attributeOfWell.IsOperating && body.Parameter is not null && body.Parameter != attributeOfWell.Element.Parameter)
                            {
                                if (body.Parameter == PIConfig._pdg2 && attributeOfWell.Element.Parameter == PIConfig._pdg1)
                                    throw new ConflictException($"Somente um PDG pode estar operacional no poço, sensor PDG com tag: {wellName} está operacional.");

                                else if (body.Parameter == PIConfig._pdg1 && attributeOfWell.Element.Parameter == PIConfig._pdg2)
                                    throw new ConflictException($"Somente um PDG pode estar operacional no poço, sensor PDG com tag: {wellName} está operacional.");
                            }

                        }
                    }

            }


            try
            {
                if (body.TagName is not null && body.TagName.ToUpper().Trim() != tag.Name.ToUpper().Trim())
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                    };

                    using HttpClient client = new(handler);

                    var username = PIConfig._user;
                    var password = PIConfig._password;

                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                    var attributeRoute = string.Empty;

                    if (instance == "FORTE")
                        attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\prrj09srvvm02\\{body.TagName.Trim()}";

                    if (instance == "BRAVO")
                        attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\PRRJTSRVVM06\\{body.TagName.Trim()}";

                    if (instance == "VALENTE")
                        attributeRoute = $"https://prrjbsrvvm170.petrorio.local/piwebapi/points?path=\\\\BZFPSOPICOLLECTIVE\\{body.TagName.Trim()}";

                    var response = await client
                        .GetAsync(attributeRoute);

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        Print(response.Content);
                        throw new BadRequestException("Algo deu errado.");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Print(response.Content);
                        throw new NotFoundException($"Tag: {body.TagName} não encontrada no PI.");
                    }

                    var jsonContent = await response.Content.ReadAsStringAsync();

                    var attributeObject = JsonSerializer.Deserialize<PointJsonPIDTO>(jsonContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? throw new BadRequestException("Formato do PI não corresponde ao esperado.");

                    tag.WebId = attributeObject.WebId;
                    tag.ValueRoute = attributeObject.Links.Value;
                    tag.SelfRoute = attributeObject.Links.Self;

                    tag.Name = body.TagName;
                }


                tag.IsOperating = body.Operational is not null ? body.Operational.Value : tag.IsOperating;
                tag.Description = body.Description is not null ? body.Description : tag.Description;
                tag.IsActive = body.StatusTag is not null ? body.StatusTag.Value : tag.IsActive;

                var wellInDatabase = await _wellRepository.GetByNameOrOperatorName(tag.WellName);

                var attributeDto = new UpdateAttributeDto
                {
                    GroupParameter = tag.Element.CategoryParameter,
                    Parameter = tag.Element.Parameter,
                    Operational = tag.IsOperating,
                    Status = tag.IsActive,
                    Id = tag.Id,
                    Tag = tag.Name,
                };

                _repository.Update(tag);

                await _repository.SaveChanges();

                return attributeDto;
            }
            catch (HttpRequestException)
            {
                throw new BadRequestException("Erro ao se conectar ao PI.");
            }
        }

        private static void Print<T>(T text)
        {
            Console.WriteLine(text);
        }
    }
}
