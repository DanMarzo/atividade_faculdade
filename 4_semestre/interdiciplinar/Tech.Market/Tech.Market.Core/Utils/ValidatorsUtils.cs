using System.Diagnostics.CodeAnalysis;

namespace Tech.Market.Core.Utils
{
    public static class ValidatorsUtils
    {
        private static string ApenasDigitos(string valueInput) =>
            valueInput == null ? string.Empty : new string(valueInput.Where(char.IsDigit).ToArray());
        public static bool PossuiOnzeDigitosCpf(string cpf)
        {
            string digitos = ApenasDigitos(cpf);
            return digitos.Length == 11;
        }

        public static bool CpfValido(string? cpf)
        {
            if(string.IsNullOrEmpty(cpf))
                return false;

            string cpfDigitos = ApenasDigitos(cpf);
            if (cpfDigitos.Length != 11) return false;

            if (cpfDigitos.Distinct().Count() == 1) return false;

            List<int> numeros = cpfDigitos.Select(c => c - '0').ToList();

            int soma1 = 0;
            for (int i = 0; i < 9; i++) soma1 += numeros[i] * (10 - i);
            int resto1 = soma1 % 11;
            int digito1 = resto1 < 2 ? 0 : 11 - resto1;
            if (numeros[9] != digito1) return false;

            int soma2 = 0;
            for (int i = 0; i < 10; i++) soma2 += numeros[i] * (11 - i);
            int resto2 = soma2 % 11;
            int digito2 = resto2 < 2 ? 0 : 11 - resto2;
            if (numeros[10] != digito2) return false;

            return true;
        }


        public static bool DataNascimentoValida(DateOnly? dataNascimento, int idadeMinima = 18, int idadeMaxima = 130)
        {
            if (dataNascimento == null)
                return false;

            DateTime hoje = DateTime.Now;
            DateTime dataNasc = dataNascimento.Value.ToDateTime(default).Date;
            if (dataNasc > hoje) return false;

            int idade = hoje.Year - dataNasc.Year;
            if (dataNasc > hoje.AddYears(-idade)) idade--;

            if (idade < idadeMinima || idade > idadeMaxima) return false;

            return true;
        }

        public static bool TelefoneValido(string? telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return false;

            string digitos = ApenasDigitos(telefone);

            if (digitos.Length == 10 || digitos.Length == 11)
            {
                string ddd = digitos.Substring(0, 2);
                if (!DddValido(ddd)) return false;

                string numeroLocal = digitos.Substring(2);
                if (numeroLocal.Length == 8) return true; // fixo
                if (numeroLocal.Length == 9) return numeroLocal[0] == '9';
                return false;
            }
            else if (digitos.Length == 12 || digitos.Length == 13)
            {
                if (!digitos.StartsWith("55")) return false;
                string resto = digitos.Substring(2);
                string ddd = resto.Substring(0, 2);
                if (!DddValido(ddd)) return false;
                string numeroLocal = resto.Substring(2);
                if (numeroLocal.Length == 8) return true;
                if (numeroLocal.Length == 9) return numeroLocal[0] == '9';
                return false;
            }

            return false;
        }

        private static bool DddValido(string ddd)
        {
            if (string.IsNullOrEmpty(ddd) || ddd.Length != 2) return false;
            if (!ddd.All(char.IsDigit)) return false;
            if (ddd[0] == '0') return false;
            return true;
        }
    }
}
