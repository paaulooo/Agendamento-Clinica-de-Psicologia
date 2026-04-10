using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;

public static class AgendaRoutes
{
	public static void MapAgendaRoutes(this WebApplication app)
	{
		app.MapGet("/agenda/{profissionalId}", (int profissionalId, [FromServices] AgendamentoContext db) =>
		{
			var agenda = db.Agendamentos
				.Where(a => a.ProfissionalId == profissionalId)
				.ToList();

			return Results.Ok(agenda);
        });
    }
}
