using System.Text;

namespace DIO.Localiza.Bank.Classes
{
    public class Conta
    {
        public Conta(TipoContaEnum tipoConta, double saldo, double credito, string nome)
        {
            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
        }

        public TipoContaEnum TipoConta { get; set; }
        public double Saldo { get; set; }
        public double Credito { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            var contaStringbuilder = new StringBuilder();
            contaStringbuilder.AppendLine($"Nome: {Nome}");
            contaStringbuilder.AppendLine($"Tipo de Conta: {TipoConta}");
            contaStringbuilder.AppendLine($"Saldo: {Saldo}");
            contaStringbuilder.AppendLine($"Crédito: {Credito}");

            return contaStringbuilder.ToString();
        }
    }
}
