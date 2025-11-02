namespace Tech.Market.Core.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.IdExterno = Guid.NewGuid();
            this.CriadoEm = DateTime.Now;
            this.AtualizadoEm = DateTime.Now;
        }
        public int Id { get; set; }
        public Guid IdExterno { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
