
using OmniMarket.Application.Exceptions;

namespace OmniMarket.Identity.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager) : IAuthService
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

            AuthResponse response = new AuthResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
            };

            return response;

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
