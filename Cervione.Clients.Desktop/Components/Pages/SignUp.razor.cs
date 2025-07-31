using System.Net.Http.Json;

using Cervione.Clients.Desktop.Components.Layout;
using Cervione.Clients.Shared.Providers;
using Cervione.Clients.Shared.Services;
using Cervione.Core.Models.Http;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cervione.Clients.Desktop.Components.Pages;

[Layout(typeof(AuthenticationLayout))]
public sealed partial class SignUp : ComponentBase
{
    private readonly IApiContextAccessor _accessor;
    private readonly JwtAuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _http;
    private readonly NavigationManager _navigation;
    
    public SignUp
    (
        IApiContextAccessor accessor,
        AuthenticationStateProvider authenticationStateProvider, 
        HttpClient http, 
        NavigationManager navigation
    )
    {
        _accessor = accessor;
        _authenticationStateProvider = authenticationStateProvider as JwtAuthenticationStateProvider;
        _http = http;
        _navigation = navigation;
    }

    private string _serverUrl = string.Empty;

    private readonly SignUpRequest _request = new SignUpRequest();
    
    protected override async Task OnInitializedAsync()
    {
        string? url = await _accessor.GetServerUrlAsync();
        if (!string.IsNullOrEmpty(url))
        {
            _serverUrl = url;
        }
    }

    private async Task SubmitAsync()
    {
        var response = await _http.PostAsJsonAsync(Path.Join(_serverUrl, "/authentication/login"), _request);
        if (!response.IsSuccessStatusCode)
        {
            return;
        }
        
        var content = await response.Content.ReadFromJsonAsync<TokenResponse>();

        await _accessor.SetTokenAsync(content.Token);
        await _accessor.SetServerUrlAsync(_serverUrl);
        await _authenticationStateProvider.NotifyAuthenticationAsync(content.Token);
        
        _navigation.NavigateTo("/");
    }
}