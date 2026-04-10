namespace AllJob.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendForgotPasswordAsync(string toEmail, string resetToken);
    Task SendWelcomeAsync(string toEmail, string fullName);
    Task SendAdminInviteAsync(string toEmail, string inviteToken, string role);
    Task SendApplicationReceivedAsync(string toEmail, string jobTitle);
    Task SendApplicationStatusChangedAsync(string toEmail, string jobTitle, string status);
}
