using System;
using System.Text;
using Kosha.CustomerManager.Web.Areas.Account.Controllers;
using Kosha.CustomerManager.Web.Infrastructure;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Models.Configurations;
using Kosha.CustomerManager.Web.Models.Extensions;
using Kosha.CustomerManager.Web.Models.Infrastructure.Helper;
using Kosha.CustomerManager.Web.Models.Infrastructure.Models;
using Kosha.CustomerManager.Web.Models.Infrastructure.Options;
using Kosha.CustomerManager.Web.Models.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Kosha.CustomerManager.Web;

public static class Startup
{
    public static void ConfigurationServices(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.ConfigureOptions<TokenInformationConfigureOption>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ITaskBinderService, TaskBinderService>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);

                    options.LoginPath =
                        new PathString(
                            string.Format(
                                "{0}{1}{0}{2}",
                                RouteConfiguration.Separator,
                                AreaNameConfiguration.Account,
                                nameof(LoginController).RemoveControllerFromString()
                            )
                        );

                    options.LogoutPath =
                        new PathString(
                            string.Format(
                                "{0}{1}{0}{2}",
                                RouteConfiguration.Separator,
                                AreaNameConfiguration.Account,
                                nameof(LogoutController).RemoveControllerFromString()
                            )
                        );
                }
            )
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    TokenInformation information = new TokenInformation();

                    configuration.GetSection(TokenInformationConfigureOption.Section).Bind(information);

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = information.Issuer,
                            ValidAudience = information.Audience,
                            IssuerSigningKey = 
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(information.Key)
                                )
                        };
                }
            );

        services.AddHttpContextAccessor();

        services.AddControllersWithViews();

        services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            }
        );

        services.AddInfrastructure(configuration);
    }

    public static void Configuration(
        WebApplication app,
        IWebHostEnvironment environment
    )
    {
        app.UseInfrastructure(environment);

        if (!environment.IsDevelopment())
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

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    }
}