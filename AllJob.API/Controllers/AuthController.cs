using AllJob.Application.DTOs.Auth;
using AllJob.Application.Interfaces.Services.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IValidator<RegisterDto> registerValidator,
    IValidator<LoginDto> loginValidator,
     IValidator<ForgotPasswordDto> forgotPasswordValidator,
    IValidator<ResetPasswordDto> resetPasswordValidator,
    IValidator<ChangePasswordDto> changePasswordValidator,
    IValidator<GoogleAuthDto> googleAuthValidator)

    : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await ValidateAsync(registerValidator, dto);
        var result = await authService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        await ValidateAsync(loginValidator, dto);
        var result = await authService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(
    [FromBody] GoogleAuthDto dto)
    {
        await ValidateAsync(googleAuthValidator, dto);
        var result = await authService.GoogleLoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
    {
        var result = await authService.RefreshTokenAsync(dto);
        return Ok(result);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken(RefreshTokenDto dto)
    {
        await authService.RevokeTokenAsync(dto);
        return NoContent();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(
        [FromBody] ForgotPasswordDto dto)
    {
        await ValidateAsync(forgotPasswordValidator, dto);
        await authService.ForgotPasswordAsync(dto);
        return NoContent();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordDto dto)
    {
        await ValidateAsync(resetPasswordValidator, dto);
        await authService.ResetPasswordAsync(dto);
        return NoContent();

    }


    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordDto dto)
    {
        await ValidateAsync(changePasswordValidator, dto);
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await authService.ChangePasswordAsync(dto, userId);
        return NoContent();

    }
}