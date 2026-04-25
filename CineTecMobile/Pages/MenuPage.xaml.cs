namespace CineTecMobile.Pages;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Simulación de usuario (luego lo conectamos bien)
        string nombre = "Jose";

        BienvenidaUsuario.Text = $"¡Bienvenido, {nombre}!";
    }

    private async void OnCinesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CinesPage");
    }

    private async void OnMisBoletosClicked(object sender, EventArgs e)
    {
        // luego lo hacemos
    }

    private async void OnPerfilClicked(object sender, EventArgs e)
    {
        // luego lo hacemos
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // luego lo hacemos
    }
}