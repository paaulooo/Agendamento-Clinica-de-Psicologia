using Agendamento.Data;
using Agendamento.Models;

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
                var agendamento = new AgendamentoModel(req.nomePaciente, req.horario);
                await context.AddAsync(agendamento);
                await context.SaveChangesAsync();
            });

        }


    }
}
