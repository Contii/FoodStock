using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Core_Service.UI;
using Blazored.Toast;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add the root component to the app. This is the main component that will be rendered in the HTML element with id "app".
builder.RootComponents.Add<App>("#app");

// Add the HeadOutlet component to the app. This component allows for dynamic updates to the <head> element of the HTML document.
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register the HttpClient services with different base addresses for dependency injection.
builder.Services.AddHttpClient("Core-Service.API", client =>
{
    client.BaseAddress = new Uri("http://localhost:8050");
});

builder.Services.AddHttpClient("Shopping-Service.API", client =>
{
    client.BaseAddress = new Uri("http://localhost:8070");
});

builder.Services.AddHttpClient("Report-Service.API", client =>
{
    client.BaseAddress = new Uri("http://localhost:8090");
});


// Register the CategoryService, StockService, and ItemService for dependency injection.
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
