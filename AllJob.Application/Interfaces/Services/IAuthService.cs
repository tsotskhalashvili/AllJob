using AllJob.Application.DTOs.Auth;

namespace AllJob.Application.Interfaces.Services;

public interface IAuthService
{

    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    Task RevokeTokenAsync(RefreshTokenDto dto);
}
