using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RadioWave.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace RadioWave
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
                    fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold"); 
                });


            builder.Services.AddSingleton<IRadioService, RadioService>();
            builder.Services.AddSingleton<IFavoriteService, FavoriteService>();
            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddSingleton<IRadioService, RadioService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
           


            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

      

            return builder.Build();
        }
    }
}