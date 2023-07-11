using InstaDirect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InstaDirect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BotUser> BotUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefferalsInfo> Refferals { get; set; }
        public DbSet<Text> Texts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}