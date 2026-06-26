using kursovayaWeb;
using kursovayaWeb.Models;
using kursovayaWeb.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<kursovayaWeb.Models.AuthState>();
builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("http://localhost:5043/")
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DataService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
