using Tech.Market.Core.Entities;

namespace Tech.Market.Core.DTOs
{
    public class SaldoDTO
    {
        public SaldoDTO() { }
        public SaldoDTO(SaldoEntity entity, ContaEntity conta)
        {
            this.Id = entity.IdExterno;
            this.Valor = entity.Valor;
            this.IdConta = conta.IdExterno;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public Guid IdConta { get; set; }
    }
}
