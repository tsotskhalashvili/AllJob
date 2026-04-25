using AllJob.Application.Exceptions;
using AllJob.Application.Helpers;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Services.Auth;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace AllJob.Application.Services.Auth;

public class TwoFactorService(
    IUserRepository userRepository,
    IEmailService emailService,
    IUnitOfWork unitOfWork,
    IOptions<TokenHashSettings> tokenHashSettings) : ITwoFactorService
{
    private readonly string _secret = tokenHashSettings.Value.Secret;

    public async Task SendOtpAsync(Guid userId, string email)
    {
        var otp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString(); 
        var otpHash = TokenHasher.Hash(otp, _secret);

      

        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        user.PendingOtpHash = otpHash;
        user.OtpExpiresAt = DateTime.UtcNow.AddMinutes(5);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();

        await emailService.SendOtpAsync(email, otp);
    }

    public async Task<bool> VerifyOtpAsync(Guid userId, string otp)
    {
        var user = await userRepository.GetByIdAsync(userId)
            ?? throw new NotFoundException("User", userId);

        if (user.PendingOtpHash is null || user.OtpExpiresAt is null)
            return false;

        if (user.OtpExpiresAt < DateTime.UtcNow)
            return false;

        var otpHash = TokenHasher.Hash(otp, _secret);

        var pendingBytes = Encoding.UTF8.GetBytes(user.PendingOtpHash);
        var otpBytes = Encoding.UTF8.GetBytes(otpHash);

        if (!CryptographicOperations.FixedTimeEquals(pendingBytes, otpBytes)) 
            return false;

        user.PendingOtpHash = null;
        user.OtpExpiresAt = null;
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}