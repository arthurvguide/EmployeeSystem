using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (registering ApplicationDbContext and Identity).
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeSystemDev")));

// Register Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    // Disable email confirmation requirement
    options.SignIn.RequireConfirmedAccount = false; // Change this to false
});

// Add Razor Pages services (required for ASP.NET Identity)
builder.Services.AddRazorPages();  // <-- This adds Razor Pages services

// Add services to support controllers and views.
builder.Services.AddControllersWithViews();

// Configure the login path for unauthenticated users
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // This is the default login page for Identity
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization middleware.
app.UseAuthentication();  // Required for ASP.NET Identity
app.UseAuthorization();

// Map Razor Pages (needed for Identity UI)
app.MapRazorPages();  // <-- This is essential for the Identity UI to work

// Map the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
