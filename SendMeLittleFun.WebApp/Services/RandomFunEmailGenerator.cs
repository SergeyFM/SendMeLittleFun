using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class RandomFunEmailGenerator : IRandomFunEmailGenerator {

    public Email ComposeEmail(string emailAddress, string userName) => new Email() {
        EmailAddress = emailAddress,
        Subject = $"Привет, {userName}! Рандомный литтлфан для тебя!",
        Body = GetRandomStory()
    };


    private string GetRandomStory() {
        return "Какой-то анекдот";
    }
}
