using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public record CompleteOnboardingCommand(
    string? FirstName,
    string? LastName,
    string? Gender,
    DateOnly? DateOfBirth) : IRequest;

public class CompleteOnboardingRequestHandler(
    // IUnitOfWork unitOfWork,
    // IUserRepository userRepository,
    IUserIdProvider userIdProvider,
    AppDbContext dbContext,
    ILogger<CompleteOnboardingRequestHandler> logger)
    : IRequestHandler<CompleteOnboardingCommand>
{
    public async Task Handle(CompleteOnboardingCommand command, CancellationToken cancellationToken)
    {
        var foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userIdProvider.GetUserId(), cancellationToken: cancellationToken)
                        ?? throw new NotFoundException();

        if (command.FirstName != null) foundUser.FirstName = command.FirstName;
        if (command.LastName != null) foundUser.LastName = command.LastName;
        if (command.DateOfBirth != null) foundUser.DateOfBirth = command.DateOfBirth;
        if (command.Gender != null) foundUser.Gender = new Gender(command.Gender);
        
        try
        {
            dbContext.Update(foundUser);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while saving User Onboarding Info");
        }
    }
}