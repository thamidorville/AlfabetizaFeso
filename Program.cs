using AlfabetizaFeso.Api.Data;
using AlfabetizaFeso.Api.Repositories;
using AlfabetizaFeso.Api.Repository.Classes;
using AlfabetizaFeso.Api.Repository.Interfaces;
using AlfabetizaFeso.Api.Services;
using AlfabetizaFeso.Api.Services.Classes;
using AlfabetizaFeso.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext com PostgreSQL
builder.Services.AddDbContext<AlfabetizaContexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar dependências (Repository e Service)
builder.Services.AddScoped<IEducadorRepository, EducadorRepository>();
builder.Services.AddScoped<IAulaRepository, AulaRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IInscricaoRepository, InscricaoRepository>();

// Registrar dependências (Service)
builder.Services.AddScoped<IEducadorService, EducadorService>();
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IInscricaoService, InscricaoService>();


// registrar PasswordHasher para educador
builder.Services.AddScoped<IPasswordHasher<AlfabetizaFeso.Api.Models.Educador>, PasswordHasher<AlfabetizaFeso.Api.Models.Educador>>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar serviços do JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var corsPolicy = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
        policy.WithOrigins(
            "http://localhost:5173",
                "http://127.0.0.1:5173",
                "https://localhost:5173",  // Caso o frontend use HTTPS
                "https://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

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

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();   

app.MapControllers();

app.Run();
