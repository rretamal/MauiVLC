using MauiVlc.Controls;
using Microsoft.Extensions.Logging;

namespace MauiVlc
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                             .ConfigureMauiHandlers((handlers) => {
                                 handlers.AddHandler(typeof(MediaViewer), typeof(MauiVlc.Handlers.MediaViewerHandler));
                             })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
