using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YgoLocals;
using YgoLocals.Data;
using YgoLocals.Data.Entities;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<Config>(builder.Configuration.GetSection("AppConfig"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<User>() // options => options.SignIn.RequireConfirmedAccount = true
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using var services = app.Services.CreateScope();
var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();
dbContext.Database.Migrate();
new ApplicationDbSeeder().SeedAsync(dbContext, services.ServiceProvider).GetAwaiter().GetResult();

app.Run();
