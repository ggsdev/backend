using PRIO.src.Modules.FileImport.XLSX.BTPS.Dtos;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.ViewModels
{
    public class ImportViewModel
    {
        public ValidateImportViewModel Validate { get; set; }
        public BTPDataDTO Data { get; set; }
    }
}
