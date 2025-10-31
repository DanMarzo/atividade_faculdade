using Tech.Market.Core.DTOs;

namespace Tech.Market.Web.Models.HomeModels
{
    public class HomeViewModel
    {
        public IEnumerable<TransacaoDTO> Transacoes { get; set; }

        public HomeViewModel(IEnumerable<TransacaoDTO> transacoes)
        {
            Transacoes = transacoes;
        }
    }
}
