using Cervione.Api.Data;
using Cervione.Api.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Cervione.Api;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddSignalR();
        builder.Services.AddDefaultCors();
        builder.Services.AddMemoryCache();
        builder.Services.AddIdentity();
        builder.Services.AddTokenAuthentication(builder.Configuration);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
        
        app.Run();
    }
}