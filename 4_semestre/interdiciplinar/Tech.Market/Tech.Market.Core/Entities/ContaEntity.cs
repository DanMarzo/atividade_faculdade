using System.Text.Json.Serialization;

namespace Tech.Market.API.Entities
{
    public class ContaEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
    }
}
