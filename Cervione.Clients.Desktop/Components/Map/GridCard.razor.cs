using Cervione.Core.Models;

using Microsoft.AspNetCore.Components;

namespace Cervione.Clients.Desktop.Components.Map;

public partial class GridCard : ComponentBase
{
    [Parameter]
    public required string Title { get; set; }
    
    [Parameter]
    public required string Icon { get; set; }
    
    [Parameter]
    public required Position Position { get; set; }
}