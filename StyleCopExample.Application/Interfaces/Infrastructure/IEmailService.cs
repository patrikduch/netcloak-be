namespace NetCloak.Application.Interfaces.Infrastructure;

public interface IEmailService
{
    Task SendEmailTest(string email);
}
