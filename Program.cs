using Agendamento.Data;
using Agendamento.Routes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<AgendamentoContext>();
builder.Services.AddScoped<ProfissionalContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AgendamentoRoutes();
app.ProfissionalRoutes();
app.MapPacienteRoutes();

app.UseHttpsRedirection();

app.Run();

