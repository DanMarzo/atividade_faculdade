import { ContaDTO } from "@/dtos/conta.dto";
import { httpServer } from "@/utils/http.server"

class ContaService {

    async getAsync(): Promise<Array<ContaDTO>> {
        const response = await httpServer.get("api/contas")

        if (response.status == 200)
            return response.data;
        return []
    }

    async getByIdAsync(id: string): Promise<ContaDTO | null> {
        const response = await httpServer.get(`api/contas/${id}`)
        if (response.status == 200)
            return response.data;
        return null
    }
}

export { ContaService }