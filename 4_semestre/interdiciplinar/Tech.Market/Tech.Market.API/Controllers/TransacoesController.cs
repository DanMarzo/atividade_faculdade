using Microsoft.AspNetCore.Mvc;
using Tech.Market.API.DTOs;

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
            if (!request.IdConta.HasValue || !request.IdContaDestino.HasValue)
                return BadRequest(new { message = "Conta inválida." });

            Task<bool> contaOrigemExistTask = this._contaRepository.ExistsAsync(request.IdConta.Value);
            Task<SaldoEntity?> saldoOrigemTask = this._saldoRepository.GetByContaAsync(request.IdConta.Value);

            Task<bool> contaDestinoExisteTask = this._contaRepository.ExistsAsync(request.IdContaDestino.Value);
            Task<SaldoEntity?> saldoDestinoTask = this._saldoRepository.GetByContaAsync(request.IdContaDestino.Value);


            await Task.WhenAll(contaOrigemExistTask, saldoOrigemTask, contaDestinoExisteTask, saldoDestinoTask);

            if (!contaOrigemExistTask.Result || !contaDestinoExisteTask.Result)
                return NotFound(new { message = "Conta não localizada." });

            Dictionary<int, SaldoEntity?> saldos = new Dictionary<int, SaldoEntity?>()
            {
                { request.IdConta.Value, saldoOrigemTask.Result},
                { request.IdContaDestino.Value, saldoDestinoTask.Result}
            };

            if (saldos.Any(x => x.Value == null))
            {
                //Talvez nao seja necessario mas para evitar BO
                IEnumerable<SaldoEntity> saldosEntities = await this._saldoRepository.InsertAsync(entities: saldos.Where(x => x.Value == null).Select(x => new SaldoEntity
                {
                    IdConta = x.Key,
                    Valor = 0
                }));
                foreach (var item in saldosEntities)
                { saldos[item.IdConta] = item; }
            }
            if (saldos.Any(x => x.Value == null))
                return BadRequest(new { message = "Erro desconhecido." });

            if (saldos[request.IdConta.Value]!.Valor < request.Valor)
                return BadRequest(new { message = "Esta conta não possui saldo suficiente para realizar a transferencia" });

            saldos[request.IdContaDestino.Value]!.Valor += request.Valor;
            saldos[request.IdConta.Value]!.Valor -= request.Valor;

            Task<IEnumerable<SaldoEntity>> updateSaldoTask = this._saldoRepository.InsertAsync(entities: saldos.Select(x => x.Value));

            Task<TransacaoEntity> novaTransacaoTask = this._transacaoRepository
                 .InsertAsync(new TransacaoEntity(idConta: request.IdConta.Value, idContaDestino: request.IdContaDestino.Value, valor: request.Valor));

            await Task.WhenAll(novaTransacaoTask, updateSaldoTask);
            //Cara, isso e so um trabalho de faculdade, nao faz sentido criar um dto para response
            return Created("", novaTransacaoTask.Result);
        }
    }
}
