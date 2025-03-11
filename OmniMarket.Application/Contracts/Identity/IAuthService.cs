
using OmniMarket.Application.Models.Identity;

namespace OmniMarket.Application.Contracts.Identity
{
   public interface IAuthService
   {
       Task<AuthResponse> Login(AuthRequest request);
       Task<RegistrationResponse> Register(RegisterationRequest request);
   }
}
