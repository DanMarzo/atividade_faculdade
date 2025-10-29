namespace Tech.Market.API.Entities
{
    public class TransacaoEntity
    {
        public TransacaoEntity()
        {
            this.CodigoOperacao = Guid.NewGuid();
        }

        public TransacaoEntity(int idConta, bool saida, decimal valor) : this()
        {
            this.IdConta = idConta;
            this.Saida = saida;
            this.Valor = valor;
        }

        public int Id { get; set; }
        public Guid CodigoOperacao { get; set; }
        public int IdConta { get; set; }
        public bool Saida { get; set; }
        public decimal Valor { get; set; }
    }
}