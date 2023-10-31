namespace PRIO.src.Modules.FileExport.XLSX.Dtos
{
    public record ClosingOpeningFileXLSXDto
    (
        Guid Id,
        string FileName,
        string FileContent,
        string FileExtension,
        string Group
    );
}
