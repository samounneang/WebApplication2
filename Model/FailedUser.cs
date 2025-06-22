namespace WebApplication2.Model
{
    public class FailedUser
    {
       
            public int Id { get; set; }
            public string? Email { get; set; }

            public string? Phonenumber { get; set; }
            public bool IsOtpVerified { get; set; }
            public DateTime? OtpVerifiedAt { get; set; }
        

    }
}
