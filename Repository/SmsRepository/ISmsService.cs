using WebApplication2.DTO;

namespace WebApplication2.Repository.SmsRepository
{
    public interface ISmsService
    {
        Task SendSmsAsync(SmsDto smsDto);
    }
}
