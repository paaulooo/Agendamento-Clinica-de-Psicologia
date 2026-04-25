using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Agendamento.Data
{
    public class AgendamentoContextFactory : IDesignTimeDbContextFactory<AgendamentoContext>
    {
        public AgendamentoContext CreateDbContext(string[] args)
        {
            var connString = DesignTimeHelper.BuildConfiguration().GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não definida.");
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
            var connString = DesignTimeHelper.BuildConfiguration().GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não definida.");
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
            var connString = DesignTimeHelper.BuildConfiguration().GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection não definida.");
            var options = new DbContextOptionsBuilder<PacienteContext>()
                .UseNpgsql(connString)
                .Options;
            return new PacienteContext(options);
        }
    }

    internal static class DesignTimeHelper
    {
        public static IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
