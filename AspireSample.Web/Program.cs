using AspireSample.Web;
using AspireSample.Web.Components;
using AspireSample.Diagnostics;

var builder = WebApplication.CreateBuilder(args);



// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(client=> client.BaseAddress = new("http://apiservice"));

builder.Services.AddObservability("AspireSample.Web", builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
    
app.MapDefaultEndpoints();

app.MapObservability();

app.Run();
