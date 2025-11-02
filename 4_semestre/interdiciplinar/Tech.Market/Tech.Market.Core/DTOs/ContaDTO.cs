using System.Text.Json.Serialization;
using Tech.Market.Core.Entities;

namespace Tech.Market.Core.DTOs
{
    public class ContaDTO : BaseDTO
    {
        public ContaDTO()
        {
        }
        public ContaDTO(ContaEntity entity)
        {
            this.Cpf = entity.Cpf;
            this.Nome = entity.Nome;
            this.Id = entity.IdExterno;
            this.CriadoEm = entity.CriadoEm;
            this.AtualizadoEm = entity.AtualizadoEm;
            this.Celular = entity.Celular;
            this.Telefone = entity.Telefone;
            this.NascEm = entity.NascEm;
        }
        public ContaDTO(ContaEntity entity, SaldoEntity? saldoEntity) : this(entity)
        {
            if (saldoEntity != null)
                this.Saldo = new SaldoDTO(saldoEntity, entity);
        }

        public DateOnly NascEm { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }
        [JsonPropertyName("celular")]
        public string Celular { get; set; }
        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }
        [JsonPropertyName("cpfMask")]
        public string CpfMask => MaskCpfPartial(this.Cpf);

        [JsonPropertyName("saldo")]
        public SaldoDTO? Saldo { get; set; } = null;

        public string MaskCpfPartial(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return cpf;
            string masked = $"{cpf.Substring(0, 3)}.{new string('*', 3)}.{new string('*', 3)}-{cpf.Substring(9, 2)}";
            return masked;
        }

    }
}
