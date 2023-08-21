using PRIO.src.Modules.ControlAccess.Users.Dtos;

namespace PRIO.src.Modules.Measuring.Comments.Dtos
{
    public class CreateUpdateCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public UserDTO CommentedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
