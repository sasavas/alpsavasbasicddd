using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Application.UseCases.Users.Exceptions;

public class PasswordResetRequestExpiredException : AppException
{
    public PasswordResetRequestExpiredException() 
        : base(ErrorCodes.USER_PASSWORD_RESET_REQUEST_EXPIRED)
    {
    }
}