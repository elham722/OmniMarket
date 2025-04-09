using OmniMarket.Web.ViewModels;

namespace OmniMarket.Web.Contracts
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email,string password);
        Task<bool> Register(RegisterViewModel register);
        Task Logout(); 
    }
}
