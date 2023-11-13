namespace SendMeLittleFun.WebApp.Services;

public class JokesUpdateService : IHostedService {

    public Task StartAsync(CancellationToken cancellationToken) {
        LoadNewJokesToDB();
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void LoadNewJokesToDB() {
        Console.WriteLine("Find and load new jokes to DB...");
    }
}
