using Tech.Market.API.Entities;

namespace Tech.Market.API.DTOs
{
    public class SaldoDTO : BaseDTO
    {
        public SaldoDTO() { }
        public SaldoDTO(SaldoEntity entity, ContaEntity conta)
        {
            this.Id = entity.IdExterno;
            this.Valor = entity.Valor;
            this.IdConta = conta.IdExterno;

            this.Id = entity.IdExterno;
            this.CriadoEm = entity.CriadoEm;
            this.AtualizadoEm = entity.AtualizadoEm;
        }

        public decimal Valor { get; set; }
        public Guid IdConta { get; set; }
    }
}
