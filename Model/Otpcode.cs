namespace AgriAuth.Model
{
    public class Otpcode
    {
        
            public int Id { get; set; }
            public string? Email { get; set; }

            public string? Phonenumber { get; set; }
            public string Code { get; set; } = null!;
            public DateTime ExpiryTime { get; set; }
            public bool IsUsed { get; set; } = false;

            public bool IsVerified { get; set; } = false;
        

    }
}
