namespace PRIO.src.Modules.FileExport.Templates.Dtos
{
    public record TemplateDto
    (
        Guid Id,
        string FileName,
        string FileExtension,
        string TypeFile,
        string FileContent
    );
}
