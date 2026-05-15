using Microsoft.Extensions.Logging;
using CineTecMobile.Services;
using CineTecMobile.Pages;

namespace CineTecMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar servicios HTTP
            builder.Services.AddHttpClient<ApiService>();

            // Registrar servicios de aplicación
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<CineService>();
            builder.Services.AddScoped<ReservaService>();

            // Registrar páginas con inyección de dependencias
            builder.Services.AddScoped<LoginPage>();
            builder.Services.AddScoped<MenuPage>();
            builder.Services.AddScoped<CinesPage>();
            builder.Services.AddScoped<PeliculasPage>();
            builder.Services.AddScoped<ProyeccionesPage>();
            builder.Services.AddScoped<AsientosPage>();
            builder.Services.AddScoped<ResumenPage>();
            builder.Services.AddScoped<MisBoletosPage>();
            builder.Services.AddScoped<PerfilPage>();
            builder.Services.AddScoped<RegistroPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
