using Hangfire;
using Hangfire.Storage;
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JobManager : IJobManager {
    private IJobEmailSender _jobEmailSender;
    private IConfiguration _config;
    private ApplicationDbContext _context;
    public JobManager(IJobEmailSender emailService, IConfiguration config, ApplicationDbContext dbContext) {
        _jobEmailSender = emailService;
        _config = config;
        _context = dbContext;
    }



    public void AddEmailJob(User user) {
        string cronExpr = user.Schedule;
        // First remove previous jobs for this email
        DeleteEmailJob(user.Email);

        // Make a job name
        string? jobName = getJobNameByUser(user);
        if (jobName is null) return;

        // Fire and forget a job
        RecurringJob.AddOrUpdate(jobName, () => _jobEmailSender.Send(user), user.Schedule, TimeZoneInfo.Local);

    }

    public int DeleteEmailJob(string emailAddress) {

        // Search for guid by email
        Guid foundGuid = new();
        List<User> allUsersWithGuid = _context.UserRegistration.Where(u => u.Email == emailAddress).ToList();
        if (allUsersWithGuid is null || allUsersWithGuid.Count == 0) return 0;
        foundGuid = allUsersWithGuid.First().UserGuid;

        // User to delete
        User user = new() {
            Email = emailAddress,
            UserGuid = foundGuid
        };

        // Delete jobs by job ID
        List<RecurringJobDto> allJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
        List<RecurringJobDto> thisEmailJobs = allJobs
            .Where(x => x.Id.ToLower() == getJobNameByUser(user)?.ToLower())?
            .ToList() ?? new();
        foreach (var job in thisEmailJobs) RecurringJob.RemoveIfExists(job.Id);
        return thisEmailJobs.Count();
    }

    private string? getJobNameByUser(User user) {
        string emailAddress = user.Email;
        if (string.IsNullOrWhiteSpace(emailAddress)) return null;
        string jobPrefix = _config["jobPrefix"] ?? "";
        string[] emailTwoParts = emailAddress.Split('@');
        string firstPart = emailTwoParts.First();
        string secondPart = emailTwoParts.Length > 1 ? emailTwoParts.Last() : "";
        string hiddenPart = new string('*', Math.Max(firstPart.Length - 2, 3));
        string openPart = firstPart.Length >= 2 ? firstPart[^2..] : "";
        string userGuid = user.UserGuid.ToString();
        var hiddenEmail = hiddenPart + openPart + "@" + secondPart + " : " + userGuid;
        return $"{jobPrefix}{hiddenEmail}";
    }

}
