using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OmniMarket.Application.Contracts.Identity;
using OmniMarket.Application.Models.Identity;
using OmniMarket.Identity.Models;

namespace OmniMarket.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _signInManager = signInManager;
        }
        public Task<AuthResponse> Login(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationResponse> Register(RegisterationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"user name '{request.UserName}' already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };


            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user,request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return new RegistrationResponse() { UserId = user.Id };
                }
                else
                {
                    throw new Exception($"{result.Errors}");
                }
            }
            else
            {
                throw new Exception($"Email '{request.Email}' already exists.");
            }
        }
    }
}
