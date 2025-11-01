namespace Tech.Market.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransacoesService _transacoesService;
        private readonly IContasService _contasService;

        public HomeController(ILogger<HomeController> logger, ITransacoesService transacoesService, IContasService contasService)
        {
            _logger = logger;
            _transacoesService = transacoesService;
            _contasService = contasService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ContaDTO> contas = await this._contasService.GetAsync();
            return View(new HomeViewModel(contas));
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
