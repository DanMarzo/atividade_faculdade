using System.Text.Json.Serialization;

namespace Tech.Market.Core.DTOs
{
    public class BaseDTO
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("criadoEm")]
        public DateTime CriadoEm { get; set; }
        [JsonPropertyName("atualizadoEm")]
        public DateTime AtualizadoEm { get; set; }
    }
}
