namespace AllJob.Application.Constants;

public static class NotificationMessages
{
    // Application
    public const string ApplicationReceivedTitle = "New Application";
    public const string ApplicationReceivedMessage = "A candidate applied for your job";

    public const string ApplicationStatusChangedTitle = "Application Status Updated";
    public const string ApplicationStatusChangedMessage = "Your application status has been updated";

    // Company
    public const string CompanyVerifiedTitle = "Company Verified";
    public const string CompanyVerifiedMessage = "Your company has been verified";

    public const string CompanyRejectedTitle = "Company Rejected";
    public const string CompanyRejectedMessage = "Your company has been rejected";

    public const string NewCompanyPendingTitle = "New Company Pending";
    public const string NewCompanyPendingMessage = "A new company requires verification";

    // Job
    public const string JobExpiredTitle = "Job Expired";
    public const string JobExpiredMessage = "Your job listing has expired";
}
