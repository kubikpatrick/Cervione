using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

namespace Cervione.Api.Controllers;

[ApiController]
public abstract class AuthorizedControllerBase : ControllerBase
{
    public string CurrentUserId => User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? throw new InvalidOperationException();
}