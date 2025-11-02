"use client";
import { createCustomInput } from "@/components/input.component";
import { createCustomLabel } from "@/components/label-input.component";
import { ModalComponent } from "@/components/modal.component";
import { ContaDTO } from "@/dtos/conta.dto";
import { TransacaoRequestDTO } from "@/dtos/transacao.request.dto";
import axios, { AxiosError } from "axios";
import { useRouter } from "next/navigation";
import { ButtonHTMLAttributes, ReactNode, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { toast } from "react-toastify";

interface Props extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: ReactNode;
  contas: Array<ContaDTO>;
  contaOrigem: ContaDTO;
}

const Input = createCustomInput<TransacaoRequestDTO>();
const LabelLogin = createCustomLabel<TransacaoRequestDTO>();

const NovaTransferenciaComponent = ({
  children,
  contas,
  contaOrigem,
  ...attrs
}: Props) => {
  const [open, setOpen] = useState<boolean>(false);
  const className =
    "bg-blue-900 text-white p-2 rounded-sm flex items-center gap-1.5 hover:cursor-pointer ";

  const formSigIn = useForm();
  const router = useRouter();

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const handleFormSignIn = (request: any) => {
    console.log(request);
    axios
      .post("/api/transferencia", request)
      .then(() => {
        router.refresh();
        setOpen(false);
      })
      .catch((error) => {
        const message: string =
          error instanceof AxiosError
            ? error.response?.data.message
            : "Erro desconhedido";
        toast(message, { type: "error" });
      });
  };
  return (
    <>
      <button className={className} onClick={() => setOpen(!open)} {...attrs}>
        {children}
      </button>
      <ModalComponent
        isOpen={open}
        onClose={() => setOpen(!open)}
        title="Nova transferência"
      >
        <form
          className="flex flex-col gap-3 "
          onSubmit={formSigIn.handleSubmit(handleFormSignIn)}
        >
          <Controller
            control={formSigIn.control}
            name="idConta"
            defaultValue={contaOrigem.id}
            render={({ field }) => (
              <div className="hidden">
                <LabelLogin htmlFor="idConta" label="Destino" />
                <input {...field}></input>
              </div>
            )}
          />
          <Controller
            control={formSigIn.control}
            name="idContaDestino"
            defaultValue=""
            render={({ field }) => (
              <>
                <LabelLogin htmlFor="idContaDestino" label="Destino" />
                <select {...field} id="idContaDestino">
                  <option value="">Selecione</option>
                  {contas.map((item) => (
                    <option key={item.id} value={item.id}>
                      {item.nome} - {item.cpfMask}
                    </option>
                  ))}
                </select>
              </>
            )}
          />

          <Controller
            control={formSigIn.control}
            name="valor"
            defaultValue=""
            render={({ field, formState }) => (
              <>
                <LabelLogin htmlFor={"valor"} label={"valor"} />
                <Input
                  type="number"
                  id={"valor"}
                  {...field}
                  aria-autocomplete="none"
                  name={"valor"}
                />
              </>
            )}
          />
          <button className={`${className} flex justify-center`} type="submit">
            Enviar
          </button>
        </form>
      </ModalComponent>
    </>
  );
};

export { NovaTransferenciaComponent };
