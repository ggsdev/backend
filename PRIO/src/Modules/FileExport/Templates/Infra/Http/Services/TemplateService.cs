using AutoMapper;
using PRIO.src.Modules.FileExport.Templates.Dtos;
using PRIO.src.Modules.FileExport.Templates.Interfaces;

namespace PRIO.src.Modules.FileExport.Templates.Infra.Http.Services
{
    public class TemplateService
    {
        private readonly ITemplateRepository _repository;
        private readonly IMapper _mapper;

        public TemplateService(ITemplateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TemplatesWithoutFileContentDto>> GetTemplates()
        {
            var templates = await _repository.GetAll();

            var templatesDto = _mapper.Map<List<TemplatesWithoutFileContentDto>>(templates);

            return templatesDto;
        }

        public async Task<TemplateDto> GetTemplateById(Guid id)
        {
            var template = await _repository.GetById(id);

            var templateDto = _mapper.Map<TemplateDto>(template);

            return templateDto;
        }
    }
}
