namespace WebApplication2.DTO
{
    public class ResetPasswordDto
    {
        public string? Identifier { get; set; } 
        public string? OtpCode { get; set; } 
        public string? NewPassword { get; set; } 
        public string? ConfirmPassword { get; set; } 
    }
}
