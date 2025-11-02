namespace Tech.Market.Core.Entities
{
    public class ContaEntity : BaseEntity
    {
        public ContaEntity() : base() { }

        public string Nome { get; set; }
        public string Cpf { get; set; }

    }
}