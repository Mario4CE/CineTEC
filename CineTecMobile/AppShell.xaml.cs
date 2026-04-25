namespace CineTecMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("MenuPage", typeof(Pages.MenuPage));
        Routing.RegisterRoute("CinesPage", typeof(Pages.CinesPage));

    }

}