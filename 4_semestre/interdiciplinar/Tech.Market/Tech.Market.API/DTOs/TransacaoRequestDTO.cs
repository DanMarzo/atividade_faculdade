namespace Tech.Market.API.DTOs
{
    public class TransacaoRequestDTO
    {
        public decimal Valor { get; set; }
        public int? IdConta { get; set; }
        public bool Saida { get; set; }

        public decimal GetValorPorTipoOperacao()
        {
            return this.Saida == true ? -this.Valor : this.Valor;
        }
    }
}
