using Azure.Storage.Blobs;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Shared.Services;
using Deacons.Hybrid.Web.Server.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Syncfusion.Blazor;

namespace Deacons.Hybrid.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RVQ2dfUUN1XUc=");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

            builder.Services.AddSingleton<IDapperContrib, DapperContrib>();
            builder.Services.AddSingleton<ICalendarEventsService, CalendarEventsService>();

            builder.Services.AddMudServices();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUri"]) });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
