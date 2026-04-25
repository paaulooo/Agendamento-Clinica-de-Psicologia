using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class ProfissionalContext : DbContext
    {
        public DbSet<ProfissionalModel> Profissionais { get; set; }

        public ProfissionalContext(DbContextOptions<ProfissionalContext> options) : base(options) { }
    }
}
