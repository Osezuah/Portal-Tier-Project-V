using APISeperateFiles;
using PortalCommunications.Components;
using PortalCommunications.Components.Services;

namespace PortalCommunications
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();


            // Register HttpClient as a service for AccountService
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7197/") });


            // Register AccountService as a scoped service
            builder.Services.AddScoped<AccountService>();

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

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            DeviceStatusAPI.Map(app);

           await app.RunAsync();
        }
    }
}

//test comment