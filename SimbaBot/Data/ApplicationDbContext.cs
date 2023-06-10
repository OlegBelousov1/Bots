using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimbaBot.Models;

namespace SimbaBot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<BotUser> BotUsers { get; set; }
        public virtual DbSet<Withdrawal> Withdrawals { get; set; }  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}