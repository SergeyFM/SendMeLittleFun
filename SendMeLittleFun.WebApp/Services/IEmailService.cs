namespace SendMeLittleFun.WebApp.Services;

public interface IEmailService {
    void Send(string emailAddress, string emailSubject, string emailBody);
}