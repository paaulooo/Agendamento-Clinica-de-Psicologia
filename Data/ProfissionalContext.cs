using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class ProfissionalContext : DbContext
    {
        public DbSet<ProfissionalModel> Profissionais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=profissional.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
