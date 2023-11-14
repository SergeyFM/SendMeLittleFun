using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JokesUpdateService : IHostedService {
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;

    public JokesUpdateService(IServiceProvider serviceProvider, IConfiguration config) {
        _serviceProvider = serviceProvider;
        _config = config;
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        using(var scope = _serviceProvider.CreateScope()) {
            var _appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            LoadNewJokesToDB(_appDbContext);
        }
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void LoadNewJokesToDB(ApplicationDbContext _appDbContext) {
        Console.WriteLine("Find and load new jokes to DB...");
        List<Joke> allJokes = _appDbContext.Jokes.ToList();
        Console.WriteLine($"Right now we have {allJokes.Count()} jokes in the DB");

        Console.WriteLine("Adding new jokes...");
        List<Joke> jokesFromTheFile = GetJokesFromFile();
        int updateCounter = 0;
        foreach (Joke joke in jokesFromTheFile) {
            bool jokeAlreadyExists = allJokes.Any(j => j.JokeText == joke.JokeText);
            if (jokeAlreadyExists == false) { 
                _appDbContext.Jokes.Add(joke);
                updateCounter++;
            }
        }
        _appDbContext.SaveChanges();
        Console.WriteLine($"Added {updateCounter} new jokes");

    }

    private List<Joke> GetJokesFromFile() {
        
        // Get file name and check existance
        string theFile = _config.GetValue<string>("JokesFile") ?? "";
        bool theFileExists = File.Exists(theFile);
        Console.WriteLine($"Reading {theFile}... (exists: {theFileExists})");
        if (!theFileExists) return new();

        // Read all jokes from the file
        List<Joke> allJokes = new();
        IEnumerable<string> lines = File.ReadLines(theFile);
        string currentJoke = "";
        foreach (string line in lines) {
            if (line.Contains("* *")) {
                if (!string.IsNullOrWhiteSpace(currentJoke)) allJokes.Add(new Joke(currentJoke));
                currentJoke = "";
                continue;
            }
            if (!string.IsNullOrWhiteSpace(line)) currentJoke += line + "\r\n";
        }

        return allJokes;
    }

}
