namespace Tech.Market.API.Entities
{
    public class TransacaoEntity
    {
        public TransacaoEntity()
        {
            this.CodigoOperacao = Guid.NewGuid();
        }

        public TransacaoEntity(int idConta, int idContaDestino, decimal valor) : this()
        {
            this.IdConta = idConta;
            this.IdContaDestino = idContaDestino;
            this.Valor = valor;
        }

        public int Id { get; set; }
        public Guid CodigoOperacao { get; set; }
        public int IdConta { get; set; }
        public int IdContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}