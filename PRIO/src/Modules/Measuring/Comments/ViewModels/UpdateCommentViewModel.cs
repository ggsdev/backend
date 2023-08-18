using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.Measuring.Comments.ViewModels
{
    public class UpdateCommentViewModel
    {
        [Required(ErrorMessage = "Text é obrigatório")]
        public string Text { get; set; }
    }
}
