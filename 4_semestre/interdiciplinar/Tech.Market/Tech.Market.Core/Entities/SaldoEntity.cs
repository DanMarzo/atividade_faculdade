namespace Tech.Market.Core.Entities
{
    public class SaldoEntity
    {
        public SaldoEntity()
        {
            this.IdExterno = Guid.NewGuid();
        }
        public SaldoEntity(decimal valor, int idConta) : this()
        {
            this.Valor = valor;
            this.IdConta = idConta;
        }

        public int Id { get; set; }
        public Guid IdExterno { get; set; }
        public decimal Valor { get; set; }
        public int IdConta { get; set; }
    }
}