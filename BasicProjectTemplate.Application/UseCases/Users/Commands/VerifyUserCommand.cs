using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public record VerifyUserCommand(string verificationCode) : IRequest<bool>;

public class VerifyUserCommandHandler(
    AppDbContext dbContext,
    ILogger<VerifyUserCommandHandler> logger)
    : IRequestHandler<VerifyUserCommand, bool>
{
    public async Task<bool> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(
            u => u.VerificationCode == Guid.Parse(request.verificationCode), cancellationToken: cancellationToken);
        if (user is null)
        {
            throw new NotFoundException();
        }

        try
        {
            user.IsVerified = true;
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not verify user");
            throw;
        }
    }
}