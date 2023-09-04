using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Comments.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = "Text é obrigatório")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "ProductionId é obrigatório")]
        public Guid? ProductionId { get; set; }
    }
}
