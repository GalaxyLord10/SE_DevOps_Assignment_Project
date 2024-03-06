using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using SE_DevOps_DataLayer;
using SE_DevOps_DataLayer.Services;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=host.docker.internal;Port=5432;Database=se_devops_assignment_project;Username=postgres;Password=postgres#Xzmypb21")
);

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<CategoryService>();

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
