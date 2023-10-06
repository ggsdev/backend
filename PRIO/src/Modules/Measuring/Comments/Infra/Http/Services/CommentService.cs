using AutoMapper;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;
using PRIO.src.Modules.Balance.Balance.Interfaces;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Interfaces;
using PRIO.src.Modules.Hierarchy.Installations.Interfaces;
using PRIO.src.Modules.Hierarchy.Wells.Interfaces;
using PRIO.src.Modules.Measuring.Comments.Dtos;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Interfaces;
using PRIO.src.Modules.Measuring.Comments.ViewModels;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Productions.Interfaces;
using PRIO.src.Modules.PI.Interfaces;
using PRIO.src.Shared.Errors;
using PRIO.src.Shared.Utils;

namespace PRIO.src.Modules.Measuring.Comments.Infra.Http.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IProductionRepository _productionRepository;
        private readonly IInstallationRepository _installationRepository;
        private readonly IPIRepository _PIRepository;
        private readonly IWellRepository _wellRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IBTPRepository _btpRepository;

        public CommentService(ICommentRepository commentRepository, IProductionRepository productionRepository, IMapper mapper, IBTPRepository bTPRepository, IInstallationRepository installationRepository, IBalanceRepository balanceRepository, IWellRepository wellRepository, IPIRepository PIRepository)
        {
            _commentRepository = commentRepository;
            _productionRepository = productionRepository;
            _mapper = mapper;
            _btpRepository = bTPRepository;
            _installationRepository = installationRepository;
            _balanceRepository = balanceRepository;
            _wellRepository = wellRepository;
            _PIRepository = PIRepository;
        }

        public async Task<CreateUpdateCommentDto> CreateComment(CreateCommentViewModel body, User loggedUser, Guid productionId)
        {
            var production = await _productionRepository
                .GetById(productionId);

            if (production is null)
                throw new NotFoundException(ErrorMessages.NotFound<Production>());

            if (production.Comment is not null)
                throw new ConflictException("Produção já tem um comentário.");

            if (production.Oil is null)
                throw new ConflictException("Produção de óleo precisa ser fechada.");

            if (production.Gas is null)
                throw new ConflictException("Produção de gás precisa ser fechada.");

            if (production.WellProductions is null || production.WellProductions.Any() is false)
                throw new ConflictException("Apropriação da produção precisa ser feita.");

            var comment = new CommentInProduction
            {
                Id = Guid.NewGuid(),
                CommentedBy = loggedUser,
                Text = body.Text,
                Production = production,
            };


            await _commentRepository.AddAsync(comment);

            await CreateBalance(production, loggedUser);

            var commentDto = _mapper.Map<CreateUpdateCommentDto>(comment);

            production.StatusProduction = "fechado";
            _productionRepository.Update(production);

            await _commentRepository.Save();

            return commentDto;
        }
        private async Task CreateBalance(Production production, User user)
        {
            var productionDate = production.MeasuredAt;
            var installationsFromUEP = await _installationRepository.GetInstallationChildrenOfUEP(production.Installation.UepCod);

            var balanceUEP = new UEPsBalance
            {
                Id = Guid.NewGuid(),
                MeasurementAt = productionDate,
                IsActive = true,
            };
            await _balanceRepository.AddUEPBalance(balanceUEP);
            foreach (var installation in installationsFromUEP)
            {
                var balanceInstallation = new InstallationsBalance
                {
                    Id = Guid.NewGuid(),
                    MeasurementAt = productionDate,
                    IsActive = true,
                    UEPBalance = balanceUEP,
                    Installation = installation
                };
                await _balanceRepository.AddInstallationBalance(balanceInstallation);
                foreach (var field in installation.Fields)
                {
                    var fieldProduction = await _productionRepository.GetFieldProductionByFieldAndProductionId(field.Id, production.Id);
                    var balanceField = new FieldsBalance
                    {
                        Id = Guid.NewGuid(),
                        MeasurementAt = productionDate,
                        IsActive = true,
                        IsParameterized = false,
                        installationBalance = balanceInstallation,
                        TotalWaterProduced = fieldProduction is not null ? fieldProduction.WaterProductionInField : 0,
                    };
                    await _balanceRepository.AddFieldBalance(balanceField);

                    foreach (var well in field.Wells)
                    {
                        var tag = await _wellRepository.GetTagFromWell(well.Name, well.WellOperatorName);
                        if (tag is not null)
                        {
                            var wellValue = await _PIRepository.GetWellValuesWithChildrens(production.MeasuredAt, well.Id);
                            if (wellValue is not null)
                            {
                                var injectionWaterWell = new InjectionWaterWell
                                {
                                    Id = Guid.NewGuid(),
                                    WellValues = wellValue,
                                    AssignedValue = wellValue.Value.Amount is not null ? wellValue.Value.Amount.Value : 0,
                                    CreatedBy = user,
                                    MeasurementAt = production.MeasuredAt,
                                };
                                await _balanceRepository.AddFieldBalance(balanceField);

                            }
                            else
                            {

                            }
                        }

                    }
                }
            }

            //subir um nivel na injecao de agua do poço
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
