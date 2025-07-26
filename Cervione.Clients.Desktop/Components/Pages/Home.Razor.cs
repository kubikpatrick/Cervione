using System.Net.Http.Json;
using Community.Blazor.MapLibre;
using Community.Blazor.MapLibre.Models.Control;

using Cervione.Core.Models.Groups;
using Device = Cervione.Core.Models.Devices.Device;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Cervione.Clients.Desktop.Components.Pages;

[Authorize]
public sealed partial class Home : ComponentBase
{
    private readonly HttpClient _http;
    
    public Home(HttpClient http)
    {
        _http = http;
    }
    
    public List<Device> Devices { get; set; } = [];
    public List<Group> Groups { get; set; } = [];
    
    private MapLibre _map = new MapLibre();
    private MapOptions Options => new MapOptions
    {
        Style = "style.json",
        MinZoom = 1,
        MaxZoom = 23
    };

    protected override async Task OnInitializedAsync()
    {
        Devices = await _http.GetFromJsonAsync<List<Device>>("/devices/me");
    }

    private async Task OnStyleLoaded()
    {
        await _map.AddControl(ControlType.NavigationControl, ControlPosition.TopRight);
        await _map.AddControl(ControlType.GeolocateControl, ControlPosition.TopRight);
        await _map.AddControl(ControlType.GlobeControl, ControlPosition.TopRight);
    }
}