import { ContaDTO } from "@/dtos/conta.dto";
import { ContaService } from "@/services/conta.service";

class HomeServerViewModel {
    private _contaService: ContaService;
    constructor() {
        this._contaService = new ContaService();
    }


    contas: Array<ContaDTO>;

    async getContasAsync() {
        this.contas = await this._contaService.getAsync()
    }
}
export { HomeServerViewModel };
