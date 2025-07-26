using Microsoft.AspNetCore.Components;

namespace Cervione.Clients.Desktop.Components.Layout;

public partial class Redirect : ComponentBase
{
    private readonly NavigationManager _navigation;
    
    public Redirect(NavigationManager navigation)
    {
        _navigation = navigation;
    }
    
    [Parameter]
    public string Url { get; set; }
    
    protected override void OnInitialized()
    {
        _navigation.NavigateTo(Url);
    }
}