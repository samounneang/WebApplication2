using WebApplication2.DTO;
using WebApplication2.Repository.AuthRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailContoller : ControllerBase
    {
        private readonly IAuthService _auth;

        public EmailContoller(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterWithEmailDto dto)
        {
            try
            {

                await _auth.RegisterEmailAsync(dto);
                await _auth.SendOtpAsync(dto.Email); // assume email is required

                return Ok(" OTP sent to email.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyDto verifyDto)
        {
            try
            {
                await _auth.VerifyOtpAsync(verifyDto);
                return Ok(new { message = "Otp verified successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("complete-register")]

        public async Task<IActionResult> CompleterRegister(CompleteRegisterDto registerDto)
        {

            try
            {

                await _auth.CompleteRegister(registerDto);
                return Ok(new { message = "Register sucessfully." });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }
    }
}
