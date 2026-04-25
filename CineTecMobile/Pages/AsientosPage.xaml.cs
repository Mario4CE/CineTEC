namespace CineTecMobile.Pages;

public partial class AsientosPage : ContentPage
{
    const int PRECIO = 8000;

    HashSet<string> seleccionados = new();

    int filas = 10;
    int columnas = 12;

    public AsientosPage()
    {
        InitializeComponent();
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

        // 🔥 FILAS
        for (int i = 0; i < filas; i++)
        {
            AsientosGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
        }

        // 🔥 COLUMNAS
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
            // 🔤 LETRA (A, B, C...)
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

            // 🎫 ASIENTOS
            for (int col = 0; col < columnas; col++)
            {
                string id = $"{fila}-{col}";

                var btn = new Button
                {
                    Text = (col + 1).ToString(), // 🔥 numeración correcta
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

                // 🔥 IMPORTANTE: col + 1 (porque 0 es la letra)
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

        await DisplayAlert("Compra", "Compra realizada 🎉", "OK");
    }
}