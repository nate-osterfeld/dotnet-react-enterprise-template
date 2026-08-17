using CapstoneApp.Integrations.DbContexts;
using CapstoneApp.Endpoints.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register Entity Framework Core with SQL Server for data persistence
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dotnet-react-db")));

builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable serving static files from wwwroot (React frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

// Map modular endpoints matching Children's architecture style
app.MapTodoEndpoints();

// Fallback routing for React Single Page Application (SPA) client-side routing
app.MapFallbackToFile("index.html");

app.Run();