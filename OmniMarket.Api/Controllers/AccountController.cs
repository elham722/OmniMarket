
using Microsoft.IdentityModel.Tokens;
using OmniMarket.Application.Exceptions;

namespace OmniMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            try
            {
                return Ok(await authService.Login(request));
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred during login.");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegisterationRequest request)
        {
            try
            {
                return Ok(await authService.Register(request));
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RegistrationFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred during registration.");
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            try
            {
                var response = await authService.RefreshToken(model.RefreshToken);
                return Ok(response);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message); // مثلا خطای غیرمعتبر بودن توکن
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred during token refresh.");
            }
        }




    }
}
