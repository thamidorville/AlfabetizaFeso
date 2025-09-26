using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Repositories;
using AlfabetizaFeso.Api.Repository.Classes;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services;
using AlfabetizaFeso.Api.Services.Classes;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext com PostgreSQL
builder.Services.AddDbContext<AlfabetizaContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar dependências (Repository e Service)
builder.Services.AddScoped<IEducadorRepository, EducadorRepository>();
builder.Services.AddScoped<IAulaRepository, AulaRepository>();
builder.Services.AddScoped<IEducadorService, EducadorService>();
builder.Services.AddScoped<IAulaService, AulaService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
