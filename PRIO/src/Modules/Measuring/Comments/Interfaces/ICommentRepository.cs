
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Comments.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentInProduction>> GetAllComments();
        Task<CommentInProduction?> GetById(Guid id);
        Task AddAsync(CommentInProduction comment);
        void Update(CommentInProduction comment);
        void Delete(CommentInProduction comment);
        Task Save();
    }
}
