using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Domain.Features.Authentication.RoleModule;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Queries;

public sealed record UserRoleQuery(Guid UserId) : IRequest<Role>;

public sealed class UserRoleQueryHandler(AppDbContext dbContext) 
    : IRequestHandler<UserRoleQuery, Role>
{
    public async Task<Role> Handle(UserRoleQuery query, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);
        return user!.Role;
    }
}