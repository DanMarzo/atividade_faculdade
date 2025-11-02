using Tech.Market.Core.DTOs;
using Tech.Market.Core.Entities;

namespace Tech.Market.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IContaRepository _contaRepository;
        private readonly ISaldoRepository _saldoRepository;

        public ContasController(IContaRepository contaRepository, ISaldoRepository saldoRepository)
        {
            _contaRepository = contaRepository;
            _saldoRepository = saldoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<ContaEntity> contas = await this._contaRepository.GetAsync();
            return Ok(contas.Select(x => new ContaDTO(x)));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            ContaEntity? conta = await this._contaRepository.GetAsync(id);
            if (conta == null)
                return NotFound();
            SaldoEntity? saldo  = await this._saldoRepository.GetByContaAsync(conta.Id);
            return Ok(new ContaDTO(conta, saldo));
        }
    }
}
