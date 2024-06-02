using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Domain.Features.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public sealed record LogoutUserCommand(string Token, DateTime ActualExpiryDate) : IRequest;

public sealed class LogoutUserCommandHandler(
    ILogger<LogoutUserCommandHandler> logger,
    AppDbContext dbContext)
    : IRequestHandler<LogoutUserCommand>
{
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.BlacklistedTokens.AddAsync(
                BlacklistedToken.Create(request.Token, DateTime.UtcNow, request.ActualExpiryDate.ToUniversalTime()),
                cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not log out user");
        }
    }
}