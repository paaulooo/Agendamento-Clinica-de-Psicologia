using System.Text.Json.Serialization;

namespace Agendamento.Models
{
    public class ProfissionalModel
    {
        [JsonConstructor]
        public ProfissionalModel(string nome, string crm)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            CRM = crm;
        }

        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public string CRM { get; private set; }
    }
}
