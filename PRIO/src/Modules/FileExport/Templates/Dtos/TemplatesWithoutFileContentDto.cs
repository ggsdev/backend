namespace PRIO.src.Modules.FileExport.Templates.Dtos
{
    public record TemplatesWithoutFileContentDto
    (
        Guid Id,
        string FileName,
        string FileExtension,
        string TypeFile
    );
}
