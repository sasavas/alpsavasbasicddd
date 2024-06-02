using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Application.UseCases.Users.Exceptions;

public class UserNotVerifiedException : AppException
{
    public UserNotVerifiedException() : base(ErrorCodes.USER_NOT_VERIFIED)
    {
    }
}