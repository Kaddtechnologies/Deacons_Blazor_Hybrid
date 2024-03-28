using Deacons.Hybrid.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;

namespace Deacons.Hybrid.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE3ODM2NUAzMjM1MmUzMDJlMzBVL05EeWJFaTdaS2ROQWVYdmJLQXpiMzNpUVVtazZKZ0tibVRTbko0c3JFPQ==");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
