namespace AllJob.Application.Interfaces.Services.Auth;

public interface ITwoFactorService
{
    Task SendOtpAsync(Guid userId, string email);
    Task<bool> VerifyOtpAsync(Guid userId, string otp);
}