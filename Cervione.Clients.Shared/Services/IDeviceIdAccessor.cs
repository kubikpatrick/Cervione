namespace Cervione.Clients.Shared.Services;

public interface IDeviceIdAccessor
{
    public Task<string?> GetDeviceIdAsync();
}