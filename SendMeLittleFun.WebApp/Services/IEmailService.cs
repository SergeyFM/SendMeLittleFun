using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;
public interface IEmailService {
    void Send(Email mailToSend);
}