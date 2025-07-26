using Cervione.Api.Services;
using Cervione.Core.Models.Http;
using Cervione.Core.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cervione.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly JwtService _jwt;
    private readonly UserManager<User> _userManager;
    
    public AuthenticationController(JwtService jwt, UserManager<User> userManager)
    {
        _jwt = jwt;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return NotFound();
        }

        bool valid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!valid)
        {
            return Unauthorized();
        }
        
        return Ok(new TokenResponse
        {
            Token = _jwt.GenerateToken(user)
        });
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp([FromBody] SignUpRequest request)
    {
        bool exists = await _userManager.FindByEmailAsync(request.Email) is not null;
        if (exists)
        {
            return Conflict();
        }
        
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = string.Empty,
            LastName = string.Empty,
            Position = default,
            Avatar = "default.svg",
            CreatedAt = DateTime.UtcNow
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        return Created();
    }
}