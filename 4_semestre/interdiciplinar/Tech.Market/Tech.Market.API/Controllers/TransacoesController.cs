using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tech.Market.API.DTOs;
using Tech.Market.API.Entities;

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

            IEnumerable<ContaEntity> contas = await this._contaRepository.GetAsync(request.IdConta.Value, request.IdContaDestino.Value);
            if (contas.Count() != 2)
                return NotFound(new { message = "Conta não localizada." });

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

            if (saldos[contaOrigem.Id]!.Valor < request.Valor)
                return BadRequest(new { message = "Esta conta não possui saldo suficiente para realizar a transferencia" });

            saldos[contaDestino.Id]!.Valor += request.Valor;
            saldos[contaOrigem.Id]!.Valor -= request.Valor;

            Task<IEnumerable<SaldoEntity>> updateSaldoTask = this._saldoRepository
                .InsertAsync(entities: saldos.Select(x => x.Value));

            Task<TransacaoEntity> novaTransacaoTask = this._transacaoRepository
                 .InsertAsync(new TransacaoEntity(idConta: contaOrigem.Id, idContaDestino: contaDestino.Id, valor: request.Valor));

            await Task.WhenAll(novaTransacaoTask, updateSaldoTask);
            //Cara, isso e so um trabalho de faculdade, nao faz sentido criar um dto para response
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
