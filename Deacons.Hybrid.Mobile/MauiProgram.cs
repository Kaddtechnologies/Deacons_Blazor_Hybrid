using Syncfusion.Maui.Core.Hosting;
using Deacons.Hybrid.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System.Reflection;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Shared.Services;
using Azure.Storage.Blobs;
using SampleBrowser.Maui.Base.Hosting;

namespace Deacons.Hybrid.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RVQ2dfUUN1XUc=");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureSampleBrowserBase()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                    fonts.AddFont("InputLayoutIcons.ttf", "InputLayoutIcons");

                });

            builder.Services.AddMauiBlazorWebView(); 
            
            builder.Services.AddSyncfusionBlazor();
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Deacons.Hybrid.Mobile.appsettings.json");

            var config = new ConfigurationBuilder()
                     .AddJsonStream(stream)
                     .Build();

            builder.Configuration.AddConfiguration(config);


            builder.Configuration.AddConfiguration(config);
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton<IDapperContrib, DapperContrib>();
            builder.Services.AddSingleton<ICalendarEventsService, CalendarEventsService>();
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://deaconapidev.myworkatcornerstone.com/") });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
