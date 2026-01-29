using AuroraForcastMS.Services;
using AuroraForcastMS.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;

namespace AuroraForcastMS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()// Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()// After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkitMediaElement();
            // 1. Registrera din Service
            builder.Services.AddSingleton<AuroraService>();
            // 2. Registrera din ViewModel
            builder.Services.AddSingleton<MainViewModel>();
            // 3. Registrera din View
            builder.Services.AddSingleton<MainPage>();
            return builder.Build();
        }
    //        public static MauiApp CreateMauiApp()
    //        {
    //            var builder = MauiApp.CreateBuilder();
    //            builder
    //                .UseMauiApp<App>()
    //                .ConfigureFonts(fonts =>
    //                {
    //                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
    //                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    //                });
    //#if DEBUG
    //    		builder.Logging.AddDebug();
    //#endif
    //            return builder.Build();
    //        }
    }
}