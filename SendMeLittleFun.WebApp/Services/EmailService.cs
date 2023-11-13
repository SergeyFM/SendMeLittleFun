
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class EmailService : IEmailService {
    public EmailService() {
    }

    public void Send(Email mailToSend) {
        Console.WriteLine($"SEND {mailToSend.EmailAddress} \n{mailToSend.Subject} \n{mailToSend.Body}");
    }

}
