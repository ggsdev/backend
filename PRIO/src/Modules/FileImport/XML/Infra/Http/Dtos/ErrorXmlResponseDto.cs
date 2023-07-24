namespace PRIO.src.Modules.FileImport.XML.Infra.Http.Dtos
{
    public class ErrorXmlResponseDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();


    }
}
