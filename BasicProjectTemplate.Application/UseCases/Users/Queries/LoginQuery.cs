using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.Domain.Features.Authentication;
using BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Queries;

public record LoginQuery(string Email, string Password)
    : IRequest<User>;

public sealed class LoginQueryHandler
    : IRequestHandler<LoginQuery, User>
{
    private readonly AppDbContext _dbContext;

    public LoginQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(
            u => u.Email == new Email(query.Email) && u.Password == new Password(query.Password),
            cancellationToken: cancellationToken);

        if (user is null)
            throw new NotFoundException();

        //TODO:production
        // if (user.IsVerified == false)
        //     throw new UserNotVerifiedException();

        return user;
    }
}