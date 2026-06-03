using WomenTaxi.Client.Pages;
using WomenTaxi.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container, +(DbContext, SignalR, авторизация и т.д.)
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();


//код в сервера временные строки, для проверки
/*
var configuration = builder.Configuration;
var connString = configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"DB Connection: {connString}");

*/

var app = builder.Build();

//настройка middleware(UseRouting, UseEndpoints и т.д.)

// Configure the HTTP request pipeline, Настройка конвейера middleware
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WomenTaxi.Client._Imports).Assembly);

app.Run();
