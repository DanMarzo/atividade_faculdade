using Tech.Market.API.DTOs;
using Tech.Market.API.Entities;
using Tech.Market.API.Utils;

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
            SaldoEntity? saldo = await this._saldoRepository.GetByContaAsync(conta.Id);
            return Ok(new ContaDTO(conta, saldo));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ContaRequestDTO request)
        {
            if (!ValidatorsUtils.TelefoneValido(request.Celular) || !ValidatorsUtils.TelefoneValido(request.Telefone))
                return BadRequest(new { message = "Telefone inválido." });

            if (!ValidatorsUtils.CpfValido(request.Cpf))
                return BadRequest(new { message = "CPF inválido." });

            if (!ValidatorsUtils.DataNascimentoValida(request.NascEm))
                return BadRequest(new { message = "Data nascimento inválida." });

            if (string.IsNullOrEmpty(request.Nome))
                return BadRequest(new { message = "Nome inválido." });

            bool cpfUsado = await this._contaRepository.ExistsCPFAsync(request.Cpf);

            if (cpfUsado)
                return Conflict(new { message = "CPF já usado." });

            ContaEntity conta = await this._contaRepository.InsertAsync(request.CreateConta());
            return Created("", new ContaDTO(conta));
        }
    }
}
