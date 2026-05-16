using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Agendamento.Models
{
    public class AgendamentoModel
    {

        // Construtor sem parâmetros para o EF Core conseguir materializar entidades do banco
        protected AgendamentoModel() { }

        [JsonConstructor]
        public AgendamentoModel(string nomePaciente, DateTime horario, string status)
        {
            Id = Guid.NewGuid();
            NomePaciente = nomePaciente;
            Horario = horario;
            Status = status;
        }
        public Guid Id { get; init; }
        public string NomePaciente { get; private set; }
        public DateTime Horario { get; private set; }

        public List<Guid> ProfissionaisDesignados { get; private set; } = []; // Lista de profissionais associados ao agendamento

        public string Status { get; private set; } = AgendamentoStatus.Aguardando.ToString();

        public void Update(string nomePaciente, DateTime horario) // Somente em caso de atualizar nome e horario.
        {
            NomePaciente = nomePaciente;
            Horario = horario;
        }

        public void MudaStatus(AgendamentoStatus status)
        {
            Status = status.ToString();
        }

        public void SetProfissionais(IEnumerable<Guid> ids)
        {
            // Modifica a lista in-place para que o EF Core detecte a mudança corretamente
            ProfissionaisDesignados.Clear();
            ProfissionaisDesignados.AddRange(ids);
        }

        [NotMapped]
        public DateTime Data => Horario;

    }

    public enum AgendamentoStatus
    {
        Aguardando,
        Em_Atendimento,
        Realizado,
        Cancelado,
        Atendimento,
        Online,
        CLS,
        Indisponivel
    }



}
