using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class PerfilPage : ContentPage
{
    private readonly ReservaService _reservaService;

    public PerfilPage()
    {
        InitializeComponent();
        _reservaService = new ReservaService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarPerfil();
    }

    private void CargarPerfil()
    {
        try
        {
            // Obtener datos del usuario
            string nombre = Preferences.Get("usuarioNombre", "Usuario");
            string cedula = Preferences.Get("usuarioCedula", "");

            NombreLabel.Text = nombre;
            CedulaLabel.Text = cedula;

            // Obtener estadísticas de reservas
            var reservas = _reservaService.ObtenerReservasDelUsuario(cedula);

            if (reservas != null && reservas.Count > 0)
            {
                // Total de boletos
                int totalAsientos = reservas.Sum(r => r.CantidadAsientos);
                TotalBoletosLabel.Text = totalAsientos.ToString();

                // Monto total gastado
                decimal montoTotal = reservas.Sum(r => r.TotalPago);
                MontoTotalLabel.Text = $"₡{montoTotal:N0}";

                // Películas únicas vistas
                int peliculasUnicas = reservas.Select(r => r.PeliculaId).Distinct().Count();
                PeliculasVistasLabel.Text = peliculasUnicas.ToString();

                // Cines visitados
                int cinesUnicos = reservas.Select(r => r.CineId).Distinct().Count();
                CinesVisitadosLabel.Text = cinesUnicos.ToString();
            }
            else
            {
                TotalBoletosLabel.Text = "0";
                MontoTotalLabel.Text = "₡0";
                PeliculasVistasLabel.Text = "0";
                CinesVisitadosLabel.Text = "0";
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error al cargar perfil: {ex.Message}", "OK");
        }
    }

    private async void OnEditarPerfilClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "La funcionalidad de editar perfil estará disponible pronto", "OK");
    }

    private async void OnCambiarContrasenaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Info", "La funcionalidad de cambiar contraseña estará disponible pronto", "OK");
    }

    private async void OnEliminarCuentaClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar", 
            "¿Estás seguro de que deseas eliminar tu cuenta? Esta acción no se puede deshacer.", 
            "Sí", "No");

        if (confirmar)
        {
            await DisplayAlert("Info", "La funcionalidad de eliminar cuenta estará disponible pronto", "OK");
        }
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MenuPage");
    }
}
