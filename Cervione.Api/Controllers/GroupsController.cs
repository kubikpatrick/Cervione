using Cervione.Api.Data;
using Cervione.Core.Models.Groups;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cervione.Api.Controllers;

[Route("[controller]")]
public sealed class GroupsController : AuthorizedControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public GroupsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public async Task<ActionResult<Group[]>> Me()
    {
        var groups = await _context.Groups
            .Where(g => g.UserId == CurrentUserId)
            .ToArrayAsync();
        
        return Ok(groups);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Group>> Get([FromRoute] string id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group is null || group.UserId != CurrentUserId)
        {
            return NotFound();
        }

        return Ok(group);
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Create([FromQuery] string name)
    {
        bool exists = await _context.Groups.AnyAsync(
            g => g.Name == name && 
            g.UserId == CurrentUserId
        );
        
        if (exists)
        {
            return Conflict();
        }
        
        await _context.Groups.AddAsync(new Group
        {
            Name = name,
            CreatedAt = DateTime.UtcNow,
            Code = Guid.NewGuid().ToString(),
            UserId = CurrentUserId,
            Members = 
            [
                new Member
                {
                    CreatedAt = DateTime.UtcNow,
                    UserId = CurrentUserId
                }
            ]
        });
        
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        var group = await _context.Groups.FindAsync(id);
        if (group is null || group.UserId != CurrentUserId)
        {
            return NotFound();
        }
        
        _context.Groups.Remove(group);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
}