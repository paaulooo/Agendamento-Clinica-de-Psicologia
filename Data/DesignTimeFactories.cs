using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agendamento.Data
{
    public class AgendamentoContextFactory : IDesignTimeDbContextFactory<AgendamentoContext>
    {
        public AgendamentoContext CreateDbContext(string[] args)
        {
            var connString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
                ?? throw new InvalidOperationException("Variável de ambiente SUPABASE_CONNECTION_STRING não definida.");
            var options = new DbContextOptionsBuilder<AgendamentoContext>()
                .UseNpgsql(connString)
                .Options;
            return new AgendamentoContext(options);
        }
    }

    public class ProfissionalContextFactory : IDesignTimeDbContextFactory<ProfissionalContext>
    {
        public ProfissionalContext CreateDbContext(string[] args)
        {
            var connString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
                ?? throw new InvalidOperationException("Variável de ambiente SUPABASE_CONNECTION_STRING não definida.");
            var options = new DbContextOptionsBuilder<ProfissionalContext>()
                .UseNpgsql(connString)
                .Options;
            return new ProfissionalContext(options);
        }
    }

    public class PacienteContextFactory : IDesignTimeDbContextFactory<PacienteContext>
    {
        public PacienteContext CreateDbContext(string[] args)
        {
            var connString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
                ?? throw new InvalidOperationException("Variável de ambiente SUPABASE_CONNECTION_STRING não definida.");
            var options = new DbContextOptionsBuilder<PacienteContext>()
                .UseNpgsql(connString)
                .Options;
            return new PacienteContext(options);
        }
    }
}
