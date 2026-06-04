using Microsoft.Extensions.Options;
using Valhalla.Mvc.Services;
using Valhalla.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .Configure<VikingApiSettings>(
        builder.Configuration.GetSection(VikingApiSettings.SectionName))
    .AddHttpClient<VikingApiService>((sp, client) =>
    {
        var settings = sp.GetRequiredService<IOptions<VikingApiSettings>>().Value;
        client.BaseAddress = new Uri(settings.BaseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vikings}/{action=Index}/{id?}");

app.Run();