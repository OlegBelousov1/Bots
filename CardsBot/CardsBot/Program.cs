using CardsBot.BOT;
using CardsBot.BOT.Commands;
using CardsBot.Data;
using CardsBot.Extensions;
using CardsBot.Models;
using CardsBot.Repository;
using CardsBot.Services.Implementations;
using CardsBot.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.Configure<BotSettings>(act => builder.Configuration.GetSection("BotSettings").Bind(act));
builder.Services.AddSingleton<Bot>();
builder.Services.AddScoped<IKeyboardManager, KeyboardManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IMessageManager, MessageManager>();
builder.Services.AddScoped<IPhotoManager, PhotoManager>();
builder.Services.AddScoped<Command, HelloCommand>();
builder.Services.AddScoped<Command, CreditCardsCommand>();
builder.Services.AddScoped<Command, DebetCardsCommand>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

var bot = app.Services.GetRequiredService<Bot>();
bot.Get();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.ApplyMigrations();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
