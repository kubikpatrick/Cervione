namespace Cervione.Clients.Shared.Services.Api;

public abstract class ApiClientBase
{
    protected readonly HttpClient Http;

    protected ApiClientBase(HttpClient http)
    {
        Http = http;
    }
}