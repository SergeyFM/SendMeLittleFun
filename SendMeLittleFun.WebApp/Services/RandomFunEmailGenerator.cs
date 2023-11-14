using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class RandomFunEmailGenerator : IRandomFunEmailGenerator {
    private ApplicationDbContext _context;

    public RandomFunEmailGenerator(ApplicationDbContext applicationDbContext) {
        _context = applicationDbContext;
    }

    public Email ComposeEmail(string emailAddress, string userName) {

        // Get a random joke
        List<Joke>? allJokes = _context.Jokes.ToList();

        int numberOfJokesInDB = allJokes?.Count() ?? 0;
        int randomIndex = new Random().Next(numberOfJokesInDB);
        Console.WriteLine($"ComposeEmail >> numberOfJokesInDB: {numberOfJokesInDB}, randomIndex: {randomIndex}");
        Joke theJoke = allJokes is not null && allJokes.Count() > 0
            ? allJokes.ElementAt(randomIndex)
            : new("Желаю тебе отличного дня, и это не шутка!");

        // Return an email to send

        return new Email() {
            EmailAddress = emailAddress,
            Subject = $"Привет, {userName}! Рандомный литтлфан для тебя!",
            Body = theJoke.JokeText
        };
    }



}
