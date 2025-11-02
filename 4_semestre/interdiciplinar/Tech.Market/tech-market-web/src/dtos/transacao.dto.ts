import { ContaDTO } from "./conta.dto"

class TransacaoDTO {
    idConta: string
    conta: ContaDTO

    idContaDestino: string
    contaDestino: ContaDTO

    codigoOperacao: string
    valor: number
}
export { TransacaoDTO }