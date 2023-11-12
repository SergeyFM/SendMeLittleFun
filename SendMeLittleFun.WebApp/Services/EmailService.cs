namespace SendMeLittleFun.WebApp.Services;

public class EmailService : IEmailService {
    public EmailService() { }

    public void Send(string emailAddress, string emailSubject, string emailBody) {

        Console.WriteLine($"SEND {emailAddress} \n{emailAddress} \n{emailBody}");
    }

}
