namespace Tech.Market.API.Entities
{
    public class SaldoEntity
    {
        public SaldoEntity() { }
        public SaldoEntity(decimal valor, int idConta)
        {
            this.Valor = valor;
            this.IdConta = idConta;
        }

        public int Id { get; set; }
        public decimal Valor { get; set; }
        public int IdConta { get; set; }
    }
}