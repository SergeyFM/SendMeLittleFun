using Hangfire;
using Hangfire.Storage;
using SendMeLittleFun.WebApp.Models;

namespace SendMeLittleFun.WebApp.Services;

public class JobManager : IJobManager {
    private IEmailService _emailService;
    public JobManager(IEmailService emailService) => _emailService = emailService;

    public static string jobPrefix = "Литтлфан для ";

    public void AddEmailJob(Email email, string cronExpr = "-") {

        // First remove previous jobs for this email
        DeleteEmailJob(email.EmailAddress);

        // Fire and forget a job
        RecurringJob.AddOrUpdate($"{jobPrefix}{email.EmailAddress}", () => _emailService.Send(email.EmailAddress, email.Subject, email.Body), cronExpr);

    }

    public int DeleteEmailJob(string email) {
        var allJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
        List<RecurringJobDto> thisEmailJobs = allJobs
            .Where(x => x.Id.ToLower() == (jobPrefix+email).ToLower())?
            .ToList() ?? new();
        foreach (var job in thisEmailJobs) RecurringJob.RemoveIfExists(job.Id);
        return thisEmailJobs.Count();
    }


}
