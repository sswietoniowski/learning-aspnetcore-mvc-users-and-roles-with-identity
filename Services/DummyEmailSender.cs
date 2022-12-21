using Microsoft.AspNetCore.Identity.UI.Services;

namespace learning_aspnetcore_mvc_users_and_roles_with_identity.Services;

public class DummyEmailSender : IEmailSender
{
    private readonly ILogger<DummyEmailSender> _logger;

    public DummyEmailSender(ILogger<DummyEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogInformation($"Email sent to {email} with subject '{subject}': {htmlMessage}");
        return Task.CompletedTask;
    }
}