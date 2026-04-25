
using System.Text.Json;

namespace CineTecMobile.Pages;

public partial class PeliculasPage : ContentPage
{
    private List<Pelicula> listaPeliculas = new();

    public PeliculasPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        string nombreCine = Preferences.Get("cineNombre", "");
        NombreCineLabel.Text = $"Cine: {nombreCine}";

        await CargarPeliculas();
    }

    private async Task CargarPeliculas()
    {
        try
        {
            int cineId = Preferences.Get("cineId", 0);

            using var client = new HttpClient();

            // Usamos proyecciones
            var response = await client.GetAsync("http://localhost:5000/api/admin/Proyecciones");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var proyecciones = JsonSerializer.Deserialize<List<Proyeccion>>(json, options);

            // Filtrar por cine
            var filtradas = proyecciones
                .Where(p => p.Sala?.Sucursal?.Id == cineId)
                .ToList();

            // Sacar películas únicas
            var peliculas = filtradas
                .Select(p => p.Pelicula)
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();

            PeliculasList.ItemsSource = peliculas;

            // UX extra
            if (!peliculas.Any())
            {
                await DisplayAlert("Info", "No hay películas en este cine", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar las películas", "OK");
            Console.WriteLine(ex.Message);
        }
    }

    private async void OnSeleccionarClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var pelicula = button?.BindingContext as Pelicula;

        if (pelicula == null) return;

        Preferences.Set("peliculaId", pelicula.Id);
        Preferences.Set("peliculaNombre", pelicula.NombreComercial);

        await Shell.Current.GoToAsync("ProyeccionesPage");

        // luego navegamos
        // await Shell.Current.GoToAsync("ProyeccionesPage");
    }
}