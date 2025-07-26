using System.Net;

using Microsoft.AspNetCore.Components;

namespace Cervione.Clients.Desktop.Handlers;

public sealed partial class JwtAuthorizationHandler : DelegatingHandler
{
    private readonly NavigationManager _navigation;
    
    public JwtAuthorizationHandler(NavigationManager navigation)
    {
        InnerHandler = new HttpClientHandler();
        
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            _navigation.NavigateTo("/login", true);
        }

        return response;
    }
}