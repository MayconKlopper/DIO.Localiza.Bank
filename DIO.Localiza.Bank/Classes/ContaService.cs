using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIO.Localiza.Bank.Classes
{
    public class ContaService
    {
        private List<Conta> contaList = new List<Conta>();

        public void Adicionar(Conta conta)
        {
            this.contaList.Add(conta);
        }

        public string Sacar(double valor, Conta conta, out bool podeSacar)
        {
            var mensagem = string.Empty;

            if ((conta.Saldo + conta.Credito) - valor < 0)
            {
                mensagem = $"Saldo da conta {conta.Nome} é Insuficiente para saque";
                podeSacar = false;
            }
            else
            {
                conta.Saldo -= valor;
                mensagem = $"Saldo atual da conta de {conta.Nome} é {conta.Saldo}";
                podeSacar = true;
            }

            return mensagem;
        }

        public string Depositar(double valor, Conta conta)
        {
            conta.Saldo += valor;

            var mensagem = $"Saldo atual da conta de {conta.Nome} é {conta.Saldo}";

            return mensagem;
        }

        public string Transferir(double valor, Conta contaSaque, Conta contaDeposito)
        {
            var mensagem = string.Empty;
            var podeSacar = false;

            mensagem = this.Sacar(valor, contaSaque, out podeSacar);

            if (podeSacar)
            {
                this.Depositar(valor, contaDeposito);
                mensagem = "Tranferência efetuada com sucesso";
            }

            return mensagem;
        }

        public string Listar()
        {
            var contasStringBuilder = new StringBuilder();

            foreach (var conta in this.contaList)
            {
                contasStringBuilder.AppendLine(conta.ToString());
            }

            return contasStringBuilder.ToString();
        }

        public Conta Buscar(string nome)
        {
            var conta = this.contaList.FirstOrDefault(x => x.Nome.Trim().ToUpper().Equals(nome.Trim().ToUpper()));

            return conta;
        }
    }
}