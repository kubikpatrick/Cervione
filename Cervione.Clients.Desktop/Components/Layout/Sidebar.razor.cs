using Cervione.Core.Models.Groups;
using Device = Cervione.Core.Models.Devices.Device;

using Microsoft.AspNetCore.Components;

namespace Cervione.Clients.Desktop.Components.Layout;

public partial class Sidebar : ComponentBase
{
    [Parameter]
    public List<Device> Devices { get; set; } = [];
    
    [Parameter]
    public List<Group> Groups { get; set; } = [];
}