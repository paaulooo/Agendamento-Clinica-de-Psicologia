using Agendamento.Data;
using Agendamento.Models;
using Microsoft.EntityFrameworkCore;


namespace Agendamento.Routes
{
    public static class ProfissionalRoute
    {
        public static void ProfissionalRoutes(this WebApplication app ) 
        { 
            var route = app.MapGroup("profissional");

            //Cria um profissional no sistema

            route.MapPost("", async (ProfissionalRequest req, ProfissionalContext context) =>
                {
                    var profissional = new ProfissionalModel(req.nomeProfissional, req.crm);
                    await context.AddAsync(profissional);
                    await context.SaveChangesAsync();
                });
    
                //Mostra todos os profissionais
    
            route.MapGet("", async (ProfissionalContext context) =>
                {
                    var profissionais = await context.Profissionais.ToListAsync();
                    return Results.Ok(profissionais);
                });
        }
    }
}
