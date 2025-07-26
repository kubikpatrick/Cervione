using System.Text;

using Cervione.Api.Data;
using Cervione.Api.Services;
using Cervione.Clients.Shared;
using Cervione.Core.Models.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cervione.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
    
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<JwtService>();
        services.AddAuthentication(options => 
        { 
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            };
            
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Query[Constants.AccessToken];
                    var path = context.HttpContext.Request.Path;
                    
                    if (!string.IsNullOrEmpty(token) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = token;
                    }
                    
                    return Task.CompletedTask;
                },
                
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    
                    return Task.CompletedTask;
                },
            };
        });
        
        return services;
    }
    
    public static IServiceCollection AddDefaultCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });

        return services;
    }
}