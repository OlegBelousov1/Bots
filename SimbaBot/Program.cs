using CardsBot.BOT.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimbaBot.BOT;
using SimbaBot.BOT.Commands;
using SimbaBot.Data;
using SimbaBot.Extensions;
using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Implementations;
using SimbaBot.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>    
options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.Configure<BotSettings>(act => builder.Configuration.GetSection("BotSettings").Bind(act));
builder.Services.AddSingleton<Bot>();
builder.Services.AddScoped<Command, Bought>();
builder.Services.AddScoped<Command, HelloCommand>();
builder.Services.AddScoped<Command, MyBalance>();
builder.Services.AddScoped<Command, Subscribe>();
builder.Services.AddScoped<Command, RefferalProgram>();
builder.Services.AddScoped<Command, RefferLink>();
builder.Services.AddScoped<Command, WithdrawalOfBalance>();
builder.Services.AddScoped<Command, WithdrawalRequest>();
builder.Services.AddScoped<IKeyboardManager, KeyboardManager>();
builder.Services.AddScoped<IPaymentManager, PaymentManager>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<IWithdrawalManager, WithdrawalManager>();

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
    app.UseExceptionHandler("/Error");
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
