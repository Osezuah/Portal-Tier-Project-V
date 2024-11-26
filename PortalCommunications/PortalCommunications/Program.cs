using APISeperateFiles;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PortalCommunications.Components;
using PortalCommunications.Components.Authorization;
using PortalCommunications.Components.Services;
using PortalCommunications.Models;

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
            builder.Services.AddDbContext<PortalDeviceContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
            });

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddServerSideBlazor();

            builder.Services.AddScoped<BlazorBootstrap.ModalService>();


            builder.Services.AddSingleton<CustomAuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();


            // Register HttpClient as a service for AccountService
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7197/") });

            builder.Services.AddScoped<ServiceAPI>();  // ServiceAPI is a service

            // Adding HttpClient (for making HTTP requests from ServiceAPI)
            builder.Services.AddHttpClient<ServiceAPI>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7197/"); // Replace with your API base URL
            });

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

            APIEndpoints.Map(app);

           await app.RunAsync();
        }
    }
}

//test comment