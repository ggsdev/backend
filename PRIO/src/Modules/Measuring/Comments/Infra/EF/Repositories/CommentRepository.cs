using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;
using PRIO.src.Modules.Measuring.Comments.Interfaces;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.src.Modules.Measuring.Comments.Infra.EF.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CommentInProduction>> GetAllComments()
        {
            return await _context.Comments
                .Include(x => x.Production)
                .Include(x => x.CommentedBy)
                .ToListAsync();
        }

        public async Task<CommentInProduction?> GetById(Guid id)
        {
            return await _context.Comments
                .Include(x => x.Production)
                .Include(x => x.CommentedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(CommentInProduction comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public void Update(CommentInProduction comment)
        {
            _context.Comments.Update(comment);
        }
        public void Delete(CommentInProduction comment)
        {
            _context.Comments.Remove(comment);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
