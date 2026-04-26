using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Agendamento.Tests;

public class SeedTestsFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        // Localiza o diretório raiz do projeto principal (contém appsettings.Development.json)
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "Agendamento.csproj")))
            dir = dir.Parent;

        if (dir != null)
            builder.UseContentRoot(dir.FullName);
    }
}

public class SeedTests : IClassFixture<SeedTestsFactory>
{
    private readonly HttpClient _client;

    private static readonly string[] PrimeirosNomes =
        { "João", "Maria", "Pedro", "Ana", "Carlos", "Fernanda", "Luiz", "Juliana", "Rafael", "Patrícia" };

    private static readonly string[] PrimeirosNomesAscii =
        { "joao", "maria", "pedro", "ana", "carlos", "fernanda", "luiz", "juliana", "rafael", "patricia" };

    private static readonly string[] Sobrenomes =
        { "Silva", "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Alves", "Pereira", "Lima", "Gomes" };

    private static readonly string[] Convenios =
        { "Unimed", "SulAmérica", "Bradesco Saúde", "Amil", "Hapvida" };

    public SeedTests(SeedTestsFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Criar_Cem_Pacientes()
    {
        for (int i = 0; i < 100; i++)
        {
            var paciente = new
            {
                nome      = $"{PrimeirosNomes[i / 10]} {Sobrenomes[i % 10]}",
                idade     = 18 + (i * 7 % 63),
                convenio  = Convenios[i % Convenios.Length],
                telefone  = $"(11) 9{i:D4}-{i:D4}",
                email     = $"{PrimeirosNomesAscii[i / 10]}.{Sobrenomes[i % 10].ToLower()}{i}@email.com"
            };

            var response = await _client.PostAsJsonAsync("/pacientes", paciente);

            Assert.True(
                response.IsSuccessStatusCode,
                $"Paciente {i + 1} ({paciente.nome}) — HTTP {(int)response.StatusCode} {response.StatusCode}"
            );
        }
    }

    [Fact]
    public async Task Criar_Vinte_Profissionais()
    {
        var profissionais = new[]
        {
            new { nomeProfissional = "Dr. Carlos Eduardo Almeida",     crm = "123456/SP" },
            new { nomeProfissional = "Dra. Ana Paula Mendes",          crm = "234567/RJ" },
            new { nomeProfissional = "Dr. Roberto Ferreira Costa",     crm = "345678/MG" },
            new { nomeProfissional = "Dra. Mariana Sousa Lima",        crm = "456789/RS" },
            new { nomeProfissional = "Dr. Paulo Henrique Barbosa",     crm = "567890/PR" },
            new { nomeProfissional = "Dra. Juliana Castro Neves",      crm = "678901/SC" },
            new { nomeProfissional = "Dr. Fernando Moreira Dias",      crm = "789012/BA" },
            new { nomeProfissional = "Dra. Renata Gonçalves Silva",    crm = "890123/PE" },
            new { nomeProfissional = "Dr. Marcos Vinícius Rocha",      crm = "901234/CE" },
            new { nomeProfissional = "Dra. Luciana Torres Cardoso",    crm = "012345/GO" },
            new { nomeProfissional = "Dr. André Luiz Campos",          crm = "123457/SP" },
            new { nomeProfissional = "Dra. Patricia Ramos Vieira",     crm = "234568/RJ" },
            new { nomeProfissional = "Dr. Rodrigo Pinheiro Lopes",     crm = "345679/MG" },
            new { nomeProfissional = "Dra. Camila Freitas Correia",    crm = "456790/RS" },
            new { nomeProfissional = "Dr. Gustavo Azevedo Martins",    crm = "567891/PR" },
            new { nomeProfissional = "Dra. Vanessa Ribeiro Santos",    crm = "678902/SC" },
            new { nomeProfissional = "Dr. Leonardo Cunha Farias",      crm = "789013/BA" },
            new { nomeProfissional = "Dra. Isabela Medeiros Cruz",     crm = "890124/PE" },
            new { nomeProfissional = "Dr. Thiago Nascimento Prado",    crm = "901235/CE" },
            new { nomeProfissional = "Dra. Beatriz Oliveira Monteiro", crm = "012346/GO" }
        };

        foreach (var profissional in profissionais)
        {
            var response = await _client.PostAsJsonAsync("/profissional", profissional);

            Assert.True(
                response.IsSuccessStatusCode,
                $"Profissional {profissional.nomeProfissional} — HTTP {(int)response.StatusCode} {response.StatusCode}"
            );
        }
    }

    [Fact]
    public async Task Criar_Trinta_E_Quatro_Agendamentos()
    {
        // 8 horários por dia → 34 agendamentos cobrem 5 dias úteis (05-09 maio 2026)
        int[] horasPorDia = { 8, 9, 10, 11, 14, 15, 16, 17 };
        var baseDate = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc);

        for (int i = 0; i < 34; i++)
        {
            var agendamento = new
            {
                nomePaciente = $"{PrimeirosNomes[(i * 3) % 10]} {Sobrenomes[(i * 7) % 10]}",
                horario      = baseDate
                                   .AddDays(i / horasPorDia.Length)
                                   .AddHours(horasPorDia[i % horasPorDia.Length])
            };

            var response = await _client.PostAsJsonAsync("/agendamento", agendamento);

            Assert.True(
                response.IsSuccessStatusCode,
                $"Agendamento {i + 1} ({agendamento.nomePaciente} — {agendamento.horario:dd/MM/yyyy HH:mm}) — HTTP {(int)response.StatusCode} {response.StatusCode}"
            );
        }
    }
}
