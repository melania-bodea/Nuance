using Nuance.API.Services.Interfaces;
using Nuance.API.Services;
using Microsoft.EntityFrameworkCore;
using Nuance.API.Data;
using Nuance.API.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NuanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NuanceDatabase")));

builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<NuanceDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 4;
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IClothingItemService, ClothingItemService>();

builder.Services.AddScoped<IAuthService, AuthService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AngularDev");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
