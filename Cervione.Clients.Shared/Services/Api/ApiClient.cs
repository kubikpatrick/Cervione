namespace Cervione.Clients.Shared.Services.Api;

public sealed class ApiClient
{
    public ApiClient(HttpClient http)
    {
        Groups = new GroupApiClient(http);
    }

    public readonly GroupApiClient Groups;
}