import { TransacaoDTO } from "@/dtos/transacao.dto";
import { httpServer } from "@/utils/http.server"

class TransacaoService {

    async getAsync(id: string): Promise<Array<TransacaoDTO>> {
        const response = await httpServer.get("api/transacoes", {
            headers: {
                idConta: id
            }
        })

        if (response.status == 200)
            return response.data;
        return []
    }

}

export { TransacaoService }