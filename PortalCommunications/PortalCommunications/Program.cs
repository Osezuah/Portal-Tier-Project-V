using PortalCommunications.Components;

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

            app.MapPut("/api/device-status/", async (DeviceStatus devStatus) =>
            {
                //return Results.Ok(new {message = "Device status received and updated"});
                //return Results.Ok(devStatus);
            })
            .WithName("ReceiveDeviceStatus")
            .WithTags("Device");
           await app.RunAsync();
        }
    }

    public record DeviceStatus(String device_name, String location, String status);
}

//test comment