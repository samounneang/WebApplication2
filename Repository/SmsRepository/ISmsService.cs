using AgriAuth.DTO;

namespace AgriAuth.Repository.SmsRepository
{
    public interface ISmsService
    {
        Task SendSmsAsync(SmsDto smsDto);
    }
}
