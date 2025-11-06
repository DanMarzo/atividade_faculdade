"use client";

import { createCustomInput } from "@/components/input.component";
import { createCustomLabel } from "@/components/label-input.component";
import { ModalComponent } from "@/components/modal.component";
import { ContaRequestDTO } from "@/dtos/conta.request.dto";
import axios, { AxiosError } from "axios";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { toast } from "react-toastify";

const Input = createCustomInput<ContaRequestDTO>();
const LabelLogin = createCustomLabel<ContaRequestDTO>();

const validarCPF = (cpf: string) => {
  cpf = cpf.replace(/[^\d]+/g, "");
  if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) return false;

  let soma = 0;
  let resto;

  for (let i = 1; i <= 9; i++)
    soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
  resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.substring(9, 10))) return false;

  soma = 0;
  for (let i = 1; i <= 10; i++)
    soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
  resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.substring(10, 11))) return false;

  return true;
};

const validarNascimento = (data: string) => {
  const hoje = new Date();
  const nasc = new Date(data);
  if (isNaN(nasc.getTime())) return "Data inválida";
  if (nasc > hoje) return "Data de nascimento não pode ser no futuro";
  const idade = hoje.getFullYear() - nasc.getFullYear();
  const mes = hoje.getMonth() - nasc.getMonth();
  const dia = hoje.getDate() - nasc.getDate();
  const idadeReal = mes < 0 || (mes === 0 && dia < 0) ? idade - 1 : idade;
  if (idadeReal < 18) return "É necessário ter pelo menos 18 anos";
  return true;
};

const validarTelefone = (telefone: string): boolean => {
  if (!telefone || telefone.trim() === "") return false;

  const digitos = telefone.replace(/\D/g, "");

  // Aceita 10 (fixo) ou 11 (celular)
  if (digitos.length === 10) {
    return true; // número fixo
  }

  if (digitos.length === 11) {
    // o primeiro dígito depois do DDD deve ser 9 (celular)
    const numeroLocal = digitos.substring(2);
    return numeroLocal[0] === "9";
  }

  // com código do país (55)
  if (digitos.length === 12 || digitos.length === 13) {
    if (!digitos.startsWith("55")) return false;

    const resto = digitos.substring(2);
    if (resto.length === 10) return true;
    if (resto.length === 11) return resto[2] === "9";
  }

  return false;
};
function formatarData(date: Date): string {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0"); // getMonth() é 0-based
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
}

const NovaContaComponent = () => {
  const className =
    "bg-blue-900 text-white p-2 rounded-sm flex items-center gap-1.5 hover:cursor-pointer ";

  const [open, setOpen] = useState<boolean>(false);
  const router = useRouter();

  const formCriarConta = useForm<ContaRequestDTO>({
    mode: "onSubmit",
  });

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const handleFormCriarConta = (request: any) => {
    axios
      .post("/api/contas", request)
      .then(() => {
        router.refresh();
        setOpen(false);
        toast("Conta criada com sucesso!", { type: "success" });
      })
      .catch((error) => {
        const message: string =
          error instanceof AxiosError
            ? error.response?.data.message
            : "Erro desconhecido";
        toast(message, { type: "error" });
      });
  };

  return (
    <>
      <button onClick={() => setOpen(!open)} className={className}>
        Nova conta
      </button>
      <ModalComponent
        isOpen={open}
        onClose={() => setOpen(!open)}
        title="Nova conta"
      >
        <form
          className="flex flex-col gap-3"
          onSubmit={formCriarConta.handleSubmit(handleFormCriarConta)}
        >
          <Controller
            control={formCriarConta.control}
            name="nome"
            rules={{ required: "Nome é obrigatório" }}
            defaultValue=""
            render={({ field, fieldState }) => (
              <>
                <LabelLogin htmlFor="nome" label="Nome:" />
                <Input id="nome" {...field} />
                {fieldState.error && (
                  <p className="text-red-500 text-sm">
                    {fieldState.error.message}
                  </p>
                )}
              </>
            )}
          />

          <Controller
            control={formCriarConta.control}
            name="celular"
            rules={{
              required: "Celular é obrigatório",
              validate: validarTelefone,
            }}
            defaultValue=""
            render={({ field, fieldState }) => (
              <>
                <LabelLogin htmlFor="celular" label="Celular:" />
                <Input.InputMasked
                  mask="(99) 9 9999-9999"
                  id="celular"
                  {...field}
                />
                {fieldState.error && (
                  <p className="text-red-500 text-sm">
                    {fieldState.error.message}
                  </p>
                )}
              </>
            )}
          />

          <Controller
            control={formCriarConta.control}
            name="telefone"
            rules={{
              validate: (value) => {
                return validarTelefone(value);
              },
            }}
            defaultValue=""
            render={({ field, fieldState }) => (
              <>
                <LabelLogin htmlFor="telefone" label="Telefone:" />
                <Input.InputMasked
                  mask="(99) 9999-9999"
                  id="telefone"
                  {...field}
                />
                {fieldState.error && (
                  <p className="text-red-500 text-sm">
                    {fieldState.error.message}
                  </p>
                )}
              </>
            )}
          />

          <Controller
            control={formCriarConta.control}
            name="cpf"
            rules={{
              required: "CPF é obrigatório",
              validate: (value) => validarCPF(value) || "CPF inválido",
            }}
            defaultValue=""
            render={({ field, fieldState }) => (
              <>
                <LabelLogin htmlFor="cpf" label="CPF:" />
                <Input.InputMasked mask="999.999.999-99" id="cpf" {...field} />
                {fieldState.error && (
                  <p className="text-red-500 text-sm">
                    {fieldState.error.message}
                  </p>
                )}
              </>
            )}
          />

          <Controller
            control={formCriarConta.control}
            name="nascEm"
            rules={{
              required: "Data de nascimento é obrigatória",
              validate: validarNascimento,
            }}
            defaultValue={formatarData(new Date())}
            render={({ field, fieldState }) => (
              <>
                <LabelLogin htmlFor="nascEm" label="Nascimento em:" />
                <Input type="date" id="nascEm" {...field} />
                {fieldState.error && (
                  <p className="text-red-500 text-sm">
                    {fieldState.error.message}
                  </p>
                )}
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

export { NovaContaComponent };
