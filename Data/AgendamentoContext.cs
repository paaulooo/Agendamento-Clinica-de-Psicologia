using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class AgendamentoContext : DbContext
    {
        public DbSet<AgendamentoModel> Agendamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=agendamento.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
