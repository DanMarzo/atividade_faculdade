namespace Tech.Market.API.DTOs
{
    public class TransacaoRequestDTO
    {
        public decimal Valor { get; set; }
        public int? IdConta { get; set; }
        public int? IdContaDestino { get; set; }
    }
}
