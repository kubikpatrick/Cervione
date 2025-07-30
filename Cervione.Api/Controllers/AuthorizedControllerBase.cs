using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cervione.Api.Controllers;

[Authorize]
[ApiController]
public abstract class AuthorizedControllerBase : ControllerBase
{
    protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException();
}