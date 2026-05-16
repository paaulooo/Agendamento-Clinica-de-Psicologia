using Agendamento.Data;
using Agendamento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
                if (req.profissionaisDesignados is { Count: > 0 })
                    agendamento.SetProfissionais(req.profissionaisDesignados);
                await context.AddAsync(agendamento);
                await context.SaveChangesAsync();
                return Results.Ok(agendamento);
            });

            // Metodos Get

            // Mostra todos os agendamentos

            route.MapGet("", async (AgendamentoContext context) =>
            {
                var agendamentos = await context.Agendamentos.ToListAsync();
                return Results.Ok(agendamentos);
            });

            // Mostra o agendamento pelo ID

            route.MapGet("{id:guid}", async (Guid id ,AgendamentoContext context) =>
            {
                var agendamento = await context.Agendamentos.FindAsync(id);
                return Results.Ok(agendamento);
            });

            // Mostra agendamento pelo nome do Paciente

            route.MapGet("{nome}", async (String nome ,AgendamentoContext context) =>
            {
                var agendamentos = await context.Agendamentos
                    .Where(a => a.NomePaciente.ToLower().Contains(nome.ToLower()))
                    .ToListAsync();
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
                    // Só atualiza profissionais se a lista for explicitamente fornecida e não vazia
                    // Lista null ou vazia = preservar a associação existente
                    if (req.profissionaisDesignados is { Count: > 0 })
                        agendamento.SetProfissionais(req.profissionaisDesignados);
                    await context.SaveChangesAsync();
                    return Results.Ok(agendamento);
                }
            });

            // Associa um profissional ao agendamento
            route.MapPatch("/{id:guid}/profissional", async (Guid id, AgendamentoContext context, [FromQuery] Guid profissionalId) =>
            {
                var agendamento = await context.Agendamentos.FindAsync(id);
                if (agendamento is null) return Results.NotFound();

                if (!agendamento.ProfissionaisDesignados.Contains(profissionalId))
                    agendamento.SetProfissionais([..agendamento.ProfissionaisDesignados, profissionalId]);

                await context.SaveChangesAsync();
                return Results.Ok(agendamento);
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
