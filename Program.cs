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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

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

