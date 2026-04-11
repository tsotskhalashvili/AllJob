using AllJob.Application.DTOs.Auth;

namespace AllJob.Application.Interfaces.Services.Auth;

public interface IAuthService
{

    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task RevokeTokenAsync(RefreshTokenDto dto);
    Task ForgotPasswordAsync(ForgotPasswordDto dto);
    Task ResetPasswordAsync(ResetPasswordDto dto);
    Task ChangePasswordAsync(ChangePasswordDto dto, Guid userId);
    Task<AuthResponseDto> GoogleLoginAsync(GoogleAuthDto dto);
}
