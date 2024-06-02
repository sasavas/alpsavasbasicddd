using BasicProjectTemplate.Application.DataAccess;
using BasicProjectTemplate.Application.Exceptions;
using BasicProjectTemplate.Domain.Features.Authentication.ValueObjects;
using BasicProjectTemplate.SharedLibrary.AzureServiceBus.EmailQueue;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BasicProjectTemplate.Application.UseCases.Users.Commands;

public record ResetPasswordCommand(string EmailAddress) : IRequest;

public class ResetPasswordRequestHandler(
    ILogger<ResetPasswordRequestHandler> logger,
    AppDbContext dbContext)
    : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        // check if email is a valid email address

        var foundUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == new Email(command.EmailAddress),
                            cancellationToken: cancellationToken)
                        ?? throw new NotFoundException();

        try
        {
            var code = Guid.NewGuid();

            foundUser.PasswordResetValues.Add(new PasswordResetValues(command.EmailAddress, code));
            dbContext.Users.Update(foundUser);

            //TODO: send email for password reset
            var email = new EmailQueueMessageBody(
                command.EmailAddress,
                "Password Reset Request",
                $@"
                        <h1>Your Password Reset Request</h1>
                        <p>Please follow the link to reset your password</p>
                        <a href=""http://localhost:4200/lobby/resetPassword/{code}"">Click to Reset Your Password</a>
                        <p>If you did not send a request for a password reset please ignore this email.</p> 
                        "); //TODO: add front-end host address to configuration

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not start password reset request process for email: {email}",
                command.EmailAddress);
        }
    }
}