using CineTecMobile.Services;
using CineTecMobile.Models;
using Microsoft.Extensions.Logging;

namespace CineTecMobile.Pages;

public partial class LoginPage : ContentPage
{
    private readonly UsuarioService _usuarioService;
    private readonly ILogger<LoginPage> _logger;

    public LoginPage(UsuarioService usuarioService, ILogger<LoginPage> logger)
    {
        InitializeComponent();
        _usuarioService = usuarioService;
        _logger = logger;
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
            _logger.LogInformation($"Intentando login con cédula: {cedula}");
            
            // Mostrar indicador de carga
            LoginButton.IsEnabled = false;
            LoginButton.Text = "Conectando...";

            // Obtener usuario del API
            var usuario = await _usuarioService.ObtenerPorCedula(cedula);

            if (usuario == null)
            {
                await DisplayAlert("Error", "Usuario no encontrado", "OK");
                LoginButton.IsEnabled = true;
                LoginButton.Text = "Iniciar Sesión";
                return;
            }

            // Log para debugging
            _logger.LogInformation($"Usuario obtenido: Nombre='{usuario.Nombre}', Apellido='{usuario.Apellido}', Cedula='{usuario.Cedula}', NombreCompleto='{usuario.NombreCompleto}'");

            // Guardar sesión
            Preferences.Set("usuarioNombre", usuario.Nombre ?? "");
            Preferences.Set("usuarioApellido", usuario.Apellido ?? "");
            Preferences.Set("usuarioCedula", usuario.Cedula ?? "");

            _logger.LogInformation($"Datos guardados en Preferences - Nombre: '{Preferences.Get("usuarioNombre", "")}', Apellido: '{Preferences.Get("usuarioApellido", "")}', Cedula: '{Preferences.Get("usuarioCedula", "")}'");

            _logger.LogInformation($"Login exitoso: {usuario.NombreCompleto}");

            await DisplayAlert("Bienvenido", $"Nombre: {usuario.Nombre}\nApellido: {usuario.Apellido}\nCédula: {usuario.Cedula}", "OK");

            await Shell.Current.GoToAsync("MenuPage");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error de conexión: {ex.Message}");
            await DisplayAlert("Error de Conexión", 
                "No se pudo conectar al servidor. Verifica:\n" +
                "• El API está corriendo\n" +
                "• La IP es correcta (172.18.92.169)\n" +
                "• El puerto es 5000\n" +
                "• La red está disponible\n\n" +
                $"Detalles: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error inesperado: {ex.Message}");
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            LoginButton.IsEnabled = true;
            LoginButton.Text = "Iniciar Sesión";
        }
    }

    private async void OnCrearCuentaClicked(object sender, EventArgs e)
    {
        DisplayAlert("Crear Cuenta", "Funcionalidad de creación de cuenta no implementada aún.", "OK");
    }
}