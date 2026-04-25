using AllJob.Application.Constants;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Repositories.Jobs;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Application.Interfaces.Services.Hangfire;
using AllJob.Application.Interfaces.Services.Notification;
using AllJob.Domain.Enums.Jobs;
using AllJob.Domain.Enums.Notifications;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Application.Services.Hangfire;

public class HangfireJobService(
    IJobRepository jobRepository,
    ISubscriptionRepository subscriptionRepository,
    ICandidateRepository candidateRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordResetTokenRepository passwordResetTokenRepository,
    IAdminInviteRepository adminInviteRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork) : IHangfireJobService
{
    public async Task CleanupExpiredTokensAsync()
    {
        await refreshTokenRepository.DeleteExpiredTokensAsync();
        await passwordResetTokenRepository.DeleteExpiredTokensAsync();
        await adminInviteRepository.DeleteExpiredInvitesAsync();
    }

    public async Task ExpireJobsAsync()
    {
        var jobs = await jobRepository.GetExpiredJobsAsync();

        foreach (var job in jobs)
        {
            job.Status = JobStatus.Expired;
            jobRepository.Update(job);
            await unitOfWork.SaveChangesAsync();

            await notificationService.CreateAsync(
                userId: job.Company.UserId,
                title: NotificationMessages.JobExpiredTitle,
                message: NotificationMessages.JobExpiredMessage,
                type: NotificationType.JobExpired,
                actionUrl: $"/employer/jobs/{job.Id}"
            );
        }
    }

    public async Task ExpireSubscriptionsAsync()
    {
        var subscriptions = await subscriptionRepository
            .GetExpiredSubscriptionsAsync();

        foreach (var subscription in subscriptions)
        {
            subscription.IsActive = false;
            subscription.Company.Tier = PlanTier.Free;
            subscriptionRepository.Update(subscription);
            await unitOfWork.SaveChangesAsync();

            await notificationService.CreateAsync(
                userId: subscription.Company.UserId,
                title: NotificationMessages.PlanExpiredTitle,
                message: NotificationMessages.PlanExpiredMessage,
                type: NotificationType.PlanExpired,
                actionUrl: "/employer/subscription"
            );
        }
    }

    public async Task NotifyNewJobsAsync()
    {
        
        var recentJobs = await jobRepository.GetRecentJobsAsync(6);

        if (!recentJobs.Any()) return;

       
        var unnotifiedJobs = recentJobs
            .Where(j => j.LastNotifiedAt == null)
            .ToList();

        if (!unnotifiedJobs.Any()) return;

        var candidates = await candidateRepository.GetOpenToWorkAsync();

        foreach (var candidate in candidates)
        {
            var candidateSkillIds = candidate.Skills
                .Select(cs => cs.SkillId)
                .ToHashSet();

            foreach (var job in unnotifiedJobs)
            {
                var jobSkillIds = job.JobSkills
                    .Select(js => js.SkillId)
                    .ToHashSet();

                var hasMatch = candidateSkillIds
                    .Intersect(jobSkillIds)
                    .Any();

                if (!hasMatch) continue;

                await notificationService.CreateAsync(
                    userId: candidate.UserId,
                    title: NotificationMessages.NewJobMatchTitle,
                    message: NotificationMessages.NewJobMatchMessage,
                    type: NotificationType.NewJobMatch,
                    actionUrl: $"/jobs/{job.Id}"
                );
            }
        }

        
        foreach (var job in unnotifiedJobs)
        {
            job.LastNotifiedAt = DateTime.UtcNow;
            jobRepository.Update(job);
        }

        await unitOfWork.SaveChangesAsync();
    }
}