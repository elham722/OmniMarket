﻿
using Microsoft.EntityFrameworkCore;
using OmniMarket.Application.Exceptions;

namespace OmniMarket.Identity.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        OmniMarketIdentityDbContext context) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
      
        #region Register
        public async Task<RegistrationResponse> Register(RegisterationRequest request)
        {
            var existingUser = await userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.UserName);
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };


            var existingEmail = await userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null)
            {
                var result = await userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Client");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new RegistrationFailedException(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                throw new EmailAlreadyExistsException(request.Email);
            }
        }
        #endregion

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException(request.Email);
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new InvalidCredentialsException(request.Email);
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedDate = DateTime.UtcNow,
                IsUsed = false,
                IsRevoked = false
            };

            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();


            AuthResponse response = new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                RefreshToken = refreshToken.Token
            };

            return response;

        }

        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            // پیدا کردن RefreshToken در دیتابیس
            var token = await context.RefreshTokens
                .Include(rt => rt.User) // کاربران رو هم لود می‌کنیم
                .FirstOrDefaultAsync(t => t.Token == refreshToken);

            // چک کردن اعتبار توکن
            if (token == null || token.IsUsed || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            // مارک کردن RefreshToken به عنوان استفاده‌شده
            token.IsUsed = true;
            context.RefreshTokens.Update(token);
            await context.SaveChangesAsync();

            // تولید Access Token جدید
            var newAccessToken = await GenerateToken(token.User);

            // برگرداندن پاسخ با Access Token جدید و یک Refresh Token جدید
            return new AuthResponse
            {
                Id = token.User.Id,
                Email = token.User.Email,
                UserName = token.User.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = Guid.NewGuid().ToString() // تولید Refresh Token جدید
            };
        }


        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(CustomClaimTypes.Uid, user.Id)
                }
                .Union(userClaims ?? Enumerable.Empty<Claim>())
                .Union(roleClaims ?? Enumerable.Empty<Claim>());

            if (string.IsNullOrEmpty(_jwtSettings.Key))
                throw new InvalidOperationException("JWT Key cannot be null or empty.");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
