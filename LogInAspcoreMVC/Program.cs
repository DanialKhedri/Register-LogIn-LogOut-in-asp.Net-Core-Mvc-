using Application.IServices;
using Application.Services;
using Domain.IRepositories;
using Infrastructure.DB;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//add dbcontext
builder.Services.AddDbContext<DataContext>();

//Repositories
builder.Services.AddScoped<IAddUserRepository, AddUserRepository>();
builder.Services.AddScoped<ILogInRepository, LogInRepository>();

//Services
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddScoped<IAddUserService, AddUserService>();

//HttpContextAccssor
builder.Services.AddHttpContextAccessor();

//Authentication
#region Authentication


    builder.Services.AddAuthentication(options =>
    {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     })
    // Add Cookie settings
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

#endregion

//Build
var app = builder.Build();

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


app.UseEndpoints(endpoints =>
{


    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
name: "default",
areaName: "SitePanel",
pattern: "{controller=User}/{action=Index}/{id?}"
 );



});





app.Run();
