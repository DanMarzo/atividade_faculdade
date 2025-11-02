using Tech.Market.API.Entities;

namespace Tech.Market.API.Contracts
{
    public interface ISaldoRepository
    {
        Task<SaldoEntity?> GetByContaAsync(int idConta);
        Task<IEnumerable<SaldoEntity>> InsertAsync(IEnumerable<SaldoEntity> entities);
        Task<SaldoEntity> InsertAsync(SaldoEntity entity);
        Task<bool> UpdateAsync(SaldoEntity entity);

    }
}
