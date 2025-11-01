namespace Tech.Market.Web.Models.HomeModels
{
    public class HomeViewModel
    {
        public IEnumerable<ContaDTO> Contas { get; set; }

        public HomeViewModel(IEnumerable<ContaDTO> contas)
        {
            Contas = contas;
        }
    }
}
