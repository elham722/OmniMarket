using Microsoft.AspNetCore.Authorization;

namespace OmniMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] 
    public class AdminController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            return Ok("خوش آمدید به داشبورد ادمین!");
        }
    }
}