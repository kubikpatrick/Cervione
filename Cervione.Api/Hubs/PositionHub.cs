using Cervione.Core;
using Cervione.Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Cervione.Api.Hubs;

[Authorize]
public sealed class PositionHub : Hub
{
    public PositionHub()
    {
        
    }
    
    [HubMethodName(WsRegisteredEventNames.UpdatePosition)]
    public async Task UpdatePositionAsync(Position position)
    {
        if (position.Type is PositionType.Device)
        {
            
        }
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}