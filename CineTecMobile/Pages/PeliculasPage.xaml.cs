using System.Text.Json;
using CineTecMobile.Services;
using CineTecMobile.Models;

namespace CineTecMobile.Pages;

public partial class PeliculasPage : ContentPage
{
    private List<Pelicula> listaPeliculas = new();
    private readonly ApiService _apiService;

    public PeliculasPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
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

            // Obtener películas del endpoint correcto
            var peliculas = await _apiService.GetAsync<List<Pelicula>>("/admin/peliculas");

            if (peliculas == null || peliculas.Count == 0)
            {
                PeliculasList.ItemsSource = new List<Pelicula>();
                await DisplayAlert("Info", "No hay películas disponibles", "OK");
                return;
            }

            listaPeliculas = peliculas;
            PeliculasList.ItemsSource = peliculas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las películas: {ex.Message}", "OK");
            Console.WriteLine($"Error detallado: {ex}");
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
    }
}