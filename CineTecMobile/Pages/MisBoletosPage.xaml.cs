using CineTecMobile.Models;
using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class MisBoletosPage : ContentPage
{
    private readonly ReservaService _reservaService;
    private List<BoletoViewModel> misBoletosFormateados = new();

    public MisBoletosPage()
    {
        InitializeComponent();
        _reservaService = new ReservaService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarMisBoletosAsync();
    }

    private async Task CargarMisBoletosAsync()
    {
        try
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            Boletos.IsVisible = false;
            MensajeVacioLabel.IsVisible = false;

            string cedulaUsuario = Preferences.Get("usuarioCedula", "");

            if (string.IsNullOrEmpty(cedulaUsuario))
            {
                MensajeVacioLabel.Text = "Debes iniciar sesión";
                MensajeVacioLabel.IsVisible = true;
                return;
            }

            // Obtener reservas del usuario
            var reservas = _reservaService.ObtenerReservasDelUsuario(cedulaUsuario);

            if (reservas == null || reservas.Count == 0)
            {
                MensajeVacioLabel.IsVisible = true;
                return;
            }

            // Convertir a BoletoViewModel
            misBoletosFormateados = reservas
                .Select(r => new BoletoViewModel
                {
                    Id = r.Id,
                    NumeroFactura = r.NumeroFactura,
                    NombrePelicula = r.NombrePelicula,
                    NombreCine = r.NombreCine,
                    HoraProyeccion = r.HoraProyeccion,
                    Asientos = r.Asientos,
                    AsientosFormateados = string.Join(", ", r.Asientos),
                    CantidadAsientos = r.CantidadAsientos,
                    TotalPago = r.TotalPago,
                    TotalFormateado = $"Total: ₡{r.TotalPago:N0}",
                    Estado = r.Estado,
                    FechaCreacion = r.FechaCreacion,
                    FechaFormateada = r.FechaCreacion.ToString("dd/MM/yyyy")
                })
                .OrderByDescending(b => b.FechaCreacion)
                .ToList();

            Boletos.ItemsSource = misBoletosFormateados;
            Boletos.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar boletos: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    private async void OnVerDetallesClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var boleto = btn?.BindingContext as BoletoViewModel;

        if (boleto == null) return;

        string detalles = $"""
            Película: {boleto.NombrePelicula}
            Cine: {boleto.NombreCine}
            Hora: {boleto.HoraProyeccion}
            Asientos: {boleto.AsientosFormateados}
            Total: ₡{boleto.TotalPago:N0}
            Estado: {boleto.Estado}
            Factura: {boleto.NumeroFactura}
            Fecha: {boleto.FechaFormateada}
            """;

        await DisplayAlert("Detalles del Boleto", detalles, "OK");
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var boleto = btn?.BindingContext as BoletoViewModel;

        if (boleto == null) return;

        bool confirmar = await DisplayAlert("Confirmar", 
            $"¿Deseas cancelar la compra de {boleto.NombrePelicula}?", 
            "Sí", "No");

        if (confirmar)
        {
            try
            {
                _reservaService.ActualizarEstado(boleto.NumeroFactura, "Cancelada");
                await DisplayAlert("Éxito", "La compra ha sido cancelada", "OK");
                await CargarMisBoletosAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al cancelar: {ex.Message}", "OK");
            }
        }
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MenuPage");
    }

    /// <summary>
    /// Modelo de vista para mostrar boletos de forma más legible
    /// </summary>
    private class BoletoViewModel
    {
        public int Id { get; set; }
        public string NumeroFactura { get; set; }
        public string NombrePelicula { get; set; }
        public string NombreCine { get; set; }
        public string HoraProyeccion { get; set; }
        public List<string> Asientos { get; set; }
        public string AsientosFormateados { get; set; }
        public int CantidadAsientos { get; set; }
        public decimal TotalPago { get; set; }
        public string TotalFormateado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string FechaFormateada { get; set; }
    }
}
