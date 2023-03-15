using Avangardum.AsposeTestTask.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Avangardum.AsposeTestTask.Data;
using Avangardum.AsposeTestTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
ConfigureServices();
var app = builder.Build();
ConfigureMiddlewarePipeline();
app.Run();

void ConfigureServices()
{
    ConfigureDatabase();
    ConfigureIdentity();
    ConfigureAuthentication();
    ConfigureAuthorization();
    ConfigureRazorPages();
    ConfigureOwnServices();

    void ConfigureDatabase()
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();
    }

    void ConfigureIdentity()
    {
        services
            .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    void ConfigureAuthentication()
    {
        services.AddAuthentication().AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ??
                                     throw new InvalidOperationException();
            googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ??
                                         throw new InvalidOperationException();
        });
    }

    void ConfigureAuthorization()
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CanManagePost",
                policyBuilder => { policyBuilder.Requirements.Add(new IsPostAuthorRequirement()); });
        });
        services.AddScoped<IAuthorizationHandler, IsPostAuthorHandler>();
    }

    void ConfigureRazorPages()
    {
        services.AddRazorPages();
    }

    void ConfigureOwnServices()
    {
        services.AddScoped<PostService>();
    }
}

void ConfigureMiddlewarePipeline()
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    
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

    app.UseStatusCodePagesWithRedirects("/{0}");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapRazorPages();
}
