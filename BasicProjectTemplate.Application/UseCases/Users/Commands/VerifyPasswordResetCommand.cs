using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.Application.UseCases.Users.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public record VerifyPasswordResetCommand(Guid Code, string NewPassword) : IRequest;

public class VerifyPasswordResetRequestHandler(
    ILogger<VerifyPasswordResetRequestHandler> logger,
    AppDbContext dbContext)
    : IRequestHandler<VerifyPasswordResetCommand>
{
    public async Task Handle(VerifyPasswordResetCommand command, CancellationToken cancellationToken)
    {
        var foundUser = await dbContext.Users.SingleOrDefaultAsync(u => u.VerificationCode == command.Code, cancellationToken: cancellationToken)
            ?? throw new NotFoundException();

        var hasValidPasswordResetRequest = foundUser.HasValidPasswordResetRequest();
        if (!hasValidPasswordResetRequest)
        {
            throw new PasswordResetRequestExpiredException();
        } 
        
        try
        {
            foundUser.UpdatePassword(command.NewPassword);
            dbContext.Users.Update(foundUser);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not verify password reset");
        }
    }
}