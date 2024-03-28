using Deacons.Hybrid.Shared.Data;
using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Services;
using Deacons.Hybrid.Web.Components;
using MudBlazor.Services;
using Syncfusion.Blazor;

namespace Deacons.Hybrid.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzE3ODM2NUAzMjM1MmUzMDJlMzBVL05EeWJFaTdaS2ROQWVYdmJLQXpiMzNpUVVtazZKZ0tibVRTbko0c3JFPQ==");

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSetting>());
            builder.Services.AddScoped<IEmailService, EmailService>();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();

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
            app.UseAntiforgery();
            app.MapBlazorHub();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
