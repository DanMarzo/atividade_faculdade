namespace Tech.Market.Web.Contracts
{
    public interface ITransacoesService
    {
        Task<IEnumerable<TransacaoDTO>> GetAsync(Guid? idConta = null);
    }
}
