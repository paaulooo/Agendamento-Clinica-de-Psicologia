using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public static class PacienteRoutes
{
    public static void MapPacienteRoutes(this WebApplication app)
    {
        app.MapGet("/pacientes", ([FromServices] PacienteContext db) =>
        {
            return db.Pacientes.ToList();
        });

        app.MapGet("/pacientes/{id}", (int id, [FromServices] PacienteContext db) =>
        {
            var paciente = db.Pacientes.Find(id);
            return paciente is not null ? Results.Ok(paciente) : Results.NotFound();
        });

        app.MapPost("/pacientes", (Paciente paciente, [FromServices] PacienteContext db) =>
        {
            db.Pacientes.Add(paciente);
            db.SaveChanges();
            return Results.Ok(paciente);
        });

        app.MapPut("/pacientes/{id}", (int id, Paciente input, [FromServices] PacienteContext db) =>
        {
            var paciente = db.Pacientes.Find(id);
            if (paciente is null) return Results.NotFound();

            paciente.Nome = input.Nome;
            paciente.Idade = input.Idade;
            paciente.Convenio = input.Convenio;
            paciente.Telefone = input.Telefone;
            paciente.Email = input.Email;

            db.SaveChanges();
            return Results.Ok(paciente);
        });

        app.MapDelete("/pacientes/{id}", (int id, [FromServices] PacienteContext db) =>
        {
            var paciente = db.Pacientes.Find(id);
            if (paciente is null) return Results.NotFound();

            db.Pacientes.Remove(paciente);
            db.SaveChanges();

            return Results.NoContent();
        });
    }
}