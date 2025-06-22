using AgriAuth.DTO;

namespace AgriAuth.Repository.AuthRepository
{
    public interface IAuthService
    {
        Task RegisterEmailAsync(RegisterWithEmailDto userDto);
        Task SendOtpAsync(string email);

        Task VerifyOtpAsync(VerifyDto verifyDto);

        Task CompleteRegister(CompleteRegisterDto registerDto);

        Task RegisterWithPhonenumberAsync(string phonenumber);

        Task SendOtpPhoneAsync(string phonenumber);
        Task VerifyPhoneOtpAsync(VerifyPhoneDto verifyPhoneDto);

        Task CompleteRegisterPhoneAsync(CompleteRegisterDto completeRegisterDto);

        Task ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto);

        Task ResetPasswordAsync (ResetPasswordDto resetPasswordDto);
        


    }
}
