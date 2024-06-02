using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Domain.Abstract.Exceptions;

public class ValidationException : BaseException
{
    protected ValidationException(ErrorCode error) : base(error)
    {
    }
}