using Tech.Market.API.Entities;

namespace Tech.Market.API.DTOs
{
    public class ContaRequestDTO
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Celular { get; set; }
        public string? Telefone { get; set; }
        public DateOnly? NascEm { get; set; }

        public ContaEntity CreateConta()
        {
            return new ContaEntity()
            {
                Celular = this.Celular,
                Telefone = this.Celular,
                Cpf = this.Cpf,
                Nome = this.Nome,
                NascEm = this.NascEm.Value
            };
        }
    }
}
