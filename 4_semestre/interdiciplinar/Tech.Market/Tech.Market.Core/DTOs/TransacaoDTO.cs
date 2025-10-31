using Tech.Market.API.Entities;

namespace Tech.Market.Core.DTOs
{
    public class TransacaoDTO
    {
        public TransacaoDTO(ContaEntity conta, ContaEntity contaDestino, TransacaoEntity transacao)
        {
            this.IdConta = conta.Id;
            this.Conta = conta;
            this.IdContaDestino = contaDestino.Id;
            this.ContaDestino = contaDestino;
            this.CodigoOperacao = transacao.CodigoOperacao;
        }

        public int IdConta { get; set; }
        public ContaEntity Conta { get; set; }
        public int IdContaDestino { get; set; }
        public ContaEntity ContaDestino { get; set; }

        public Guid CodigoOperacao { get; set; }
    }
}
