using Agendamento.Data;
using Agendamento.Routes;
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
        policy.WithOrigins("http://localhost:5188")
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
        Path.Combine(app.Environment.WebRootPath, "admin.html"));
});

app.AgendamentoRoutes();
app.ProfissionalRoutes();
app.MapPacienteRoutes();
app.MapAgendaRoutes();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.Run();

public partial class Program { }

