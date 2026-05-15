using CineTecMobile.Services;
using CineTecMobile.Models;

namespace CineTecMobile.Pages;

public partial class RegistroPage : ContentPage
{
    private readonly UsuarioService _usuarioService;

    public RegistroPage(UsuarioService usuarioService)
    {
        InitializeComponent();
        _usuarioService = usuarioService;

        // Mostrar/ocultar campos de estudiante al cambiar el switch
        EsEstudianteSwitch.Toggled += (s, e) =>
        {
            UniversidadLabel.IsVisible = e.Value;
            UniversidadEntry.IsVisible = e.Value;
            CarnetLabel.IsVisible = e.Value;
            CarnetEntry.IsVisible = e.Value;
        };
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        MensajeLabel.IsVisible = false;

        // Validar campos requeridos
        if (string.IsNullOrWhiteSpace(NombreCompletoEntry.Text))
        {
            MostrarError("El nombre es requerido");
            return;
        }

        if (string.IsNullOrWhiteSpace(CedulaEntry.Text))
        {
            MostrarError("La cédula es requerida");
            return;
        }

        if (CedulaEntry.Text.Length < 9)
        {
            MostrarError("La cédula debe tener al menos 9 dígitos");
            return;
        }

        try
        {
            var button = sender as Button;
            button.IsEnabled = false;
            button.Text = "Registrando...";

            // Crear objeto de registro
            var usuarioDto = new
            {
                nombreCompleto = NombreCompletoEntry.Text,
                cedula = CedulaEntry.Text,
                telefono = TelefonoEntry.Text ?? "",
                email = EmailEntry.Text ?? "",
                esEstudiante = EsEstudianteSwitch.IsToggled,
                universidad = EsEstudianteSwitch.IsToggled ? UniversidadEntry.Text : "",
                carnet = EsEstudianteSwitch.IsToggled ? CarnetEntry.Text : ""
            };

            // Registrar usuario
            var usuarioRegistrado = await _usuarioService.Registrar(usuarioDto);

            if (usuarioRegistrado != null)
            {
                await DisplayAlert("¡Éxito!", 
                    $"¡Bienvenido {usuarioRegistrado.NombreCompleto}!\nYa puedes iniciar sesión", 
                    "OK");

                // Limpiar campos
                LimpiarCampos();

                // Volver al login
                await Shell.Current.GoToAsync("/");
            }
            else
            {
                MostrarError("Error al registrar el usuario");
            }
        }
        catch (HttpRequestException ex)
        {
            MostrarError($"Error de conexión: {ex.Message}");
        }
        catch (Exception ex)
        {
            MostrarError($"Error: {ex.Message}");
        }
        finally
        {
            var button = sender as Button;
            button.IsEnabled = true;
            button.Text = "Registrarse";
        }
    }

    private async void OnVolverLoginClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar", 
            "¿Deseas volver al login? Se perderán los datos ingresados.", 
            "Sí", "No");

        if (confirmar)
        {
            await Shell.Current.GoToAsync("/");
        }
    }

    private void MostrarError(string mensaje)
    {
        MensajeLabel.Text = mensaje;
        MensajeLabel.IsVisible = true;
    }

    private void LimpiarCampos()
    {
        NombreCompletoEntry.Text = "";
        CedulaEntry.Text = "";
        TelefonoEntry.Text = "";
        EmailEntry.Text = "";
        UniversidadEntry.Text = "";
        CarnetEntry.Text = "";
        EsEstudianteSwitch.IsToggled = false;
    }
}