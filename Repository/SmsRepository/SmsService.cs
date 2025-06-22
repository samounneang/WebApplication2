using WebApplication2.Repository.SmsRepository;
using System.Text.Json;
using System.Text;
using WebApplication2.DTO;

public class SmsService : ISmsService
{
    private readonly HttpClient _httpClient;
    private const string PlasgateUrl = "https://cloudapi.plasgate.com/rest/send";

    private readonly string _privateKey = "CP78LQQDVmlOQh3xCcizDQg7yqlkAZ6ySXF6iKX3UlA1ENCQZhGsMOrahbmcPAek8HtJk59Ftu3DqYhX87OvNQ";
    private readonly string _secret = "$5$rounds=535000$A/mqmdJbbNvx4QdE$o7kviE1UvDwyNfS.mcvcJa.FEVU0p6wWbmhoCxoyyq8";

    public SmsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendSmsAsync(SmsDto smsDto)
    {
        var requestPayload = new
        {
            sender = "OaktreeUAT", // Use a custom sender name
            to = smsDto.to,
            content = smsDto.content
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"{PlasgateUrl}?private_key={_privateKey}");
        request.Headers.Add("X-Secret", _secret);
        request.Content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to send OTP SMS");
        }
    }
}
