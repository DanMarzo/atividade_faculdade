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
            if (!request.IdConta.HasValue)
                return BadRequest(new { message = "Conta inválida." });

            Task<bool> contaExisteTask = this._contaRepository.ExistsAsync(request.IdConta.Value);
            Task<SaldoEntity?> saldoTask = this._saldoRepository.GetByContaAsync(request.IdConta.Value);

            await Task.WhenAll(contaExisteTask, saldoTask);

            if (!contaExisteTask.Result)
                return NotFound(new { message = "Conta não localizada." });

            SaldoEntity? saldo = saldoTask.Result;
            if (saldo == null)
            {
                saldo = await this._saldoRepository
                    .InsertAsync(new SaldoEntity(
                        idConta: request.IdConta.Value, 
                        valor: request.GetValorPorTipoOperacao()
                        ));
            }

            if (request.Saida && saldo.Valor < request.Valor)
                return BadRequest(new { message = "Esta conta não possui saldo suficiente" });

            if (request.Saida)
                saldo.Valor -= request.Valor;
            else
                saldo.Valor += request.Valor;

            Task<bool> updateSaldoTask = this._saldoRepository.UpdateAsync(saldo);
            Task<TransacaoEntity> novaTransacaoTask = this._transacaoRepository
                 .InsertAsync(new TransacaoEntity(idConta: request.IdConta.Value, request.Saida, request.Valor));

            await Task.WhenAll(novaTransacaoTask, updateSaldoTask);
            //Cara, isso e so um trabalho de faculdade, nao faz sentido criar um dto para response
            return Created("", novaTransacaoTask.Result);
        }
    }
}
