using WebApplication2.DTO;

namespace WebApplication2.Repository.EmailRepository
{
    public interface IEmailService
    {
        Task SendEmailAsync (EmailDto dto);
    }
}
