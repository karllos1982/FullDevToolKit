using Microsoft.AspNetCore.Components.Web;
using WebApp;

using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Bootstrap5;

using MyApp.Proxys;
using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.ServerCode;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddSingleton<IAppSettings, MyAppSettings>();
builder.Services.AddSingleton<IAuthProxyManager, AuthProxy>();
builder.Services.AddSingleton<ISystemProxyManager, SystemProxy>();
builder.Services.AddSingleton<IDataCacheProxyManager, DataCacheProxy>();
builder.Services.AddSingleton<IMyAppProxy, MyAppProxy>();

builder.Services.AddScoped<IAppControllerAsync<UserAuthenticated>, MyAppController>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Environment.WebRootPath) });

builder.Services
	.AddBlazorise(options =>
	{
		options.Immediate = true;
	})
	.AddBootstrap5Providers()
	.AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
