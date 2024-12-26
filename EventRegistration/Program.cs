using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using EventRegistration.Data;
using Prometheus;
using EventRegistration.Services;


var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configure identity options if needed
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });


builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddRouting();
builder.Services.AddEndpointsApiExplorer();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddSystemdConsole();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EventCreatorPolicy", policy =>
        policy.RequireRole("EventCreator"));
    options.AddPolicy("EventParticipantPolicy", policy =>
        policy.RequireRole("EventParticipant"));
});

builder.Services.AddHealthChecks();

builder.Services.AddScoped<IUserService>();
builder.Services.AddScoped<IEventService>();
builder.Services.AddScoped<IRegistrationService>();
builder.Services.AddScoped<ICheckService>();


var app = builder.Build();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
SeedData.Initialize(services).Wait();


// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Ensure Authentication middleware is used
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHealthChecks("/health");
app.MapMetrics();
app.Run();