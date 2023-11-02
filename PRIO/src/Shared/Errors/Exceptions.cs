using PRIO.src.Modules.FileImport.XML.Measuring.Infra.Http.Dtos;

namespace PRIO.src.Shared.Errors
{
    public class BadRequestException : Exception
    {
        public ErrorXmlResponseDto? Error { get; }
        public List<string>? Errors { get; }
        public string? ReturnStatus { get; }
        public List<FileErrorDto>? DifferentDates { get; }
        public List<FilesDuplicated>? DuplicatedFiles { get; }
        public DateTime? ReferenceDate { get; }

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

        public BadRequestException(string message, List<FileErrorDto>? differentDates, DateTime? referenceDate)
            : base(message)
        {
            DifferentDates = differentDates;
            ReferenceDate = referenceDate;
        }

        public BadRequestException(string message, List<FilesDuplicated>? duplicatedFiles)
            : base(message)
        {
            DuplicatedFiles = duplicatedFiles;
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
