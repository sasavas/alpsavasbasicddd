using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Domain.Features.Authentication.RoleModule;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Queries;

public record UserPermissionQuery(Guid UserId) : IRequest<IEnumerable<Permission>?>;

public class UserPermissionQueryHandler : IRequestHandler<UserPermissionQuery, IEnumerable<Permission>?>
{
    private readonly AppDbContext _appDbContext;

    public UserPermissionQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Permission>?> Handle(UserPermissionQuery query, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Users
            .Include(u => u.Role.Permissions)
            .SingleOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);

        var permissions = result?.Role.Permissions;
        return permissions;
    }
}