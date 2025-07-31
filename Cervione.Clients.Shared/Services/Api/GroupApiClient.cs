using System.Net.Http.Json;

using Cervione.Core.Models.Groups;

namespace Cervione.Clients.Shared.Services.Api;

public sealed class GroupApiClient : ApiClientBase
{
    public GroupApiClient(HttpClient http) : base(http)
    {
        
    }

    public async Task<Group[]> MeAsync()
    {
        var groups = await Http.GetFromJsonAsync<Group[]>("/devices/me");

        return groups ?? [];
    }

    public async Task<Group?> GetAsync(string id)
    {
        var group = await Http.GetFromJsonAsync<Group>($"/devices/{id}");

        return group;
    }
}