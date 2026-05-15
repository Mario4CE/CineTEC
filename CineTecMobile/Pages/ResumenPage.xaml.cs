using CineTecMobile.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CineTecMobile.Pages;

public partial class ResumenPage : ContentPage
{
    private readonly ReservaService _reservaService;

    public ResumenPage()
    {
        InitializeComponent();
        _reservaService = new ReservaService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarDatos();
    }

    void CargarDatos()
    {
        try
        {
            // Obtener datos de la reserva que guardamos en Preferences
            string numeroFactura = Preferences.Get("numeroFactura", "");
            int totalPago = Preferences.Get("totalPago", 0);
            string asientosSeleccionados = Preferences.Get("asientosSeleccionados", "");
            string nombrePelicula = Preferences.Get("peliculaNombre", "");
            string nombreCine = Preferences.Get("cineNombre", "");
            string horaProyeccion = Preferences.Get("horaProyeccion", "");

            // Llenar los labels
            NumeroFacturaLabel.Text = numeroFactura;
            FechaLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            PeliculaLabel.Text = nombrePelicula;
            CineLabel.Text = $"Cine: {nombreCine}";
            FechaHoraLabel.Text = $"Hora: {horaProyeccion}";

            // Asientos
            var asientosList = asientosSeleccionados.Split(',').ToList();
            AsientosLabel.Text = string.Join(", ", asientosList);

            // Pago
            int cantidadAsientos = asientosList.Count;
            const int PRECIO_UNITARIO = 8000;

            CantidadLabel.Text = cantidadAsientos.ToString();
            PrecioUnitarioLabel.Text = $"₡{PRECIO_UNITARIO:N0}";
            TotalLabel.Text = $"₡{totalPago:N0}";

            EstadoLabel.Text = "Pendiente";
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
        }
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        try
        {
            string numeroFactura = Preferences.Get("numeroFactura", "");

            // Actualizar estado a "Confirmada"
            _reservaService.ActualizarEstado(numeroFactura, "Confirmada");

            await DisplayAlert("¡Éxito!", "Tu compra ha sido confirmada. ¡Que disfrutes la película! 🎬", "OK");

            // Navegar al menú
            await Shell.Current.GoToAsync("MenuPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al confirmar: {ex.Message}", "OK");
        }
    }

    private async void OnDescargarClicked(object sender, EventArgs e)
    {
        DisplayAlert("¡Funcionalidad en desarrollo!", "La descarga del resumen en PDF estará disponible próximamente. ¡Gracias por tu paciencia! 📄", "OK");
    }

    private async void OnVolverMenuClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MenuPage");
    }
}