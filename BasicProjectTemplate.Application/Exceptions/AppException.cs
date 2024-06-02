using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Application.Exceptions;

public class AppException : BaseException
{
    public AppException(ErrorCode errorCode) : base(errorCode)
    {
    }
}