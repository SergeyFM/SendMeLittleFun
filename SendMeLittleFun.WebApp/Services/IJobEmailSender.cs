using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;
public interface IJobEmailSender {
    void Send(User user);
}