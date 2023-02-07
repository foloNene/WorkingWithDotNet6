namespace CityInfo.APi.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}