using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Application.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException() : base(ErrorCodes.NOT_FOUND) { }
    }
}