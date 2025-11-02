namespace Tech.Market.API.Entities
{
    public class ContaEntity : BaseEntity
    {
        public ContaEntity() : base() { }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public DateOnly NascEm { get; set; }
    }
}