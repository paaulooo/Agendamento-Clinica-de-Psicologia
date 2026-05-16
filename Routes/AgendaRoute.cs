using Agendamento.Data;
using Microsoft.AspNetCore.Mvc;

public static class AgendaRoutes
{
	public static void MapAgendaRoutes(this WebApplication app)
	{
		app.MapGet("/agenda/{profissionalId}", (Guid profissionalId, [FromServices] AgendamentoContext db) =>
		{
			var agenda = db.Agendamentos
				.ToList()
				.Where(a => a.ProfissionaisDesignados.Contains(profissionalId))
				.Select(a => new
				{
					id           = a.Id,
					nomePaciente = a.NomePaciente,
					horario      = a.Horario,
					diaSemana = a.Data.DayOfWeek switch
				{
					DayOfWeek.Monday    => "Seg",
					DayOfWeek.Tuesday   => "Ter",
					DayOfWeek.Wednesday => "Qua",
					DayOfWeek.Thursday  => "Quin",
					DayOfWeek.Friday    => "Sex",
					DayOfWeek.Saturday  => "Sab",
					_                   => null
				},
					hora   = a.Data.Hour,
					status = a.Status
				});
			
			return Results.Ok(agenda);
        });
    }
}