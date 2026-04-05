using System.Text.Json.Serialization;

namespace Agendamento.Models
{
    public class Paciente
    {
        [JsonConstructor]
        public Paciente(string nome, int idade, string convenio, string telefone, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Idade = idade;
            Convenio = convenio;
            Telefone = telefone;
            Email = email;
        }

        public Guid Id { get; init; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Convenio { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
