namespace AllJob.Application.Settings;

public class SendGridSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string ForgotPasswordTemplateId { get; set; } = string.Empty;
    public string WelcomeTemplateId { get; set; } = string.Empty;
    public string AdminInviteTemplateId { get; set; } = string.Empty;
    public string ApplicationReceivedTemplateId { get; set; } = string.Empty;
    public string ApplicationStatusChangedTemplateId { get; set; } = string.Empty;
}