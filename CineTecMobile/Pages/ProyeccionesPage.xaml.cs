using System.Text.Json;

namespace CineTecMobile.Pages;

public partial class ProyeccionesPage : ContentPage
{
    private List<Proyeccion> lista = new();

    public ProyeccionesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int peliculaId = Preferences.Get("peliculaId", 0);
        int cineId = Preferences.Get("cineId", 0);
        string nombrePelicula = Preferences.Get("peliculaNombre", "");
        string nombreCine = Preferences.Get("cineNombre", "");

        InfoLabel.Text = $"{nombrePelicula} - {nombreCine}";

        await CargarProyecciones(peliculaId, cineId);
    }

    private async Task CargarProyecciones(int peliculaId, int cineId)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync("http://192.168.100.21:5000/api/admin/Proyecciones");

            var json = await response.Content.ReadAsStringAsync();

            var todas = JsonSerializer.Deserialize<List<Proyeccion>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Se filtran
            var filtradas = todas
                .Where(p =>
                    p.PeliculaId == peliculaId &&
                    p.Sala?.Sucursal?.Id == cineId
                )
                .OrderBy(p => p.Fecha)
                .ToList();

            // Formateo para UI
            foreach (var p in filtradas)
            {
                p.HoraFormateada = p.Fecha.ToString("HH:mm");
                p.FechaFormateada = p.Fecha.ToString("ddd dd MMM");
                p.SalaInfo = $"Sala: {p.Sala?.Identificador}";
            }

            ProyeccionesList.ItemsSource = filtradas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar las proyecciones", "OK");
            Console.WriteLine(ex.Message);
        }
    }

    private async void OnSeleccionarClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var proy = btn?.BindingContext as Proyeccion;

        if (proy == null) return;

        Preferences.Set("proyeccionId", proy.Id);
        Preferences.Set("horaProyeccion", proy.HoraFormateada);
        Preferences.Set("fechaProyeccion", proy.FechaFormateada);
        Preferences.Set("salaId", proy.SalaId);

        await Shell.Current.GoToAsync("AsientosPage");

        // siguiente paso
        // await Shell.Current.GoToAsync("AsientosPage");
    }
}