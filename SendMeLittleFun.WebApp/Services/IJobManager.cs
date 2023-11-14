using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;
public interface IJobManager {
    void AddEmailJob(Email email, User user);
    int DeleteEmailJob(string emailAddress);
}