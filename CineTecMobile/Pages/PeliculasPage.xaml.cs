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

        // Obtener cine seleccionado
        int cineId = Preferences.Get("cineId", 0);
        string nombreCine = Preferences.Get("cineNombre", "");

        NombreCineLabel.Text = $"Cine: {nombreCine}";

        await CargarPeliculas();
    }

    private async Task CargarPeliculas()
    {
        try
        {
            LoadingIndicator.IsRunning = true;

            using var client = new HttpClient();

            var response = await client.GetAsync("http://TU_IP:5000/api/admin/peliculas");

            var json = await response.Content.ReadAsStringAsync();

            listaPeliculas = JsonSerializer.Deserialize<List<Pelicula>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            PeliculasList.ItemsSource = listaPeliculas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar las películas", "OK");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }

    private async void OnSeleccionarClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var pelicula = button?.BindingContext as Pelicula;

        if (pelicula == null) return;

        Preferences.Set("peliculaId", pelicula.Id);
        Preferences.Set("peliculaNombre", pelicula.NombreComercial);

        await DisplayAlert("Película", pelicula.NombreComercial, "OK");

        // luego navegamos
        // await Shell.Current.GoToAsync("ProyeccionesPage");
    }
}