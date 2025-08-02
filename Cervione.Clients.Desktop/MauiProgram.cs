using Cervione.Clients.Desktop.Handlers;
using Cervione.Clients.Desktop.Services;
using Cervione.Clients.Shared;
using Cervione.Clients.Shared.Providers;
using Cervione.Clients.Shared.Services;
using Cervione.Clients.Shared.Services.Api;
using Cervione.Core;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Cervione.Clients.Desktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddLucideIcons();
        builder.Services.AddSingleton<IApiContextAccessor, ApiContextAccessor>();
        builder.Services.AddScoped<ApiClient>();
        builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        builder.Services.AddScoped<HttpClient>(provider =>
        {
            string? url = string.Empty;
            
            Task.Run(async () =>
            {
                url = await SecureStorage.Default.GetAsync(Constants.ServerUrl);
            })
            .Wait();
            
            return new HttpClient(new JwtAuthorizationHandler(provider.GetRequiredService<NavigationManager>()))
            {
                BaseAddress = new Uri(!string.IsNullOrEmpty(url) ? url : "https://localhost:9001")
            };
        });
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}