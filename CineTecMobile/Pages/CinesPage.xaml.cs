using CineTecMobile.Services;

namespace CineTecMobile.Pages;

public partial class CinesPage : ContentPage
{
    private readonly CineService _cineService;

    public CinesPage()
    {
        InitializeComponent();
        _cineService = new CineService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var cines = await _cineService.GetCines();
        ListaCines.ItemsSource = cines;
    }
}