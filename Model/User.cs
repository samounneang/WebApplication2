using AgriAuth.Enum;

namespace AgriAuth.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public Gender Gender { get; set; }

        public DateTime DateofBirth { get; set; }
        public string? Email { get; set; }

        public string? Imagecover { get; set; }
        public string? Phonenumber { get; set; }

        // External logins
        public string? GoogleId { get; set; }
        public string? TelegramId { get; set; }

        // Common Auth Fields
        public string? PasswordHash { get; set; } // Used for email/phone logins
        public LoginProvider LoginProvider { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




    }
}
