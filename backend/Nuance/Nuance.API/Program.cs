using Nuance.API.Services.Interfaces;
using Nuance.API.Services;
using Microsoft.EntityFrameworkCore;
using Nuance.API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NuanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NuanceDatabase")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IClothingItemService, ClothingItemService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
