namespace CineTecMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("LoginPage", typeof(Pages.LoginPage));
        Routing.RegisterRoute("MenuPage", typeof(Pages.MenuPage));
        Routing.RegisterRoute("CinesPage", typeof(Pages.CinesPage));
        Routing.RegisterRoute("PeliculasPage", typeof(Pages.PeliculasPage));
        Routing.RegisterRoute("ProyeccionesPage", typeof(Pages.ProyeccionesPage));
        Routing.RegisterRoute("AsientosPage", typeof(Pages.AsientosPage));
        Routing.RegisterRoute("ResumenPage", typeof(Pages.ResumenPage));
        Routing.RegisterRoute("MisBoletosPage", typeof(Pages.MisBoletosPage));
        Routing.RegisterRoute("PerfilPage", typeof(Pages.PerfilPage));
        Routing.RegisterRoute("RegistroPage", typeof(Pages.RegistroPage));
    }

}