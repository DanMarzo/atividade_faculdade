using Tech.Market.API.Entities;

namespace Tech.Market.API.Contracts
{
    public interface IContaRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<ContaEntity>> GetAsync(IEnumerable<int> ids);
        Task<IEnumerable<ContaEntity>> GetAsync(params Guid[] ids);
        Task<IEnumerable<ContaEntity>> GetAsync();
        Task<ContaEntity?> GetAsync(Guid id);
        Task<ContaEntity> InsertAsync(ContaEntity entity);
    }
}
