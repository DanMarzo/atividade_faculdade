
namespace Tech.Market.Web.Contracts
{
    public interface IContasService
    {
        Task<IEnumerable<ContaDTO>> GetAsync();
        Task<ContaDTO?> GetAsync(Guid id);
    }
}
