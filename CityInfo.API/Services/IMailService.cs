namespace CityInfo.API.Services
{
    public interface IMailService
    {
        public void send(string subject, string massage);
    }
}
