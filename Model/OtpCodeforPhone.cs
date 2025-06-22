namespace WebApplication2.Model
{
    public class OtpCodeforPhone
    {
        public int Id { get; set; }

        public string? Phonenumber { get; set; }
        public string? Code { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; } = false;

        public bool IsVerified { get; set; } = false;


    }
}
