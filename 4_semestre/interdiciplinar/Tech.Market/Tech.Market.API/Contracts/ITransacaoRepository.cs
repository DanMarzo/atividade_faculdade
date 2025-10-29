namespace Tech.Market.API.Contracts
{
    public interface ITransacaoRepository
    {
        Task<TransacaoEntity> InsertAsync(TransacaoEntity entity);
    }
}
