using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AllJob.API.Services;

public class SendGridEmailService(
    IOptions<SendGridSettings> sendGridSettings,
    IOptions<AppSettings> appSettings) : IEmailService
{
    private readonly SendGridSettings _sendGrid = sendGridSettings.Value;
    private readonly AppSettings _app = appSettings.Value;

    private async Task SendAsync(string toEmail, string templateId, object templateData)
    {
        var client = new SendGridClient(_sendGrid.ApiKey);
        var msg = new SendGridMessage();
        msg.SetFrom(new EmailAddress(_sendGrid.FromEmail, _sendGrid.FromName));
        msg.AddTo(new EmailAddress(toEmail));
        msg.SetTemplateId(templateId);
        msg.SetTemplateData(templateData);

        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Body.ReadAsStringAsync();
            throw new Exception($"SendGrid error: {body}");
        }
    }

    public async Task SendForgotPasswordAsync(string toEmail, string resetToken)
        => await SendAsync(toEmail, _sendGrid.ForgotPasswordTemplateId, new
        {
            resetLink = $"{_app.BaseUrl}/reset-password?token={resetToken}"
        });

    public async Task SendWelcomeAsync(string toEmail, string fullName)
        => await SendAsync(toEmail, _sendGrid.WelcomeTemplateId, new { fullName });

    public async Task SendAdminInviteAsync(string toEmail, string inviteToken, string role)
        => await SendAsync(toEmail, _sendGrid.AdminInviteTemplateId, new
        {
            inviteLink = $"{_app.BaseUrl}/admin/invite/accept?token={inviteToken}",
            role
        });

    public async Task SendApplicationReceivedAsync(string toEmail, string jobTitle)
        => await SendAsync(toEmail, _sendGrid.ApplicationReceivedTemplateId, new { jobTitle });

    public async Task SendApplicationStatusChangedAsync(string toEmail, string jobTitle, string status)
        => await SendAsync(toEmail, _sendGrid.ApplicationStatusChangedTemplateId, new { jobTitle, status });

    public async Task SendOtpAsync(string toEmail, string otp)
      => await SendAsync(toEmail, _sendGrid.OtpTemplateId, new { otp });
}