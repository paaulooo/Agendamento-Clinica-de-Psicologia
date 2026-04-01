using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace Agendamento.Routes
{
    public static class AgendamentoRoute
    {
        public static void AgendamentoRoutes(this WebApplication app)
        {
            var route = app.MapGroup("agendamento");
            //route.MapGet("", () => new AgendamentoModel("", ""));
            route.MapPost("", async (AgendamentoRequest req, AgendamentoContext context) =>
            {
                var agendamento = new AgendamentoModel(req.nomePaciente, req.horario, AgendamentoStatus.Aguardando.ToString());
                await context.AddAsync(agendamento);
                await context.SaveChangesAsync();
            });

            route.MapGet("", async (AgendamentoContext context) =>
            {
                var agendamentos = await context.Agendamentos.ToListAsync();
                return Results.Ok(agendamentos);
            });

            route.MapPut("{id:guid}", async (Guid id, AgendamentoContext context, AgendamentoRequest req) =>
            {
                var agendamento = await context.Agendamentos.FindAsync(id);

                if (agendamento == null)
                {
                    return Results.NotFound();

                }
                else
                {
                    agendamento.Update(req.nomePaciente, req.horario);
                    await context.SaveChangesAsync();
                    return Results.Ok(agendamento);
                }
            });

            route.MapPatch("/{id:guid}/status", async (Guid id, AgendamentoContext context, [FromQuery] AgendamentoStatus status) =>
            {
                var agendamento = await context.Agendamentos.FindAsync(id);
                if (agendamento is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    agendamento.MudaStatus(status);
                    await context.SaveChangesAsync();
                    return Results.Ok(agendamento);
                }
            });

            route.MapDelete("/{id:guid}", async (Guid id, AgendamentoContext context) =>
            {
                var agendamento = await context.Agendamentos.FindAsync(id);

                if (agendamento is null)
                {
                    return Results.NotFound();

                }
                else
                {
                    agendamento.MudaStatus(AgendamentoStatus.Cancelado);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
            });

        }

        
    }
}
