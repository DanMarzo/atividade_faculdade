namespace Tech.Market.API.Contracts
{
    public interface IContaRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
