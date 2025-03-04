using OmniMarket.UI.Models;

namespace OmniMarket.UI.Contracts
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email,string password);
        Task<bool> Register(RegisterViewModel register);
        Task Logout(); 
    }
}
