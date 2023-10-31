using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileExport.XLSX.ViewModels
{
    public class GenerateClosingOpeningRelatoryViewModel
    {
        [Required(ErrorMessage = "BeginningDate é obrigatório")]
        public DateTime? BeginningDate { get; set; } = null!;
        [Required(ErrorMessage = "EndDate é obrigatório")]
        public DateTime? EndDate { get; set; } = null!;
        [Required(ErrorMessage = "FieldId é obrigatório")]
        public Guid? FieldId { get; set; } = null!;
    }
}
