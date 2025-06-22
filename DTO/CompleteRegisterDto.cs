using WebApplication2.Enum;

namespace WebApplication2.DTO
{
    public class CompleteRegisterDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string? Phonenumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateofBirth { get; set; }


    }
}
