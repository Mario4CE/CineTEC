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

        // Obtener nombre del usuario desde Preferences
        string nombre = Preferences.Get("usuarioNombre", "Usuario");

        BienvenidaUsuario.Text = $"¡Bienvenido, {nombre}!";
    }

    private async void OnCinesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CinesPage");
    }

    private async void OnMisBoletosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MisBoletosPage");
    }

    private async void OnPerfilClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("PerfilPage");
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar",
            "¿Deseas cerrar sesión?",
            "Sí",
            "No");

        if (confirmar)
        {
            try
            {
                // Limpiar datos de sesión
                Preferences.Remove("usuarioNombre");
                Preferences.Remove("usuarioCedula");
                Preferences.Remove("cineId");
                Preferences.Remove("cineNombre");

                // Volver al login con navegación limpia
                await Shell.Current.GoToAsync($"../LoginPage");
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Error",
                    $"Error al cerrar sesión: {ex.Message}",
                    "OK");
            }
        }
    }
}