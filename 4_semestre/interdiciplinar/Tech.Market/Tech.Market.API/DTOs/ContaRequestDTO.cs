using Tech.Market.API.Entities;

namespace Tech.Market.API.DTOs
{
    public class ContaRequestDTO
    {
        public string? Nome { get; set; }
        private string? _cpf = string.Empty;
        public string? Cpf
        {
            get => _cpf;
            set => this._cpf = value == null? null : value.Replace(".", "").Replace("-", "");
        }
        public string? Celular { get; set; }
        public string? Telefone { get; set; }
        public DateOnly? NascEm { get; set; }

        public ContaEntity CreateConta()
        {
            return new ContaEntity()
            {
                Celular = this.Celular,
                Telefone = this.Celular,
                Cpf = this.Cpf.Replace(".", "").Replace("-", ""),
                Nome = this.Nome,
                NascEm = this.NascEm.Value
            };
        }
    }
}
