using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Data
{
    public class PacienteContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }

        public PacienteContext(DbContextOptions<PacienteContext> options) : base(options) { }
    }
}
