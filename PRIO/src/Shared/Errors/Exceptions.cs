using PRIO.src.Modules.FileImport.XML.Infra.Http.Dtos;

namespace PRIO.src.Shared.Errors
{
    public class BadRequestException : Exception
    {
        public ErrorXmlResponseDto? Error { get; }
        public List<string>? Errors { get; }
        public string? ReturnStatus { get; }

        public BadRequestException()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }
        public BadRequestException(string message, string status)
            : base(message)
        {
            ReturnStatus = status;
        }

        public BadRequestException(string message, List<string> errors)
            : base(message)
        {
            Errors = errors;
        }

        public BadRequestException(ErrorXmlResponseDto error)
        {
            Error = error;
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string message)
            : base(message)
        {
        }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
