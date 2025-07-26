using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

using Cervione.Core.Models.Identity;

using Microsoft.IdentityModel.Tokens;

namespace Cervione.Api.Services;

public sealed class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
    
    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Picture, user.Avatar)
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            null,
            DateTime.Now.AddHours(24),
            credentials
        );

        return _handler.WriteToken(token);
    }

    public Claim[] ExtractClaims(string token)
    {
        if (!_handler.CanReadToken(token))
        {
            throw new SecurityTokenException("Invalid token.");
        }
        
        return _handler.ReadJwtToken(token).Claims.ToArray();
    }
}