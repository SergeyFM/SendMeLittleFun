using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendMeLittleFun.WebApp.Models;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using SendMeLittleFun.WebApp.Services;

namespace SendMeLittleFun.WebApp;


public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Myconnection");
        // Add services to the container.
        builder.Services.AddDbContext<ApplicationUser>(x => x.UseSqlServer(connectionString));
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IJobManager, JobManager>();

        // HangFire

        builder.Services.AddHangfire(hf => {
            hf.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            hf.UseSimpleAssemblyNameTypeSerializer();
            hf.UseRecommendedSerializerSettings();
            hf.UseColouredConsoleLogProvider();
            hf.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnection"),
                new SqlServerStorageOptions {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });
            var server = new BackgroundJobServer(new BackgroundJobServerOptions {ServerName = "HangFire server" });


        } );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Hangfire
        app.UseHangfireDashboard("/hangfire", new DashboardOptions {
            IsReadOnlyFunc = (DashboardContext context) => false // READ ONLY OR NOT
        });

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
