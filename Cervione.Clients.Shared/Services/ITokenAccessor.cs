namespace Cervione.Clients.Shared.Services;

public interface ITokenAccessor
{
    public Task<string?> GetTokenAsync();
    
    public Task SetTokenAsync(string token);
    
    public Task RemoveTokenAsync();
}