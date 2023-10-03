using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.PI.ViewModels
{
    public class CreateTagViewModel
    {
        [Required(ErrorMessage = "TagName é obrigatório")]
        public string TagName { get; set; }
        [Required(ErrorMessage = "WellId é obrigatório")]
        public Guid WellId { get; set; }

        [Required(ErrorMessage = "CategoryParameter é obrigatório")]
        public string CategoryParameter { get; set; }
        [Required(ErrorMessage = "Parameter é obrigatório")]
        public string Parameter { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "StatusTag é obrigatório")]
        public bool StatusTag { get; set; }
        [Required(ErrorMessage = "Operational é obrigatório")]
        public bool Operational { get; set; }
    }
}
