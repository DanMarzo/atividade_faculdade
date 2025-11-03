import { InputHTMLAttributes, useState } from "react";
import { FiEye, FiEyeOff } from "react-icons/fi";
import InputMask from "@mona-health/react-input-mask";

interface ICustomInputProps<T>
  extends Omit<InputHTMLAttributes<HTMLInputElement>, "id" | "name"> {
  id: keyof T;
  name: keyof T;
}

interface ICustomInputMaksProps<T>
  extends Omit<
    InputHTMLAttributes<HTMLInputElement>,
    "id" | "name" | "children"
  > {
  id: keyof T;
  name: keyof T;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  children?: (inputProps: any) => React.ReactNode;
  mask: string | Array<string | RegExp>;
}

interface ICustomInputPasswordProps<T>
  extends Omit<InputHTMLAttributes<HTMLInputElement>, "id" | "name"> {
  id: keyof T;
  name: keyof T;
}

function createCustomInput<T>() {
  const className =
    "focus:border-primary-100 focus:outline-none hover:border-primary-100 transition-all duration-300 border-background-100 border-2 rounded-md p-1.5 ";

  const Input = ({ id, name, ...attr }: ICustomInputProps<T>) => {
    return (
      <input
        name={name.toString()}
        className={className}
        {...attr}
        id={id.toString()}
      />
    );
  };

  const InputMasked = ({ id, name, ...attr }: ICustomInputMaksProps<T>) => {
    return (
      <InputMask
        name={name.toString()}
        className={className}
        {...attr}
        id={id.toString()}
      />
    );
  };

  const Password = ({ id, name, ...attr }: ICustomInputPasswordProps<T>) => {
    const [eyeState, setEyeState] = useState(true);
    return (
      <div className="relative w-full">
        <input
          name={name.toString()}
          type={eyeState ? "password" : "text"}
          className={`w-full pr-10 ${className}`}
          id={id.toString()}
          {...attr}
        />
        <button
          type="button"
          onClick={() => setEyeState(!eyeState)}
          className="absolute inset-y-0 right-2 flex items-center text-gray-500 cursor-pointer"
        >
          {eyeState ? <FiEyeOff size={18} /> : <FiEye size={18} />}
        </button>
      </div>
    );
  };

  Input.Password = Password;
  Input.InputMasked = InputMasked;
  return Input;
}

export { createCustomInput };
