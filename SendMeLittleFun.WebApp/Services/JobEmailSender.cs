
using System.Net;
using System.Net.Mail;
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JobEmailSender : IJobEmailSender {
    private IRandomFunEmailGenerator _randomFunEmailGenerator;
    private readonly IConfiguration _config;

    public JobEmailSender(IRandomFunEmailGenerator randomFunEmailGenerator, IConfiguration config) {
        _randomFunEmailGenerator = randomFunEmailGenerator;
        _config = config;
    }

    public void Send(User user) {

        // Generate an email
        Email mailToSend = _randomFunEmailGenerator.ComposeEmail(user.Email, user.Name);
        
        // Send it!!!

        string emailHost = _config["EmailSettings:Host"] ?? "";
        int emailSmtpPort = _config.GetValue<int>("EmailSettings:SMTP_port");
        string emailLogin = _config["EmailSettings:Login"] ?? "";
        string emailPassword = _config["EmailSettings:Password"] ?? "";
        string emailFrom = _config["EmailSettings:FromAddress"] ?? "";

        Console.WriteLine($"SEND {emailHost}:{emailSmtpPort}, {emailLogin}, {emailPassword} \n {mailToSend.EmailAddress} \n{mailToSend.Subject} \n{mailToSend.Body}");

        SmtpClient smtpClient = new(emailHost) {
            Port = emailSmtpPort,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailLogin, emailPassword),
            EnableSsl = false,
            Timeout = 20000,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        MailMessage mailMessage = new() {
            From = new MailAddress(emailFrom), 
            Subject = mailToSend.Subject, 
            Body = mailToSend.Body,
            IsBodyHtml = false
        };
        mailMessage.To.Add(new MailAddress(mailToSend.EmailAddress));

        try {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Message is sent.");
        } catch (Exception ex) {
            Console.WriteLine($"ERROR sending email: {ex.Message}");
        }
    }

}
