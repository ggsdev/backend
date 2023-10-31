using System.ComponentModel.DataAnnotations;

namespace PRIO.src.Modules.FileExport.Templates.Infra.EF.Enums
{
    public enum TypeFile
    {
        [EnumDataType(typeof(TypeFile), ErrorMessage = "Invalid enum")]
        XML042 = 1,
        WellTestXLS = 2,
        WellTestPDF = 3,
        ClosingAndOpeningEventsXLS = 4,
        ProductionHistory = 5,
        InjectionHistory = 6,

        BMP92XLS = 7,
        BMP93XLS = 8,
        BMP92PDF = 9,
        BMP93PDF = 10,
    }
}
