using BasicProjectTemplate.Application.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Queries;

public record BlackListedTokenQuery(string Token)
    : IRequest<bool>;


public sealed class BlackListedTokenQueryHandler
    : IRequestHandler<BlackListedTokenQuery, bool>
{
    private AppDbContext _dbContext;
    public BlackListedTokenQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> Handle(BlackListedTokenQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.BlacklistedTokens.SingleOrDefaultAsync(bt => bt.Token == request.Token, cancellationToken: cancellationToken);
        return result is not null;
    }
}
