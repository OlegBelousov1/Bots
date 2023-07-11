using InstaDirect.BOT;
using InstaDirect.BOT.Commands;
using InstaDirect.Data;
using InstaDirect.Extensions;
using InstaDirect.Models;
using InstaDirect.Repository;
using InstaDirect.Services.Implementations;
using InstaDirect.Services.Interfaces;
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
builder.Services.Configure<ChannelsIds>(act => builder.Configuration.GetSection("ChannelsIds").Bind(act));
builder.Services.AddSingleton<Bot>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IKeyboardManager, KeyboardManager>();
builder.Services.AddScoped<IRefferalManager, RefferalManager>();
builder.Services.AddScoped<ISubscribeManager, SubscribeManager>();
builder.Services.AddScoped<ITextManager, TextManager>();
builder.Services.AddScoped<IMessageManager, MessageManager>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<Command, BuyRegPro>();
builder.Services.AddScoped<Command, GetAccount>();
builder.Services.AddScoped<Command, HelloCommand>();
builder.Services.AddScoped<Command, RefferLink>();
builder.Services.AddScoped<Command, Shop>();
builder.Services.AddScoped<Command, SubscribeToLiteChannel>();
builder.Services.AddScoped<Command, SubscribeToInformationChannel>();
builder.Services.AddScoped<Command, Support>();
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
