using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Orfograffiti
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Regular.ttf", "classic");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("impact.ttf", "impact");
                    fonts.AddFont("comicsans.ttf", "comicsans");
                    fonts.AddFont("timesnewroman.ttf", "timesnewroman");
                    fonts.AddFont("pixel.otf", "pixel");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
