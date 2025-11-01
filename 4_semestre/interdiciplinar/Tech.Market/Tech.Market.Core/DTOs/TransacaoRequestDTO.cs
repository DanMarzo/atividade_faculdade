namespace Tech.Market.Core.DTOs
{
    public class TransacaoRequestDTO
    {
        public decimal Valor { get; set; }
        public Guid? IdConta { get; set; }
        public Guid? IdContaDestino { get; set; }
    }
}
