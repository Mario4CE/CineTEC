using CineTecMobile.Models;
using System.Text.Json;

namespace CineTecMobile.Pages;

public partial class CinesPage : ContentPage
{
    // Lista de cines cargados desde la API
    private List<Cine> listaCines = new();

    public CinesPage()
    {
        InitializeComponent();
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

            using var client = new HttpClient();

            // Endpoint real
            var response = await client.GetAsync("http://localhost:5000/api/sucursales");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al obtener cines");

            var json = await response.Content.ReadAsStringAsync();

            // Convertir JSON a objetos
            listaCines = JsonSerializer.Deserialize<List<Cine>>(json);

            // Llenar el Picker
            CinePicker.ItemsSource = listaCines;
            CinePicker.ItemDisplayBinding = new Binding("NombreCompleto");
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = "No se pudieron cargar los cines";
            ErrorLabel.IsVisible = true;

            Console.WriteLine(ex.Message);
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
        await DisplayAlert("Cine seleccionado", cineSeleccionado.Nombre, "OK");
    }
}