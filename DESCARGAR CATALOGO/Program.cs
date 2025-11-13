using DESCARGAR_CATALOGO.Models;
using DESCARGAR_CATALOGO.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// ===== Logging =====
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// ===== Controllers =====
builder.Services.AddControllers();

// ===== EPPlus =====
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

// ===== DbContext =====
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TuDbContext>(opt =>
{
    opt.UseSqlServer(cs);
    opt.EnableDetailedErrors();
    opt.EnableSensitiveDataLogging();
});

// ===== Servicios =====
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IExcelGeneratorService, ExcelGeneratorService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CORS (YA CORREGIDO CON LAS IPs) =====
const string AllowFrontend = "AllowFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowFrontend, policy =>
        policy
            .WithOrigins(
                "http://localhost:5173",       // React (Vite)
                "http://localhost:81",         // Frontend IIS (Servidor)
                "http://localhost",            // Frontend IIS (Servidor puerto 80)
                "http://192.168.100.206:81",  // IP Frontend (Vendedor)
                "http://192.168.100.206"     // IP Frontend (Vendedor puerto 80)
            )
            .WithMethods("GET", "POST", "OPTIONS")
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            .SetPreflightMaxAge(TimeSpan.FromHours(1))
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // <-- CAMBIO PUNTO 4: Arregla descarga no segura

app.UseRouting();
app.UseCors(AllowFrontend);
app.UseAuthorization();

// Controllers (con CORS)
app.MapControllers().RequireCors(AllowFrontend);

// Endpoint de salud SIN DB (siempre disponible)
// app.MapGet("/api/Filtros/ping", () => Results.Ok("OK"))
//    .RequireCors(AllowFrontend);

app.Run();