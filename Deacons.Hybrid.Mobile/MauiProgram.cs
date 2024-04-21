using Syncfusion.Maui.Core.Hosting;
using Deacons.Hybrid.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Shared.Services;
using Azure.Storage.Blobs;
using Microsoft.Maui.Controls;
using MudBlazor.Services;
using DevExpress.Maui;
using Deacons.Hybrid.Shared;
using Deacons.Hybrid.Shared.Utility;
using DevExpress.Maui.Core;
using DevExpress.Blazor;
using Theme = DevExpress.Maui.Core.Theme;
using Syncfusion.Blazor;
using CommunityToolkit.Maui;

namespace Deacons.Hybrid.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RVQ2dfUUN1XUc=");
           // ThemeManager.UseAndroidSystemColor = false;
           // ThemeManager.Theme = new Theme(ThemeSeedColor.DarkGreen);
            var builder = MauiApp.CreateBuilder();
            
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()            
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("opensans-regular.ttf", "OpenSansRegular");
                    fonts.AddFont("univia-pro-regular.ttf", "Univia-Pro");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                     fonts.AddFont("fa_solid.ttf", "FontAwesome");

                });

            builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Deacons.Hybrid.Mobile.appsettings.json");

            var config = new ConfigurationBuilder()
                     .AddJsonStream(stream)
                     .Build();

            builder.Configuration.AddConfiguration(config);
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton<IDapperContrib, DapperContrib>();
            builder.Services.AddSingleton<ICalendarEventsService, CalendarEventsService>();
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://deaconapidev.myworkatcornerstone.com/") });
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
             var app = builder.Build();

            UtilityService.Initialize(app.Services);

            return app;
        }
    }
}
