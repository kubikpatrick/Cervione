using Cervione.Api.Data;
using Cervione.Core.Models.Devices;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cervione.Api.Controllers;

[Route("[controller]")]
public sealed class DevicesController : AuthorizedControllerBase
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
            .Where(d => d.UserId == CurrentUserId)
            .ToArrayAsync();

        return Ok(devices);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Device>> Get([FromRoute] string id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device is null || device.UserId != CurrentUserId)
        {
            return NotFound();
        }

        return Ok(device);
    }
}