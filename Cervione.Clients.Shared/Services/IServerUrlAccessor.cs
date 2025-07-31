namespace Cervione.Clients.Shared.Services;

public interface IServerUrlAccessor
{
    public Task<string?> GetServerUrlAsync();
    
    public Task SetServerUrlAsync(string url);
    
    public Task RemoveServerUrlAsync();
}