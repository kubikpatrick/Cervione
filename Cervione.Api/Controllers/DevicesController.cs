using System.Security.Claims;
using Cervione.Api.Data;
using Cervione.Core.Models.Devices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Cervione.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public sealed class DevicesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public DevicesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public async Task<ActionResult<Device[]>> Me()
    {
        var devices = await _context.Devices
            .Where(d => d.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .ToArrayAsync();

        return Ok(devices);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Device>> Get([FromRoute] string id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null || device.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return NotFound();
        }

        return Ok(device);
    }
}