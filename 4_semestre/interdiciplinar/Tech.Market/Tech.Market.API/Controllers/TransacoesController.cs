namespace Tech.Market.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        //Ate poderia separar, mas vai dar mo trampo
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ISaldoRepository _saldoRepository;
        private readonly IContaRepository _contaRepository;

        public TransacoesController(ITransacaoRepository transacaoRepository, ISaldoRepository saldoRepository, IContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _saldoRepository = saldoRepository;
            _contaRepository = contaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostTransacaoAsync(TransacaoRequestDTO request)
        {
            if (!request.IdConta.HasValue || !request.IdContaDestino.HasValue) return BadRequest(new { message = "Conta inválida." });

            IEnumerable<ContaEntity> contas = await this._contaRepository.GetAsync(request.IdConta.Value, request.IdContaDestino.Value);
            if (contas.Count() != 2) return NotFound(new { message = "Conta não localizada." });

            ContaEntity contaOrigem = contas.First(x => x.IdExterno == request.IdConta.Value);
            ContaEntity contaDestino = contas.First(x => x.IdExterno == request.IdContaDestino.Value);
            Task<SaldoEntity?> saldoOrigemTask = this._saldoRepository.GetByContaAsync(contaOrigem.Id);
            Task<SaldoEntity?> saldoDestinoTask = this._saldoRepository.GetByContaAsync(contaDestino.Id);

            await Task.WhenAll(saldoOrigemTask, saldoDestinoTask);

            Dictionary<int, SaldoEntity?> saldos = new Dictionary<int, SaldoEntity?>()
            {
                { contaOrigem.Id, saldoOrigemTask.Result},
                { contaDestino.Id, saldoDestinoTask.Result}
            };

            if (saldos.Any(x => x.Value == null))
            {
                foreach (var item in saldos.Where(x => x.Value == null))
                {
                    saldos[item.Key] = await this._saldoRepository.InsertAsync(new SaldoEntity{ IdConta = item.Key, Valor = 0 });
                }
            }
            if (saldos.Any(x => x.Value == null))
                return BadRequest(new { message = "Erro desconhecido." });

            if (saldos[contaOrigem.Id]!.Valor < request.Valor)
                return BadRequest(new { message = "Esta conta não possui saldo suficiente para realizar a transferencia" });

            saldos[contaDestino.Id]!.Valor += request.Valor;
            saldos[contaOrigem.Id]!.Valor -= request.Valor;

            List<Task> tasks = new List<Task>();
            tasks.AddRange(saldos.Select(x => this._saldoRepository.InsertAsync(x.Value)));
            Task<TransacaoEntity> novaTransacaoTask = this._transacaoRepository.InsertAsync(new TransacaoEntity(idConta: contaOrigem.Id, idContaDestino: contaDestino.Id, valor: request.Valor));
            tasks.Add(novaTransacaoTask);
            await Task.WhenAll(tasks);
            return Created("", new TransacaoDTO(new ContaDTO(contaOrigem), new ContaDTO(contaDestino), novaTransacaoTask.Result));
        }


        [HttpGet]
        public async Task<IActionResult> GetTransacaoAsync([FromHeader] Guid idConta)
        {
            ContaEntity? conta = await this._contaRepository.GetAsync(idConta);
            if (conta == null)
                return NotFound();
            IEnumerable<TransacaoEntity> transacoes = await this._transacaoRepository.GetAsync(conta.Id);

            List<int> ids = transacoes.DistinctBy(x => x.IdContaDestino).Select(x => x.IdContaDestino).ToList();
            ids.AddRange(transacoes.DistinctBy(x => x.IdConta).Select(x => x.IdConta));

            ids = ids.Distinct().ToList();

            IEnumerable<ContaEntity> contas = await this._contaRepository.GetAsync(ids);
            return Ok(
                transacoes.Select(x => new TransacaoDTO(
                    conta: new ContaDTO(contas.First(c => c.Id == x.IdConta)),
                    contaDestino: new ContaDTO(contas.First(c => c.Id == x.IdContaDestino)),
                    transacao: x
                    )));
        }
    }
}
