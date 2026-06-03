using WomenTaxi.Client.Pages;
using WomenTaxi.Components;
using Microsoft.EntityFrameworkCore;
using WomenTaxi.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WomenTaxi.Services;


var builder = WebApplication.CreateBuilder(args);

// Добавляем DbContext с PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//Настройка аутентификации
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key missing")))
        };
    });

builder.Services.AddAuthorization();


// Add services to the container, +(DbContext, SignalR, авторизация и т.д.)
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//если ваш сервер сам будет ходить в сторонние API (Яндекс.Карты, 2ГИС, платежи). Без этого не получите расстояние и стоимость поездки
builder.Services.AddHttpClient();// Регистрируем фабрику HTTP-клиентов

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
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
