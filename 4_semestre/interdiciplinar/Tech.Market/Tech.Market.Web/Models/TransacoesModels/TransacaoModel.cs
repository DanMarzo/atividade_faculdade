namespace Tech.Market.Web.Models.TransacoesModels
{
    public class TransacaoModel
    {
        public TransacaoModel(ContaDTO conta)
        {
            Conta = conta;
        }

        public TransacaoModel(ContaDTO conta, IEnumerable<TransacaoDTO> transacoes) : this(conta)
        {
            Transacoes = transacoes;
        }

        public ContaDTO Conta { get; set; }
        public IEnumerable<TransacaoDTO> Transacoes { get; set; }
    }
}
