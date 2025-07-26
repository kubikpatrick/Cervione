using Cervione.Clients.Shared;
using Cervione.Clients.Shared.Services;

namespace Cervione.Clients.Desktop.Services;

public sealed class ApiContextAccessor : IApiContextAccessor
{
    public async Task<string?> GetServerUrlAsync()
    {
        return await SecureStorage.Default.GetAsync(Constants.ServerUrl);
    }

    public async Task SetServerUrlAsync(string url)
    {
        await SecureStorage.Default.SetAsync(Constants.ServerUrl, url);
    }

    public Task RemoveServerUrlAsync()
    {
        SecureStorage.Default.Remove(Constants.ServerUrl);

        return Task.CompletedTask;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await SecureStorage.Default.GetAsync(Constants.AccessToken);
    }

    public async Task SetTokenAsync(string token)
    {
        await SecureStorage.Default.SetAsync(Constants.AccessToken, token);
    }

    public Task RemoveTokenAsync()
    {
        SecureStorage.Default.Remove(Constants.AccessToken);

        return Task.CompletedTask;
    }
}