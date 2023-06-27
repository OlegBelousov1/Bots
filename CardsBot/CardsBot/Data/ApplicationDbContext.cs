using CardsBot.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardsBot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BotUser> BotUsers { get; set; }    
        public DbSet<Message> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}