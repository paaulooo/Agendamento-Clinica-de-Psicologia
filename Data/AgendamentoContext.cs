using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class AgendamentoContext : DbContext
    {
        public DbSet<AgendamentoModel> Agendamentos { get; set; }

        public AgendamentoContext(DbContextOptions<AgendamentoContext> options) : base(options) { }
    }
}
