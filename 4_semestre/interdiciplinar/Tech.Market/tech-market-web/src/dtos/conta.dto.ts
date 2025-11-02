import { SaldoDTO } from "./saldo.dto";

class ContaDTO {
    id: string
    nome: string;
    cpfMask: string;
    saldo: SaldoDTO | null
}
export { ContaDTO }