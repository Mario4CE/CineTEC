using System.Text.Json;

namespace CineTecMobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string cedula = CedulaEntry.Text;

        if (string.IsNullOrWhiteSpace(cedula))
        {
            await DisplayAlert("Error", "Ingrese una cédula", "OK");
            return;
        }

        try
        {
            using var client = new HttpClient();

            // endpoint
            var response = await client.GetAsync($"http://192.168.0.14:5000/api/usuarios/cedula/{cedula}");

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Usuario no encontrado", "OK");
                return;
            }

            var json = await response.Content.ReadAsStringAsync();

            var usuario = JsonSerializer.Deserialize<Usuario>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // guardar sesión
            Preferences.Set("usuarioNombre", usuario.NombreCompleto);
            Preferences.Set("usuarioCedula", usuario.Cedula);

            await DisplayAlert("Bienvenido", usuario.NombreCompleto, "OK");

            await Shell.Current.GoToAsync("MenuPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo conectar", "OK");
            Console.WriteLine(ex.Message);
        }
    }
    private async void OnCrearCuentaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "Función no disponible aún 😅", "OK");
    }
}