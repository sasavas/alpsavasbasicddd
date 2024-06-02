using BasicProjectTemplate.Application.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public sealed record DeleteUserCommand(Guid UserId) : IRequest;

public class DeleteUserCommandHandler(
    AppDbContext dbContext,
    ILogger<DeleteUserCommandHandler> logger) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);
            if (user is not null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not delete User {userId}", request.UserId);
        }
    }
}