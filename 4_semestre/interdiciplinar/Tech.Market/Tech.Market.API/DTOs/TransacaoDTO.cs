using System.Text.Json.Serialization;
using Tech.Market.API.Entities;

namespace Tech.Market.API.DTOs
{
    public class TransacaoDTO : BaseDTO
    {
        public TransacaoDTO()
        { }
        public TransacaoDTO(ContaDTO conta, ContaDTO contaDestino, TransacaoEntity transacao)
        {
            this.IdConta = conta.Id;
            this.Conta = conta;
            this.IdContaDestino = contaDestino.Id;
            this.ContaDestino = contaDestino;
            this.CodigoOperacao = transacao.CodigoOperacao;
            this.Valor = transacao.Valor;

            this.Id = transacao.IdExterno;
            this.CriadoEm = transacao.CriadoEm;
            this.AtualizadoEm = transacao.AtualizadoEm;
        }

        [JsonPropertyName("idConta")]
        public Guid IdConta { get; set; }
        [JsonPropertyName("conta")]
        public ContaDTO Conta { get; set; }
        [JsonPropertyName("idContaDestino")]
        public Guid IdContaDestino { get; set; }

        [JsonPropertyName("contaDestino")]
        public ContaDTO ContaDestino { get; set; }

        [JsonPropertyName("codigoOperacao")]
        public Guid CodigoOperacao { get; set; }
        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }
    }
}
