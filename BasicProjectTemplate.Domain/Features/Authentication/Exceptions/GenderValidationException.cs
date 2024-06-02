using BasicProjectTemplate.Domain.Abstract.Exceptions;
using BasicProjectTemplate.SharedLibrary.Exceptions;

namespace BasicProjectTemplate.Domain.Features.Authentication.Exceptions;

public class GenderValidationException : ValidationException
{
    public GenderValidationException() : base(ErrorCodes.USER_NOT_VALID_GENDER)
    {
    }
}