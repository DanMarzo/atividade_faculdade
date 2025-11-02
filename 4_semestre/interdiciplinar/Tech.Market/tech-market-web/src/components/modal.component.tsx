import React, { ReactNode } from "react";
interface ModalComponentProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children?: ReactNode;
}
const ModalComponent = ({
  isOpen,
  onClose,
  title,
  children,
}: ModalComponentProps) => {
  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 bg-black/30  flex items-center justify-center z-50"
      onClick={onClose} // fecha clicando fora
    >
      <div
        className="bg-white rounded-lg shadow-lg w-11/12 max-w-md p-6 relative transform transition-transform duration-300 scale-100"
        onClick={(e) => e.stopPropagation()} // impede fechar ao clicar dentro
      >
        {/* Botão fechar */}
        <button
          className="absolute top-2 right-2 text-gray-500 hover:text-gray-700 text-xl font-bold hover:cursor-pointer"
          onClick={onClose}
        >
          &times;
        </button>

        {/* Título */}
        {title && <h2 className="text-xl font-semibold mb-4">{title}</h2>}

        {/* Conteúdo do modal */}
        <div>{children}</div>
      </div>
    </div>
  );
};

export { ModalComponent };
