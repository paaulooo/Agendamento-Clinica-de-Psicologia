using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class ProfissionalContext : DbContext
    {
        public ProfissionalContext(DbContextOptions<ProfissionalContext> options)
            : base(options)
        {
        }

        public DbSet<ProfissionalModel> Profissionais { get; set; }
    }
}
