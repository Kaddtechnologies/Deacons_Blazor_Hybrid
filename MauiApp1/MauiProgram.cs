using Azure.Storage.Blobs;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Deacons.Hybrid.Shared.Models;
using System.Reflection;
using Syncfusion.Blazor;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RVQ2dfUUN1XUc=");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("fa_solid.ttf", "FontAwesome");

                });
            builder.Services.AddSyncfusionBlazor();
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("MauiApp1.appsettings.json");

            var config = new ConfigurationBuilder()
                     .AddJsonStream(stream)
                     .Build();

            builder.Configuration.AddConfiguration(config);
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton<IDapperContrib, DapperContrib>();
            builder.Services.AddSingleton<ICalendarEventsService, CalendarEventsService>();
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://deaconapidev.myworkatcornerstone.com/") });
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}
