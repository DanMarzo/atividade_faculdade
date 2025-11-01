using System.Text.Json.Serialization;
using Tech.Market.Core.Entities;

namespace Tech.Market.Core.DTOs
{
    public class ContaDTO
    {
        public ContaDTO()
        {
        }

        public ContaDTO(ContaEntity entity)
        {
            this.Id = entity.IdExterno;
            this.Cpf = entity.Cpf;
            this.Nome = entity.Nome;
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }
    }
}
