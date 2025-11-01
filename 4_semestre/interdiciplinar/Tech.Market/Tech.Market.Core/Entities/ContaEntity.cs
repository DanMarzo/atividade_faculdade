using System.Text.Json.Serialization;

namespace Tech.Market.Core.Entities
{
    public class ContaEntity
    {
        public int Id { get; set; }

        public Guid IdExterno { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }

    }
}