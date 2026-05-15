using CineTecMobile.Models;
using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class AsientosPage : ContentPage
{
    const int PRECIO = 8000;

    HashSet<string> seleccionados = new();

    int filas = 10;
    int columnas = 12;
    
    private readonly ReservaService _reservaService;

    public AsientosPage()
    {
        InitializeComponent();
        _reservaService = new ReservaService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string peli = Preferences.Get("peliculaNombre", "");
        string hora = Preferences.Get("horaProyeccion", "");

        InfoLabel.Text = $"{peli} - {hora}";

        GenerarAsientos();
    }

    void GenerarAsientos()
    {
        AsientosGrid.RowDefinitions.Clear();
        AsientosGrid.ColumnDefinitions.Clear();
        AsientosGrid.Children.Clear();

        // filas
        for (int i = 0; i < filas; i++)
        {
            AsientosGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
        }

        // columnas
        // Primera columna = letras
        AsientosGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });

        // Columnas de asientos
        for (int j = 0; j < columnas; j++)
        {
            AsientosGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 40 });
        }

        var random = new Random();

        for (int fila = 0; fila < filas; fila++)
        {
            //  letra (A, B, C...)
            var labelFila = new Label
            {
                Text = ((char)('A' + fila)).ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                TextColor = Colors.Gray,
                FontAttributes = FontAttributes.Bold,
                FontSize = 12
            };

            AsientosGrid.Add(labelFila, 0, fila);

            // asientos
            for (int col = 0; col < columnas; col++)
            {
                string id = $"{fila}-{col}";

                var btn = new Button
                {
                    Text = (col + 1).ToString(), // numeración correcta
                    FontSize = 10,
                    WidthRequest = 35,
                    HeightRequest = 35,
                    CornerRadius = 5
                };

                int r = random.Next(100);

                if (r < 30)
                {
                    // ocupado
                    btn.BackgroundColor = Colors.Gray;
                    btn.IsEnabled = false;
                }
                else
                {
                    // disponible
                    btn.BackgroundColor = Colors.Green;
                    btn.Clicked += (s, e) => ToggleAsiento(btn, id);
                }

                // col + 1 (porque 0 es la letra)
                AsientosGrid.Add(btn, col + 1, fila);
            }
        }
    }

    void ToggleAsiento(Button btn, string id)
    {
        if (seleccionados.Contains(id))
        {
            seleccionados.Remove(id);
            btn.BackgroundColor = Colors.Green;
        }
        else
        {
            seleccionados.Add(id);
            btn.BackgroundColor = Colors.Blue;
        }

        ActualizarResumen();
    }

    void ActualizarResumen()
    {
        int cantidad = seleccionados.Count;
        int total = cantidad * PRECIO;

        ResumenLabel.Text = $"Asientos: {cantidad}  Total: ₡{total}";
        BtnComprar.IsEnabled = cantidad > 0;
    }

    async void OnComprarClicked(object sender, EventArgs e)
    {
        if (seleccionados.Count == 0)
        {
            await DisplayAlert("Error", "Selecciona asientos", "OK");
            return;
        }

        try
        {
            BtnComprar.IsEnabled = false;
            BtnComprar.Text = "Procesando...";

            // Obtener datos guardados en Preferences
            string cedulaUsuario = Preferences.Get("usuarioCedula", "");
            int cineId = Preferences.Get("cineId", 0);
            string nombreCine = Preferences.Get("cineNombre", "");
            int peliculaId = Preferences.Get("peliculaId", 0);
            string nombrePelicula = Preferences.Get("peliculaNombre", "");
            int proyeccionId = Preferences.Get("proyeccionId", 0);
            string horaProyeccion = Preferences.Get("horaProyeccion", "");

            // Convertir asientos de "fila-col" a formato legible (A1, A2, etc)
            var asientosFormateados = seleccionados
                .Select(asiento =>
                {
                    var partes = asiento.Split('-');
                    if (partes.Length == 2 && 
                        int.TryParse(partes[0], out int fila) && 
                        int.TryParse(partes[1], out int col))
                    {
                        string letraFila = ((char)('A' + fila)).ToString();
                        return $"{letraFila}{col + 1}";
                    }
                    return asiento;
                })
                .ToList();

            decimal totalPago = seleccionados.Count * PRECIO;

            // Crear la reserva
            var reserva = new Reserva
            {
                CedulaUsuario = cedulaUsuario,
                CineId = cineId,
                NombreCine = nombreCine,
                PeliculaId = peliculaId,
                NombrePelicula = nombrePelicula,
                ProyeccionId = proyeccionId,
                HoraProyeccion = horaProyeccion,
                Asientos = asientosFormateados,
                CantidadAsientos = seleccionados.Count,
                PrecioUnitario = PRECIO,
                TotalPago = totalPago,
                Estado = "Pendiente"
            };

            // Guardar reserva
            var reservaGuardada = _reservaService.GuardarReserva(reserva);

            // Guardar el número de factura en Preferences para pasarlo a la siguiente página
            Preferences.Set("numeroFactura", reservaGuardada.NumeroFactura);
            Preferences.Set("totalPago", (int)totalPago);
            Preferences.Set("asientosSeleccionados", string.Join(",", asientosFormateados));

            // Navegar a la página de resumen
            await Shell.Current.GoToAsync("ResumenPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al procesar la compra: {ex.Message}", "OK");
        }
        finally
        {
            BtnComprar.IsEnabled = true;
            BtnComprar.Text = "Comprar";
        }
    }
}