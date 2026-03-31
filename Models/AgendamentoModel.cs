using System.Text.Json.Serialization;

namespace Agendamento.Models
{
    public class AgendamentoModel
    {
        [JsonConstructor]
        public AgendamentoModel(string nomePaciente, DateTime horario)
        {
            Id = Guid.NewGuid();
            NomePaciente = nomePaciente;
            Horario = horario;
        }
        public Guid Id { get; init; }
        public string NomePaciente { get; private set; }
        public DateTime Horario { get; private set; }
    }
}
