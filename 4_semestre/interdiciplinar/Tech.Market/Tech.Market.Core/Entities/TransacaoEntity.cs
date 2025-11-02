namespace Tech.Market.Core.Entities
{
    public class TransacaoEntity : BaseEntity
    {
        public TransacaoEntity() : base()
        {
            this.CodigoOperacao = Guid.NewGuid();
        }

        public TransacaoEntity(int idConta, int idContaDestino, decimal valor) : this()
        {
            this.IdConta = idConta;
            this.IdContaDestino = idContaDestino;
            this.Valor = valor;
        }

        public Guid CodigoOperacao { get; set; }
        public int IdConta { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}