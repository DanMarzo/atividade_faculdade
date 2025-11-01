using Tech.Market.Core.Entities;

namespace Tech.Market.API.Contracts
{
    public interface ITransacaoRepository
    {
        Task<TransacaoEntity> InsertAsync(TransacaoEntity entity);
        Task<IEnumerable<TransacaoEntity>> GetAsync(int? idConta = null);
    }
}
