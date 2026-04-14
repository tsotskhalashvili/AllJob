using AllJob.Application.DTOs.Auth;
using AllJob.Application.Exceptions;
using AllJob.Application.Helpers;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Auth;
using AllJob.Application.Interfaces.Services.Auth;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Application.Settings;
using AllJob.Domain.Entities.Auth;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AllJob.Application.Services.Auth
{
    public class AuthService(
            IUserRepository userRepository,
            IGenericRepository<Role> roleRepository,
            IGenericRepository<UserRole> userRoleRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IOptions<JwtSettings> jwtSettings,
            IOptions<GoogleSettings> googleSettings,
            IOptions<TokenHashSettings> tokenHashSettings
        ) : IAuthService
    {
        private readonly JwtSettings _jwt = jwtSettings.Value;
        private readonly GoogleSettings _google = googleSettings.Value;
        private readonly string _tokenHashSecret = tokenHashSettings.Value.Secret;

        #region PublicMethods
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await userRepository.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new ConflictException($"Email '{dto.Email}' is already registered");

            var roles = await roleRepository.GetAllAsync();
            var role = roles.FirstOrDefault(r => r.Name == dto.Role)
                ?? throw new NotFoundException("Role", dto.Role);

            var user = dto.ToEntity();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await unitOfWork.BeginTransactionAsync();
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.SaveChangesAsync();

                await userRoleRepository.AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                });
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitAsync();
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }

            return await GenerateAuthResponseAsync(user, dto.Role);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await userRepository.GetByEmailWithRolesAsync(dto.Email)
                ?? throw new UnauthorizedException("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password");

            if (!user.IsActive)
                throw new ForbiddenException("Account is deactivated");

            var role = user.UserRoles.FirstOrDefault()?.Role
                ?? throw new UnauthorizedException("User has no assigned role");

            return await GenerateAuthResponseAsync(user, role.Name);
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleAuthDto dto)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                dto.IdToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _google.ClientId }
                });

            var user = await userRepository.GetByEmailAsync(payload.Email);
            if (user is null)
            {
                var roles = await roleRepository.GetAllAsync();
                var role = roles.FirstOrDefault(r => r.Name == dto.Role)
                    ?? throw new NotFoundException("Role", dto.Role);

                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    PasswordHash = string.Empty,
                    IsActive = true,
                    IsExternalLogin = true,
                    IsPasswordChangeRequired = false
                };

                await unitOfWork.BeginTransactionAsync();
                try
                {
                    await userRepository.AddAsync(user);
                    await unitOfWork.SaveChangesAsync();

                    await userRoleRepository.AddAsync(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                    await unitOfWork.SaveChangesAsync();
                    await unitOfWork.CommitAsync();
                }
                catch
                {
                    await unitOfWork.RollbackAsync();
                    throw;
                }

                return await GenerateAuthResponseAsync(user, role.Name);
            }

            if (!user.IsActive)
                throw new ForbiddenException("Account is deactivated");

            var existingRole = await userRepository.GetByIdWithRolesAsync(user.Id);

            var roleName = existingRole!.UserRoles
                .FirstOrDefault()?.Role.Name
                ?? throw new UnauthorizedException("User has no assigned role");

            return await GenerateAuthResponseAsync(user, roleName);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var refreshToken = await refreshTokenRepository
                .GetByTokenAsync(dto.RefreshToken)
                ?? throw new UnauthorizedException("Invalid refresh token");

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedException("Refresh token expired");

            if (refreshToken.RevokedAt is not null)
                throw new UnauthorizedException("Refresh token revoked");

            var user = await userRepository
                .GetByIdWithRolesAsync(refreshToken.UserId)
                ?? throw new NotFoundException("User", refreshToken.UserId);

            var role = user.UserRoles.FirstOrDefault()?.Role
                ?? throw new UnauthorizedException("User has no assigned role");

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshTokenRepository.Update(refreshToken);

            return await GenerateAuthResponseAsync(user, role.Name);
        }

        public async Task RevokeTokenAsync(RefreshTokenDto dto)
        {
            var refreshToken = await refreshTokenRepository
                .GetByTokenAsync(dto.RefreshToken)
                ?? throw new UnauthorizedException("Invalid refresh token");

            if (refreshToken.RevokedAt is not null)
                throw new ConflictException("Token is already revoked");

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshTokenRepository.Update(refreshToken);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await userRepository.GetByEmailAsync(dto.Email);

            if (user is null) return;

            var existingToken = await passwordResetTokenRepository
                .GetActiveTokenByUserIdAsync(user.Id);

            if (existingToken is not null)
            {
                existingToken.IsUsed = true;
                passwordResetTokenRepository.Update(existingToken);
            }

            var rawToken = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var resetToken = new PasswordResetToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = TokenHasher.Hash(rawToken, _tokenHashSecret),
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await passwordResetTokenRepository.AddAsync(resetToken);
            await unitOfWork.SaveChangesAsync();

            await emailService.SendForgotPasswordAsync(user.Email, rawToken);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var resetToken = await passwordResetTokenRepository
                .GetByTokenAsync(dto.Token)
                ?? throw new NotFoundException("Token", dto.Token);

            if (resetToken.IsUsed)
                throw new ConflictException("Token is already used");

            if (resetToken.ExpiresAt < DateTime.UtcNow)
                throw new ConflictException("Token has expired");

            var user = resetToken.User
                ?? throw new NotFoundException("User", resetToken.UserId);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            userRepository.Update(user);
            resetToken.IsUsed = true;
            passwordResetTokenRepository.Update(resetToken);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(ChangePasswordDto dto, Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId)
               ?? throw new NotFoundException("User", userId);

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new UnauthorizedException("Current password is incorrect");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            userRepository.Update(user);
            await unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region PrivateMethods
        private string GenerateAccessToken(User user, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.SecretKey));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwt.AccessTokenExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private async Task<AuthResponseDto> GenerateAuthResponseAsync(
            User user, string role)
        {
            var accessToken = GenerateAccessToken(user, role);
            var rawRefreshToken = GenerateRefreshToken();

            await refreshTokenRepository.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = TokenHasher.Hash(rawRefreshToken, _tokenHashSecret),
                ExpiresAt = DateTime.UtcNow.AddDays(
                    _jwt.RefreshTokenExpirationDays)
            });

            await unitOfWork.SaveChangesAsync();

            return new AuthResponseDto(
                AccessToken: accessToken,
                RefreshToken: rawRefreshToken,
                ExpiresAt: DateTime.UtcNow.AddMinutes(
                    _jwt.AccessTokenExpirationMinutes));
        }
        #endregion
    }
}