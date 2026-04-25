using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class RegistroPage : ContentPage
{
    public RegistroPage()
    {
        InitializeComponent();

        // Restricción: mínimo 13 años
        FechaNacimientoPicker.MaximumDate = DateTime.Today.AddYears(-13);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        MensajeLabel.IsVisible = false;

        var datos = new
        {
            nombre = NombreEntry.Text,
            apellido = ApellidoEntry.Text,
            cedula = CedulaEntry.Text,
            telefono = TelefonoEntry.Text,
            fechaNacimiento = FechaNacimientoPicker.Date
        };

        try
        {
            
            await Task.Delay(500); // simulación

            await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");

            // Ir al menú
            await Shell.Current.GoToAsync("MenuPage");
        }
        catch (Exception ex)
        {
            MensajeLabel.Text = ex.Message;
            MensajeLabel.IsVisible = true;
        }
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        // luego hacemos login page
        await DisplayAlert("Info", "Ir a login (pendiente)", "OK");
    }
}