import { ContaDTO } from "@/dtos/conta.dto";
import { TransacaoDTO } from "@/dtos/transacao.dto";
import { ContaService } from "@/services/conta.service";
import { TransacaoService } from "@/services/transacao.service";

class TransacoesServerViewModel {
    private _contaService: ContaService;
    private _transacaoService: TransacaoService;
    private _idConta: string;

    constructor(idConta: string) {
        this._idConta = idConta
        this._contaService = new ContaService();
        this._transacaoService = new TransacaoService();
    }

    conta: ContaDTO | null
    transacoes: Array<TransacaoDTO>

    contas: Array<ContaDTO>

    async getContaAsync() {
        this.conta = await this._contaService.getByIdAsync(this._idConta);
    }

    async getTodasContasAsync() {
        this.contas = await this._contaService.getAsync();
    }

    async getTransacoesAsync() {
        this.transacoes = await this._transacaoService.getAsync(this._idConta);
    }
}

export { TransacoesServerViewModel }