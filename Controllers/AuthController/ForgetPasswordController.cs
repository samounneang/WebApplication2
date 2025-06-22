using WebApplication2.DTO;
using WebApplication2.Repository.AuthRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly IAuthService _auth;

        public ForgetPasswordController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("forget-password")]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            try
            {
                await _auth.ForgotPasswordAsync(forgetPasswordDto);

                return Ok("Otp has sent to you ");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {

            try
            {
                await _auth.ResetPasswordAsync(resetPasswordDto);

                return Ok("reset password successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
