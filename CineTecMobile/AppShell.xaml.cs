namespace CineTecMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("MenuPage", typeof(Pages.MenuPage));
        Routing.RegisterRoute("CinesPage", typeof(Pages.CinesPage));
        Routing.RegisterRoute("PeliculasPage", typeof(Pages.PeliculasPage));
        Routing.RegisterRoute("ProyeccionesPage", typeof(Pages.ProyeccionesPage));
        Routing.RegisterRoute("AsientosPage", typeof(Pages.AsientosPage));

    }

}