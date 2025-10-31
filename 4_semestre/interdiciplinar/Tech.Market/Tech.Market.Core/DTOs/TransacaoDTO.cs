using System.Text.Json.Serialization;
using Tech.Market.API.Entities;

namespace Tech.Market.Core.DTOs
{
    public class TransacaoDTO
    {
        public TransacaoDTO()
        { }
        public TransacaoDTO(ContaEntity conta, ContaEntity contaDestino, TransacaoEntity transacao)
        {
            this.IdConta = conta.Id;
            this.Conta = conta;
            this.IdContaDestino = contaDestino.Id;
            this.ContaDestino = contaDestino;
            this.CodigoOperacao = transacao.CodigoOperacao;
        }
        [JsonPropertyName("idConta")]
        public int IdConta { get; set; }
        [JsonPropertyName("conta")]
        public ContaEntity Conta { get; set; }
        [JsonPropertyName("idContaDestino")]
        public int IdContaDestino { get; set; }

        [JsonPropertyName("contaDestino")]
        public ContaEntity ContaDestino { get; set; }

        [JsonPropertyName("codigoOperacao")]
        public Guid CodigoOperacao { get; set; }
    }
}


/*
 [{"idConta":1,"conta":{"id":1,"nome":"Cleiton"},"idContaDestino":2,"contaDestino":{"id":2,"nome":"Paula"},"codigoOperacao":"60ace452-408a-4bc9-a495-6ad14acbb6c5"}]
 */