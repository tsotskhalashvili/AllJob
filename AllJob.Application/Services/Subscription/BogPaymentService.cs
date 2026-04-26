using AllJob.Application.DTOs.Payment;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Companies;
using AllJob.Application.Interfaces.Repositories.Subscriptions;
using AllJob.Application.Interfaces.Services.Subscription;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Subscriptions;
using AllJob.Domain.Enums.Subscriptions;

namespace AllJob.Application.Services.Subscription;

public class BogPaymentService(
    IPaymentRepository paymentRepository,
    ISubscriptionRepository subscriptionRepository,
    ICompanyRepository companyRepository,
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork) : IPaymentService
{
    public async Task<InitiatePaymentResponseDto> InitiatePaymentAsync(
        InitiatePaymentDto dto, Guid userId)
    {
        var company = await companyRepository
            .GetCompanyWithDetailsAsync(userId)
            ?? throw new NotFoundException("Company", userId);

        var plan = await planRepository.GetByIdAsync(dto.PlanId)
            ?? throw new NotFoundException("Plan", dto.PlanId);

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            CompanyId = company.Id,
            PlanId = plan.Id,
            Amount = plan.Price,
            Status = PaymentStatus.Pending
        };

        await paymentRepository.AddAsync(payment);
        await unitOfWork.SaveChangesAsync();

        return payment.ToPaymentDto();
    }

    public async Task HandleWebhookAsync(BogWebhookDto dto)
    {
        var payment = await paymentRepository
            .GetByIdWithDetailsAsync(dto.PaymentId)
            ?? throw new NotFoundException("Payment", dto.PaymentId);

        if (dto.Status != "Completed")
        {
            payment.Status = PaymentStatus.Failed;
            paymentRepository.Update(payment);
            await unitOfWork.SaveChangesAsync();
            return;
        }

        payment.Status = PaymentStatus.Completed;
        payment.TransactionId = dto.TransactionId;
        payment.PaidAt = DateTime.UtcNow;
        paymentRepository.Update(payment);

        var existingSubscription = await subscriptionRepository
            .GetByIdAsync(payment.CompanyId);

        if (existingSubscription is not null)
        {
            existingSubscription.IsActive = true;
            existingSubscription.PlanId = payment.PlanId;
            existingSubscription.StartDate = DateTime.UtcNow;
            existingSubscription.EndDate = DateTime.UtcNow.AddMonths(1);
            subscriptionRepository.Update(existingSubscription);
        }
        else
        {
            var subscription = new CompanySubscription
            {
                Id = Guid.NewGuid(),
                CompanyId = payment.CompanyId,
                PlanId = payment.PlanId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                IsActive = true
            };
            await subscriptionRepository.AddAsync(subscription);
        }

        payment.Company.Tier = payment.Plan.Name switch 
        {
            "Standard" => PlanTier.Standard,
            "VIP" => PlanTier.VIP,
            "SuperVIP" => PlanTier.SuperVIP,
            _ => PlanTier.Free
        };
        companyRepository.Update(payment.Company);

        await unitOfWork.SaveChangesAsync();
    }
}