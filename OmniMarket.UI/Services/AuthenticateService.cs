
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OmniMarket.UI.Contracts;
using OmniMarket.UI.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OmniMarket.UI.Models;

namespace OmniMarket.UI.Services
{
    public class AuthenticateService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor) 
        : BaseHttpService(client, localStorage), IAuthenticateService
    {
        private IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticateRequest = new()
                {
                    Email = email,
                    Password = password
                };
                var authenticateResponse = await client.LoginAsync(authenticateRequest);
                if (authenticateResponse.Token != string.Empty)
                {
                    var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authenticateResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, user);

                    localStorage.SetStorageValue("token", authenticateResponse.Token);

                    return true;
                }

                return false;


            }
            catch
            {

                return false;
            }
        }

        public async Task Logout()
        {
            localStorage.ClearStorage(new List<string>() { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterViewModel register)
        {
            RegisterationRequest registrationRequest = new()
            {
                Email = register.Email,
                Password = register.Password,
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Username,
                
            };
            var response = await client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }

            return false;

        }

        private IList<Claim> ParseClaims(JwtSecurityToken token)
        {
            var claims = token.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, token.Subject));
            return claims;
        }
    }
}
