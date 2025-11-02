namespace Tech.Market.API.DTOs
{
    public class TransacaoRequestDTO
    {
        public decimal Valor { get; set; }
        public Guid? IdConta { get; set; }
        public Guid? IdContaDestino { get; set; }
    }
}
