using DIO.Localiza.Bank.Classes;
using System;

namespace DIO.Localiza.Bank
{
    class Program
    {
        private static ContaService contaService = new ContaService();
        static void Main(string[] args)
        {
            OpcaoEnum opcaoUsuario;
            string mensagem = string.Empty;

            do
            {
                opcaoUsuario = ObterOpcoesUsuario();

                switch (opcaoUsuario)
                {
                    case OpcaoEnum.ListarContas:
                        mensagem = contaService.Listar();
                        Console.WriteLine(mensagem);
                        break;
                    case OpcaoEnum.InserirNovaConta:
                        var conta = NovaConta();
                        contaService.Adicionar(conta);
                        break;
                    case OpcaoEnum.Transferir:
                        Transferir();
                        break;
                    case OpcaoEnum.Sacar:
                        Console.WriteLine("Digite o valor a ser sacado");
                        var valorSacar = Convert.ToDouble(Console.ReadLine());
                        Sacar(valorSacar);
                        break;
                    case OpcaoEnum.Depositar:
                        Console.WriteLine("Digite o valor a ser Depositado");
                        var valorDepositar = Convert.ToDouble(Console.ReadLine());
                        Depositar(valorDepositar);
                        break;
                    case OpcaoEnum.LimparTela:
                        Console.Clear();
                        break;
                    case OpcaoEnum.Sair:
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            } while (opcaoUsuario != OpcaoEnum.Sair);
        }

        private static OpcaoEnum ObterOpcoesUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank ao seu dispor!!!");
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1 - Listar Contas");
            Console.WriteLine("2 - Inserir Nova Conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("6 - Limpar tela");
            Console.WriteLine("7 - Sair");
            Console.WriteLine();

            var opcaoUsuario = (OpcaoEnum)Enum.ToObject(typeof(OpcaoEnum), Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine();
            return opcaoUsuario;
        } 

        private static Conta NovaConta()
        {
            Console.WriteLine("Nova Conta");
            Console.WriteLine("Digite o Nome");
            string nome = Console.ReadLine();
            Console.WriteLine("Tipo de conta");
            Console.WriteLine("Digite 1 para pessoa fisica");
            Console.WriteLine("Digite 2 para pessoa juridica");
            TipoContaEnum tipoConta = (TipoContaEnum)Enum.ToObject(typeof(TipoContaEnum), Convert.ToInt32(Console.ReadLine()));
            Console.WriteLine("Digite o crédito");
            double credito = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Digite o saldo");
            double saldo = Convert.ToDouble(Console.ReadLine());

            Conta conta = new Conta(tipoConta, saldo, credito, nome);
            return conta;
        }

        private static bool Sacar(double valor)
        {
            var podeSacar = false;

            Console.WriteLine("Digite o nome do beneficiário para Saque");
            string nome = Console.ReadLine();

            var conta = contaService.Buscar(nome);
            string mensagem = contaService.Sacar(valor, conta, out podeSacar);
            Console.WriteLine(mensagem);

            return podeSacar;
        }

        private static void Depositar(double valor)
        {
            Console.WriteLine("Digite o nome do beneficiário para deposito");
            string nome = Console.ReadLine();

            var conta = contaService.Buscar(nome);
            string mensagem = contaService.Depositar(valor, conta);
            Console.WriteLine(mensagem);
        }

        private static void Transferir()
        {
            Console.WriteLine("Digite o valor a ser transferido");
            var valor = Convert.ToDouble(Console.ReadLine());

            if (Sacar(valor))
            {
                Depositar(valor);
            }
        }
    }
}
