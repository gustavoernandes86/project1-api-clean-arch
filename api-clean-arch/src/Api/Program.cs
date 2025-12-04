using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project1.Application.Services;
using Project1.Application.Interfaces;
using Project1.Infrastructure.Repositories;
using Microsoft.OpenApi;

var frontendOrigin = "http://localhost:5173";
var builder = WebApplication.CreateBuilder(args);

// OpenAPI nativo (.NET 9)
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevPolicy", policy =>
    {
        policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger (Swashbuckle) para UI
builder.Services.AddEndpointsApiExplorer();
// MVC / Controllers
builder.Services.AddControllers();

// DI registrations
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Mapeia o JSON nativo: /openapi/v1.json
    app.MapOpenApi();

}
app.UseCors("LocalDevPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
