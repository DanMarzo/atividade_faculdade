namespace Tech.Market.API.Contracts
{
    public interface ITransacaoRepository
    {
        Task<TransacaoEntity> InsertAsync(TransacaoEntity entity);
        Task<IEnumerable<TransacaoEntity>> GetAsync(IEnumerable<int>? idsContas = null);
    }
}
