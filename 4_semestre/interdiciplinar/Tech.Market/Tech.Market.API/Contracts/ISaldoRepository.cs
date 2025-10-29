namespace Tech.Market.API.Contracts
{
    public interface ISaldoRepository
    {
        Task<SaldoEntity?> GetByContaAsync(int idConta);
        Task<SaldoEntity> InsertAsync(SaldoEntity entity);
        Task<bool> UpdateAsync(SaldoEntity entity);

    }
}
