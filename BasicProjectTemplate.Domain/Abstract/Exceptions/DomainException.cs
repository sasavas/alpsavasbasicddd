using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Domain.Abstract.Exceptions;

public class DomainException : BaseException
{
    protected DomainException(ErrorCode error) : base(error)
    {
    }
}