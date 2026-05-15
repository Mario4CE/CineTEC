using CineTecMobile.Models;
using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class CinesPage : ContentPage
{
    // Servicios inyectados
    private readonly CineService _cineService;

    // Lista de cines cargados desde la API
    private List<Cine> listaCines = new();

    public CinesPage(CineService cineService)
    {
        InitializeComponent();
        _cineService = cineService;
    }

    /// <summary>
    /// Se ejecuta cuando la página aparece en pantalla
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarCines();
    }

    /// <summary>
    /// Llama a la API para obtener los cines
    /// </summary>
    private async Task CargarCines()
    {
        try
        {
            // Mostrar loading
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            ErrorLabel.IsVisible = false;

            // Usar CineService inyectado
            listaCines = await _cineService.GetCines();

            if (listaCines == null || listaCines.Count == 0)
            {
                ErrorLabel.Text = "No hay cines disponibles en este momento";
                ErrorLabel.IsVisible = true;
                return;
            }

            // Llenar el Picker
            CinePicker.ItemsSource = listaCines;
            CinePicker.ItemDisplayBinding = new Binding("NombreCompleto");
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error al cargar cines: {ex.Message}";
            ErrorLabel.IsVisible = true;

            System.Diagnostics.Debug.WriteLine($"[CinesPage] Error: {ex}");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    /// <summary>
    /// Se ejecuta cuando el usuario presiona "Continuar"
    /// </summary>
    private async void OnContinuarClicked(object sender, EventArgs e)
    {
        var cineSeleccionado = CinePicker.SelectedItem as Cine;

        if (cineSeleccionado == null)
        {
            ErrorLabel.Text = "Selecciona un cine";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Guardar en memoria local (equivalente a localStorage)
        Preferences.Set("cineId", cineSeleccionado.Id);
        Preferences.Set("cineNombre", cineSeleccionado.Nombre);

        // Navegar a la siguiente pantalla
        await Shell.Current.GoToAsync("PeliculasPage");
    }
}