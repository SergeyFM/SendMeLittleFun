using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;
public interface IRandomFunEmailGenerator {
    Email ComposeEmail(string emailAddress, string userName);
}