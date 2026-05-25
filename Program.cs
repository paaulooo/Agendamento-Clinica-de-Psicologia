using Agendamento.Data;
using Agendamento.Routes;
using Agendamento.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AgendamentoContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<ProfissionalContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<PacienteContext>(options =>
    options.UseNpgsql(connectionString));

// cors 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


// Antes do app.UseAuthorization()
app.UseCors("AllowFrontend");




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Serve admin.html em /admin (sem extensão)
app.MapGet("/admin", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(
        Path.Combine(app.Environment.WebRootPath, "admin/admin.html"));
});

app.AgendamentoRoutes();
app.ProfissionalRoutes();
app.MapPacienteRoutes();
app.MapAgendaRoutes();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapPost("/auth/login", async (HttpContext context) =>
{
    using var reader =
        new StreamReader(context.Request.Body);

    var body =
        await reader.ReadToEndAsync();

    var req =
        JsonSerializer.Deserialize<LoginRequest>(
            body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

    if (req == null)
    {
        return Results.BadRequest("Request null");
    }

    var email =
        req.Email?.Trim().ToLower() ?? "";

    var senha =
        req.Senha?.Trim() ?? "";

    if (email == "admin@clinica.com"
       && senha == "123")
    {
        return Results.Ok(new
        {
            token = "TOKEN_TESTE"
        });
    }

    return Results.Unauthorized();
});

app.Run();

public partial class Program { }

