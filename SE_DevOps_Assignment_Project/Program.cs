using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using SE_DevOps_DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"))
);

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


var app = builder.Build();

app.UseMetricServer(url: "/metrics");

using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    var dbContext = scopedServices.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
