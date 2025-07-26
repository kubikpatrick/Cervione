using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

using Cervione.Clients.Shared.Services;

using Microsoft.AspNetCore.Components.Authorization;

namespace Cervione.Clients.Shared.Providers;

public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IApiContextAccessor _accessor;
    private readonly HttpClient _http;
    
    public JwtAuthenticationStateProvider(IApiContextAccessor accessor, HttpClient http)
    {
        _accessor = accessor;
        _http = http;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        string? token = await _accessor.GetTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(anonymous);
        }
        
        try
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var claims = ExtractClaims(token);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return new AuthenticationState(user);
        }
        catch
        {
            await _accessor.RemoveTokenAsync();
            
            return new AuthenticationState(anonymous);
        }
    }
    
    public async Task NotifyAuthenticationAsync(string token)
    {
        await _accessor.SetTokenAsync(token);
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task NotifyLogoutAsync()
    {
        await _accessor.RemoveTokenAsync();
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    
    private Claim[] ExtractClaims(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToArray();
    }
}