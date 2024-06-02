using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.Application.UseCases.Users.Exceptions;
using BasicProjectTemplate.Domain.Features.Authentication;
using BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;
using BasicProjectTemplate.SharedLibrary.AzureServiceBus.EmailQueue;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public record RegisterUserCommand(
    string Email,
    string Password,
    string LanguageCode) : IRequest<User>;

public class RegisterUserCommandHandler(
    AppDbContext dbContext,
    ILogger<RegisterUserCommandHandler> logger) : IRequestHandler<RegisterUserCommand, User>
{
    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == new Email(request.Email), cancellationToken: cancellationToken);
        if (existingUser is not null)
        {
            throw new UserWithSameEmailAlreadyExistsException();
        }

        var defaultUserRole = dbContext.Roles.SingleOrDefault(r => r.Name.Equals("User")) ??
                              throw new NotFoundException();
        
        try
        {
            // create and save the user
            var verificationCode = Guid.NewGuid();
            
            var user = User.Create(new Email(request.Email),
                                   new Password(request.Password),
                                   defaultUserRole,
                                   verificationCode);
            
            var createdUser = await dbContext.Users.AddAsync(user, cancellationToken);
            
            //TODO: Send email to user-to-register
            var email = new EmailQueueMessageBody(request.Email,
                    "Welcome to MyApp",
                    $"""
                         <h1>Welcome to MyApp!</h1>
                         <p>Please verify to complete your registration process</p>
                         <a href="http://localhost:4200/lobby/verification/{verificationCode}">Click to Verify</a>
                     """);

            await dbContext.SaveChangesAsync(cancellationToken);

            return createdUser.Entity;
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, e, "Error occurred while user registration");
            throw;
        }
    }
}