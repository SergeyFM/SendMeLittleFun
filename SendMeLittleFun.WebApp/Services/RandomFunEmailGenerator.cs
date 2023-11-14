using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class RandomFunEmailGenerator : IRandomFunEmailGenerator {
    private ApplicationDbContext _context;

    public RandomFunEmailGenerator(ApplicationDbContext applicationDbContext) {
        _context = applicationDbContext;
    }

    public Email ComposeEmail(string emailAddress, string userName) {

        // Get a random joke
        IQueryable<Joke> allJokes = _context.Jokes;

        int numberOfJokesInDB = allJokes?.Count() ?? 0;
        int randomIndex = new Random().Next(numberOfJokesInDB);
        Console.WriteLine($"ComposeEmail >> numberOfJokesInDB: {numberOfJokesInDB}, randomIndex: {randomIndex}");
        Joke theJoke = allJokes is not null && allJokes.Count() > 0
            ? allJokes.Skip(randomIndex).First()
            : new("Желаю тебе отличного дня, и это не шутка!");

        // Return an email to send

        return new Email() {
            EmailAddress = emailAddress,
            Subject = $"Привет, {userName}! Рандомный литтлфан для тебя!",
            Body = theJoke.JokeText
        };
    }



}
