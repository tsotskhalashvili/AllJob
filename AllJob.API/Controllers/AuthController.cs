using AllJob.Application.DTOs.Auth;
using AllJob.Application.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

public class AuthController(
    IAuthService authService,
    IValidator<RegisterDto> registerValidator,
    IValidator<LoginDto> loginValidator)
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
}