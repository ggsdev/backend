using AutoMapper;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Interfaces;
using PRIO.src.Modules.Measuring.Comments.ViewModels;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Comments.Infra.Http.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IProductionRepository _productionRepository;

        public CommentService(ICommentRepository commentRepository, IProductionRepository productionRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _productionRepository = productionRepository;
            _mapper = mapper;
        }

        public async Task<CreateUpdateCommentDto> CreateComment(CreateCommentViewModel body, User loggedUser)
        {
            var prodution = await _productionRepository
                .GetById(body.ProductionId);

            if (prodution is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (prodution.Comment is not null)
                throw new ConflictException("Produção já tem um comentário.");

            if (prodution.Oil is null)
                throw new ConflictException("Produção de óleo precisa ser fechada.");

            if (prodution.Gas is null)
                throw new ConflictException("Produção de gás precisa ser fechada.");

            if (prodution.WellProductions is null || prodution.WellProductions.Count == 0)
                throw new ConflictException("Apropriação da produção precisa ser feita.");

            var comment = new CommentInProduction
            {
                Id = Guid.NewGuid(),
                CommentedBy = loggedUser,
                Text = body.Text,
                Production = prodution,
            };

            await _commentRepository.AddAsync(comment);

            var commentDto = _mapper.Map<CreateUpdateCommentDto>(comment);

            prodution.StatusProduction = "fechado";
            _productionRepository.Update(prodution);

            await _commentRepository.Save();

            return commentDto;
        }

        public async Task<CreateUpdateCommentDto> UpdateComment(UpdateCommentViewModel body, Guid id, User loggedUser)
        {
            var comment = await _commentRepository
                .GetById(id);

            if (comment is null)
                throw new NotFoundException(ErrorMessages.NotFound<CommentInProduction>());

            if (comment.CommentedBy.Id != loggedUser.Id)
                throw new ConflictException("Comentário só pode ser atualizado por quem comentou.");

            var updatedProperties = UpdateFields
                .CompareUpdateReturnOnlyUpdated(comment, body);

            if (updatedProperties.Any() is false)
                throw new BadRequestException(ErrorMessages.UpdateToExistingValues<CommentInProduction>());

            _commentRepository.Update(comment);

            var commentDto = _mapper.Map<CreateUpdateCommentDto>(comment);

            await _commentRepository.Save();

            return commentDto;
        }
    }
}
