using System.Net.Http.Json;

using Cervione.Core.Models.Groups;
using Device = Cervione.Core.Models.Devices.Device;

using Community.Blazor.MapLibre;
using Community.Blazor.MapLibre.Models;
using Community.Blazor.MapLibre.Models.Control;
using Community.Blazor.MapLibre.Models.Marker;

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
        MinZoom = 2,
        MaxZoom = 23,
        CanvasContextAttributes = new WebGLContextAttributes
        {
            Antialias = true,
            ContextType = "webgl2"
        }
    };

    protected override async Task OnInitializedAsync()
    {
        Devices = await _http.GetFromJsonAsync<List<Device>>("/devices/me");
    }

    private async Task OnStyleLoaded()
    {
        await _map.AddControl(ControlType.NavigationControl, ControlPosition.TopRight);
        await _map.AddControl(ControlType.GlobeControl, ControlPosition.TopRight);
        
        var position = await Geolocation.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.High,
            RequestFullAccuracy = true
        });
        
        if (position is not null)
        {
            await _map.SetZoom(14);
            await _map.SetCenter(new LngLat(position.Longitude, position.Latitude));
            await _map.AddMarker(new MarkerOptions
            {
                OpacityWhenCovered = "0",
                Offset = [0, 0],
                Extensions = new MarkerOptionsExtensions
                {
                    HtmlContent = "<div class='me-marker'></div>" 
                }
            }, new LngLat(position.Longitude, position.Latitude));
        }

        await RenderMarkersAsync();
    }

    private async Task RenderMarkersAsync()
    {
        foreach (var device in Devices)
        {
            await _map.AddMarker(new MarkerOptions
            {
                OpacityWhenCovered = "0",
                Extensions = new MarkerOptionsExtensions
                {
                    HtmlContent = "<img src='https://i.pravatar.cc/300' class='marker' />" 
                }
            }, new LngLat(device.Position.Longitude, device.Position.Latitude));
        }
    }
}