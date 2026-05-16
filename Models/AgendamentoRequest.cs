namespace Agendamento.Models
{
    public record AgendamentoRequest(string nomePaciente, DateTime horario);

    public class AgendamentoResquest
    {
        public string NomePaciente { get; set; }

        public DateTime Horario { get; set; }

        public AgendamentoStatus Status { get; set; }
    }
}
