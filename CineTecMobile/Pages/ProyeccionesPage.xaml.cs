using System.Text.Json;
using CineTecMobile.Services;
using CineTecMobile.Models;

namespace CineTecMobile.Pages;

public partial class ProyeccionesPage : ContentPage
{
    private List<Proyeccion> lista = new();
    private readonly ApiService _apiService;

    public ProyeccionesPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
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
            // Obtener proyecciones del endpoint correcto
            var todas = await _apiService.GetAsync<List<Proyeccion>>("/admin/proyecciones");

            if (todas == null)
            {
                await DisplayAlert("Error", "No se pudieron cargar las proyecciones", "OK");
                return;
            }

            // Se filtran por película y cine
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

            if (!filtradas.Any())
            {
                await DisplayAlert("Info", "No hay proyecciones disponibles para esta película en este cine", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las proyecciones: {ex.Message}", "OK");
            Console.WriteLine($"Error detallado: {ex}");
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