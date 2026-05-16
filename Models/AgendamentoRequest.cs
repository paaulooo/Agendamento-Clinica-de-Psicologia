namespace Agendamento.Models
{
    public record AgendamentoRequest(
        string nomePaciente,
        DateTime horario,
        List<Guid>? profissionaisDesignados = null
    );
}
