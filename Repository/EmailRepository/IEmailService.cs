using AgriAuth.DTO;

namespace AgriAuth.Repository.EmailRepository
{
    public interface IEmailService
    {
        Task SendEmailAsync (EmailDto dto);
    }
}
