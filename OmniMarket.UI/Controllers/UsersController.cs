using OmniMarket.Web.Contracts;
using OmniMarket.Web.Models;

namespace OmniMarket.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        #region Login

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticateService.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("", "Login Failed. Please Try again.");
            return View(login);
        }

        #endregion

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {

                return View(register);
            }

            var isCreated = await _authenticateService.Register(register);
            if (isCreated)
            {
                return LocalRedirect("/");
            }
            ModelState.AddModelError("", "Registration Failed.");
            return View(register);
        }
        #endregion


        #region Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return LocalRedirect("/Users/Login");
        }

        #endregion 
    }
}