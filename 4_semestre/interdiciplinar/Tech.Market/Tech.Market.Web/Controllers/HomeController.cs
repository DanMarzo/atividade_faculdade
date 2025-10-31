using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Tech.Market.Core.DTOs;
using Tech.Market.Web.Constantes;
using Tech.Market.Web.Models;
using Tech.Market.Web.Models.HomeModels;

namespace Tech.Market.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int? idConta = null)
        {
            using HttpClient http = this._httpClientFactory.CreateClient(URIConts.URL_TECH);
            using HttpResponseMessage response = await http.GetAsync($"/api/transacoes?{(idConta.HasValue ? $"idsContas={idConta}" : "")}");
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<TransacaoDTO> transacoes = JsonSerializer.Deserialize<IEnumerable<TransacaoDTO>>(content) 
                    ?? Enumerable.Empty<TransacaoDTO>();
            return View(new HomeViewModel(transacoes));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
