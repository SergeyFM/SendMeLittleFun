using Microsoft.EntityFrameworkCore;

namespace SendMeLittleFun.WebApp.Models;

public class ApplicationUser : DbContext {
    public ApplicationUser(DbContextOptions<ApplicationUser> options) : base(options) { }
    public DbSet<User> UserRegistration { get; set; }
}
