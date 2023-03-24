using System.Net.Http;

namespace TTCompanion.GUI.FantasyFootball.MAUI
{
    public static class MauiProgram
    {
        public static HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7295") };

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

            return builder.Build();
        }
    }
}