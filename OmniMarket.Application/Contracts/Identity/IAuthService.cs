using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniMarket.Application.Models.Identity;

namespace OmniMarket.Application.Contracts.Identity
{
   public interface IAuthService
   {
       Task<AuthResponse> Login(AuthRequest request);
       Task<RegistrationResponse> Register(RegisterationRequest request);
   }
}
