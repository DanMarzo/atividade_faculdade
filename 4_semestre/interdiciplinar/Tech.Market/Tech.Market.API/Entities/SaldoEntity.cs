namespace Tech.Market.API.Entities
{
    public class SaldoEntity : BaseEntity
    {
        public SaldoEntity() : base() { }
        public SaldoEntity(decimal valor, int idConta) : this()
        {
            this.Valor = valor;
            this.IdConta = idConta;
        }

        public decimal Valor { get; set; }
        public int IdConta { get; set; }
    }
}