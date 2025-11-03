import { TransacoesServerViewModel } from "@/view-models/transacoes.server.view-model";
import Link from "next/link";
import { redirect } from "next/navigation";
import { BsArrowLeft } from "react-icons/bs";
import { BsPlus } from "react-icons/bs";
import { NovaTransferenciaComponent } from "./(components)/nova-transferencia.component";

const TransacoesPage = async ({
  params,
}: {
  params: Promise<{ id: string }>;
}) => {
  const { id } = await params;
  const transacoesViewModel = new TransacoesServerViewModel(id);

  await Promise.all([
    transacoesViewModel.getContaAsync(),
    transacoesViewModel.getTransacoesAsync(),
    transacoesViewModel.getTodasContasAsync(),
  ]);

  if (transacoesViewModel.conta == null) redirect("/");

  function formatNumber(valor: number): string {
    {
      return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
      }).format(valor);
    }
  }

  return (
    <>
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 justify-center max-w-4xl mx-auto relative">
          <div className="flex justify-end col-span-2">
            <NovaTransferenciaComponent
              contaOrigem={transacoesViewModel.conta}
              contas={transacoesViewModel.contas.filter(
                (x) => x.id != transacoesViewModel.conta?.id
              )}
            >
              Nova transferência <BsPlus className="text-xl" />
            </NovaTransferenciaComponent>
          </div>
          <div>
            <div className="bg-white p-6 rounded-lg shadow relative ">
              <Link href={"/"} className="absolute right-0 top-0 p-2">
                <BsArrowLeft className="text-blue-900" />
              </Link>
              <div className="font-bold">Nome:</div>
              <div>{transacoesViewModel.conta.nome}</div>
              <hr />
              <div className="font-bold">CPF:</div>
              <div>{transacoesViewModel.conta.cpfMask}</div>
              {transacoesViewModel.conta.saldo && (
                <>
                  <hr />
                  <div className="font-bold">Saldo disponível:</div>
                  <div>
                    {formatNumber(transacoesViewModel.conta.saldo.valor)}
                  </div>
                </>
              )}
            </div>
          </div>
          <div>
            {transacoesViewModel.transacoes.map((item, index) => {
              return (
                <div
                  key={index}
                  className="bg-white p-6 rounded-lg shadow mb-2"
                >
                  <div className="font-bold">Código operação:</div>
                  <div>{item.codigoOperacao}</div>
                  <hr />
                  <div className="font-bold">Destinatário:</div>
                  <div>{item.contaDestino.nome}</div>
                  <hr />
                  <div className="font-bold">CPF Destinatário:</div>
                  <div>{item.contaDestino.cpfMask}</div>
                  <hr />
                  <div className="font-bold">Valor:</div>
                  <div
                    className={`${
                      item.valor >= 5000 ? "text-red-900 font-bold" : ""
                    }`}
                  >
                    {formatNumber(item.valor)}
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </>
  );
};

export default TransacoesPage;
