using AeroVeloz.Web.Services.Interfaces;
using AeroVeloz.Web.Services.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar soporte para Razor Pages
builder.Services.AddRazorPages();

// 2. Configurar la URL Base de la API
builder.Services.AddHttpClient("AeroVelozApi", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5006/";
    client.BaseAddress = new Uri(baseUrl);
});

// 3. Registro de Servicios del Frontend
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFlightApiService, FlightApiService>();
builder.Services.AddScoped<IAirlineApiService, AirlineApiService>();
builder.Services.AddScoped<IAuditApiService, AuditApiService>();
builder.Services.AddScoped<ISubscriptionApiService, SubscriptionApiService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();

// 4. Configuración de Cookies para manejar la sesión del usuario (guardar JWT)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.Name = "AeroVeloz.AuthCookie";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

// Nota: Hemos eliminado las inyecciones de DbContext y capa Core (Domain/Application) 
// porque la capa de Presentación Web SOLO debe consumir la API RESTful.

var app = builder.Build();

// Configuración del Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 5. Agregar Autenticación y Autorización al Pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
