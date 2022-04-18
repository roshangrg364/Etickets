using CoreModule.DbContextConfig;
using CoreModule.Source.Entity;
using ETicketing;
using ETicketing.Extensions;
using ETicketing.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.SignIn.RequireConfirmedEmail = false;

});

builder.Services.UseETicketing();
builder.Services.ConfigureApplicationCookie(options =>
{
   
    options.Events.OnRedirectToAccessDenied = (evnt) =>
        {
            if (evnt.Request.Path.StartsWithSegments("/api") )
            {
                evnt.Response.StatusCode = 403;
                evnt.Response.ContentType = "application/json";
                var data = new
                {
                    Message = "You are not authorized here",
                    Code = StatusCodes.Status401Unauthorized,
                    Status = "401 Unauthorized",
                    Errors = "Unauthorized",
                    LoginRedirectUrl = "/Error/AccessDenied"
                };
                evnt.Response.WriteAsJsonAsync(JsonConvert.SerializeObject(data));
                return Task.CompletedTask;
            }
            else
            {
                evnt.Response.Redirect("/Error/AccessDenied");
            }

            return Task.CompletedTask;
        };
    options.Events.OnRedirectToLogin = (evnt) =>
    {
        if (evnt.Request.Path.StartsWithSegments("/api"))
        {
            evnt.Response.StatusCode = 401;
            evnt.Response.ContentType = "application/json";
            var data = new
            {
                Message = "You are not authorized here",
                Code = StatusCodes.Status401Unauthorized,
                Status = "401 Unauthorized",
                Errors = "Unauthorized",
                LoginRedirectUrl = "/Account/Login"
            };
            evnt.Response.WriteAsJsonAsync(JsonConvert.SerializeObject(data));
            return Task.CompletedTask;
        }
        else 
        {
            evnt.Response.Redirect("/Account/Login" + "?ReturnUrl=" + evnt.Request.Path);
        }
       
        return Task.CompletedTask;
    };
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

});

builder.Services.AddMvc()
    .AddMvcOptions(options =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        ProgressBar = true,
        TimeOut = 1500,
        PositionClass = ToastPositions.TopRight
    })
     .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null); ;

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
    logging.AddNLog(hostingContext.Configuration.GetSection("Logging"));
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
ServiceActivator.Configure(builder.Services.BuildServiceProvider());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseNToastNotify();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//seed database
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
