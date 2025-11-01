namespace Tech.Market.Web.Controllers
{
    [Route("[controller]")]
    public class TransacoesController : Controller
    {
        private readonly IContasService _contasService;
        private readonly ITransacoesService _transacoesService;
        public TransacoesController(IContasService contasService, ITransacoesService transacoesService)
        {
            _contasService = contasService;
            _transacoesService = transacoesService;
        }
        [Route("{id:guid}")]
        public async Task<IActionResult> Index([FromRoute] Guid id)
        {
            Task<ContaDTO?> contaTask = this._contasService.GetAsync(id);
            Task<IEnumerable<TransacaoDTO>> transacoesTask = this._transacoesService.GetAsync(idConta: id);

            await Task.WhenAll(contaTask, transacoesTask);

            if (contaTask.Result == null)
                return RedirectToActionPermanent(actionName: nameof(HomeController.Index), controllerName: "Home");

            return View(new TransacaoModel(contaTask.Result, transacoesTask.Result));
        }
    }
}
