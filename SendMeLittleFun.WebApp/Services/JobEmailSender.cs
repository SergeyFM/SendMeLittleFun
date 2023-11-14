
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JobEmailSender : IJobEmailSender {
    private IRandomFunEmailGenerator _randomFunEmailGenerator;
    public JobEmailSender(IRandomFunEmailGenerator randomFunEmailGenerator) {
        _randomFunEmailGenerator = randomFunEmailGenerator;
    }

    public void Send(User user) {

        Email mailToSend = _randomFunEmailGenerator.ComposeEmail(user.Email, user.Name);

        Console.WriteLine($"SEND {mailToSend.EmailAddress} \n{mailToSend.Subject} \n{mailToSend.Body}");
    }

}
