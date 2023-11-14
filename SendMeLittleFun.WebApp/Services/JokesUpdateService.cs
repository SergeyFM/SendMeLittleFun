using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JokesUpdateService : IHostedService {
    private readonly IServiceProvider _serviceProvider;

    public JokesUpdateService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
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
        Console.WriteLine("Right now we have following jokes:");
        foreach (var joke in allJokes) Console.WriteLine("> " + joke.JokeText);
        Console.WriteLine("Adding new joke...");
        Joke newJoke = new Joke("New Joke " + DateTime.Now);
        _appDbContext.Jokes.Add(newJoke);
        _appDbContext.SaveChanges();

    }

    private List<Joke> GetJokesFromFile() {
        return new();
    }

}
