using AlfabetizaFeso.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext com PostgreSQL
builder.Services.AddDbContext<AlfabetizaContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar dependências (Repository e Service)
builder.Services.AddScoped<AlfabetizaFeso.Api.Repositories.IEducadorRepository, AlfabetizaFeso.Api.Repositories.EducadorRepository>();
builder.Services.AddScoped<AlfabetizaFeso.Api.Services.IEducadorService, AlfabetizaFeso.Api.Services.EducadorService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
