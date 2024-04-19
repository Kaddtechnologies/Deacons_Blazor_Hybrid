using Azure.Storage.Blobs;
using Deacons.Hybrid.Shared;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Pages;
using Deacons.Hybrid.Shared.Services;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Web.Components;
using Deacons.Hybrid.Web.Components.Pages;
using Google.Api;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using Syncfusion.Blazor;


namespace Deacons.Hybrid.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RVQ2dfUUN1XUc=");
            InteractiveRenderSettings.InteractiveRenderMode = Microsoft.AspNetCore.Components.Web.RenderMode.InteractiveServer;

            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

            builder.Services.AddSingleton<IDapperContrib, DapperContrib>();
            builder.Services.AddSingleton<ICalendarEventsService, CalendarEventsService>();

            builder.Services.AddMudServices();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                //only add details when debugging
                o.DetailedErrors = true;
            }); ;
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();

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
            app.UseAntiforgery();
            //app.MapBlazorHub();
            app.MapFallbackToPage("/_Host", "/Components");
            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
            app.Run();
        }
    }
}