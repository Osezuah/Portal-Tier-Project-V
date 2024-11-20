using Microsoft.AspNetCore.Components.Authorization;
using PortalCommunications.Components;
using PortalCommunications.Components.Authorization;
using PortalCommunications.Components.Services;
using PortalCommunications.Services;

namespace PortalCommunications
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddBlazorBootstrap();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("WebAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["WebAPIBaseUrl"] ?? "https://localhost:7068/api");
            });



            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddServerSideBlazor();

            builder.Services.AddScoped<BlazorBootstrap.ModalService>();


            builder.Services.AddSingleton<CustomAuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<DeviceService>();


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


            app.Run();
        }
    }
}

//test comment