import { LabelHTMLAttributes } from "react";

interface ICustomLabelProps<T>
  extends Omit<LabelHTMLAttributes<HTMLLabelElement>, "htmlFor"> {
  htmlFor: keyof T;
  label: string;
}
/**
 * Instanciar sempre fora da funcao de renderizacao
 * const TestComponent = () => {
      NUNCA INSTANCIAR DENTRO
      const {
        formLogin,
        getTranslation,
        handleFormLogin,
        handleStateEye,
        viewEye,
        loading,
      } = useAuthSignInViewModel();
      return <></>
    }
 * @returns
 */
function createCustomLabel<T>() {
  return function CustomLabel({
    htmlFor,
    label,
    ...attrs
  }: ICustomLabelProps<T>) {
    return (
      <label {...attrs} htmlFor={htmlFor.toString()}>
        {label}
      </label>
    );
  };
}

export { createCustomLabel };
