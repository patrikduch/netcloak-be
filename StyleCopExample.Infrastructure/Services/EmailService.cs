using NetCloak.Application.Interfaces.Infrastructure;

namespace NetCloak.Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendEmailTest(string email)
    {
        // TODO Send email
        return Task.CompletedTask;
    }
}
