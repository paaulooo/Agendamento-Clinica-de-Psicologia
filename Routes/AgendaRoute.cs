using Agendamento.Data;
using Microsoft.AspNetCore.Mvc;

public static class AgendaRoutes
{
    public static void MapAgendaRoutes(this WebApplication app)
    {
        app.MapGet("/agenda/{profissionalId}", (
            Guid profissionalId,
            [FromServices] AgendamentoContext db) =>
        {
            var agenda = db.Agendamentos
                .ToList()
                .Where(a => a.ProfissionaisDesignados.Contains(profissionalId))
                .Select(a => new
                {
                    paciente = a.NomePaciente,
                    status = a.Status,
                    diaSemana = a.Horario.DayOfWeek.ToString(),
                    hora = a.Horario.Hour
                });

            return Results.Ok(agenda);
        });

        app.MapPost("/agenda/{agendaId}/profissional/{profissionalId}",
        (
            Guid agendaId,
            Guid profissionalId,
            [FromServices] AgendamentoContext db
        ) =>
        {
            var agenda = db.Agendamentos.Find(agendaId);

            if (agenda == null)
                return Results.NotFound();

            agenda.AdicionarProfissional(profissionalId);

            db.SaveChanges();

            return Results.Ok(agenda);
        });
    }
}