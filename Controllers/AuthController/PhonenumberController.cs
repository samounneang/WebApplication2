using WebApplication2.DTO;
using WebApplication2.Repository.AuthRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonenumberController : ControllerBase
    {
        private readonly IAuthService _auth;

        public PhonenumberController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register-phonenumber")]

        public async Task<IActionResult> RegisterWithPhone([FromBody] string phonenumber)
        {
            try
            {    
                await _auth.RegisterWithPhonenumberAsync(phonenumber);
                await _auth.SendOtpPhoneAsync(phonenumber);
                return Ok("Otp has sent to phonenumber");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify-phone")]

        public async Task<IActionResult> VerifyPhone([FromBody] VerifyPhoneDto verifyPhoneDto)
        {
            try
            {
                await _auth.VerifyPhoneOtpAsync(verifyPhoneDto);

                return Ok("Verified Successfully");


            }
            catch (Exception ex) { 
            
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("complete-register")]

        public async Task<IActionResult> CompleteRegister([FromBody] CompleteRegisterDto verifyRegisterDto)
        {
            try
            {
                await _auth.CompleteRegisterPhoneAsync(verifyRegisterDto);

                return Ok("Register Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
