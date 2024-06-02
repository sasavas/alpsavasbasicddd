using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Application.UseCases.Users.Exceptions;

public class UserWithSameEmailAlreadyExistsException : AppException
{
    public UserWithSameEmailAlreadyExistsException() : base(ErrorCodes.USER_EMAIL_TAKEN)
    {
    }
}