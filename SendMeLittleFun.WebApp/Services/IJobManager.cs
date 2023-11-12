using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;
public interface IJobManager {
    void AddEmailJob(Email email, string cronExpr = "-");
    int DeleteEmailJob(string email);
}