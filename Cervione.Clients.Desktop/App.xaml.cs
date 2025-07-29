namespace Cervione.Clients.Desktop;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? state)
    {
        return new Window(new MainPage())
        {
            MinimumWidth = 1024,
            MinimumHeight = 720
        };
    }
}