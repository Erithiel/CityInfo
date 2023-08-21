using System.Runtime.InteropServices;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private string sendTo ;
        private string sendFrom ;

        public LocalMailService(IConfiguration configuration)
        {
            sendTo = configuration["MailSettings:mailToAdress"];
            sendFrom = configuration["MailSettings:mailFromAdress"];    
        }
        public void send(string subject, string massage)
        {
            Console.WriteLine($"Mail from {sendFrom} to {sendTo}" + $" with {nameof(LocalMailService)} ");
            Console.WriteLine($"subject :{subject}");
            Console.WriteLine($"massage :{massage}");

        }

    }
}
