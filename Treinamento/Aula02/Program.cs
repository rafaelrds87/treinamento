using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aula02.Data;
using Aula02.Model;
using Microsoft.SqlServer.Server;

namespace Aula02
{
    class Program
    {

        static void ImprimirConta(Conta conta, string titulo = null)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            if (!string.IsNullOrEmpty(titulo))
            {
                Console.WriteLine(titulo.ToUpper());
                Console.WriteLine("---------------------------------");
            }

            string tipoConta = string.Empty;
            if (conta.GetType() == typeof(ContaCorrente))
            {
                tipoConta = "Corrente";
            }
            else if (conta is ContaPoupanca)
            {
                tipoConta = "Poupanca";
            }
            
            Console.WriteLine($" Conta {tipoConta}: {conta.Numero}   Agencia: {conta.Agencia}");
            Console.WriteLine("---------------------------------");
        }

        static void ImprimirExtrato(Conta c)
        {
            ImprimirConta(c, "Imprimir Extrato");

            Item[] extrato = c.ObterExtrato();
            
            foreach(Item item in extrato)
            {
                Console.ForegroundColor = item.Valor < 0 ? ConsoleColor.Red : ConsoleColor.Gray; 

                /*
                if (item.Valor < 0 || item.Valor == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }*/

                Console.WriteLine(item);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("--------------------------------------");
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("  GERENCIADOR DE CONTAS ");
            Console.WriteLine("--------------------------------");

            Console.WriteLine(" 0 - Sair");
            Console.WriteLine(" 1 - Criar nova conta");
            Console.WriteLine(" 2 - Selecionar conta");
            Console.WriteLine(" 3 - Movimentar contas");
        }

        static void ExibirMenuDaConta(Conta conta)
        {
            ImprimirConta(conta, "Selecione a conta");

            Console.WriteLine("  0 - Sair");
            Console.WriteLine("  1 - Exibir Saldo");
            Console.WriteLine("  2 - Exibir Extrato");
            Console.WriteLine("  3 - Saque");
            Console.WriteLine("  4 - Deposito");
        }


        static void CriarNovaConta()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine(" NOVA CONTA CORRENTE ");
            Console.WriteLine("---------------------------------");

            int agencia, numero;
            decimal saldoInicial;

            Console.Write("Informe a agencia: ");
            agencia = int.Parse(Console.ReadLine());

            Console.Write("Informe o numero da conta: ");
            numero = int.Parse(Console.ReadLine());

            Console.Write("Informe o saldo inicial: ");
            saldoInicial = decimal.Parse(Console.ReadLine());

            var contaCorrente = new ContaCorrente(agencia, numero);
            contaCorrente.Depositar(saldoInicial);

            RepositorioDeContas.Salvar(contaCorrente);
        }

        public static Conta ObterConta()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine(" SELECIONE UMA CONTA ");
            Console.WriteLine("---------------------------------");

            Console.Write("Informe a agencia: ");
            int agencia = int.Parse(Console.ReadLine());

            Console.Write("Informe a conta: ");
            int numero = int.Parse(Console.ReadLine());

            var conta = RepositorioDeContas.Obter(x => x.Agencia == agencia && x.Numero == numero);

            return conta;
        }

        static void Sacar(Conta conta)
        {
            ImprimirConta(conta, "Saque");

            Console.Write("Informe o valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            conta.Sacar(valor);
        }

        static void Depositar(Conta conta)
        {
            ImprimirConta(conta, "Deposito");

            Console.Write("Informe o valor: ");
            var valor = decimal.Parse(Console.ReadLine());

            conta.Depositar(valor);
        }


        static RepositorioBase<Conta> RepositorioDeContas = new RepositorioBase<Conta>();

        static void Main(string[] args)
        {
            for (var i = 1; i <= 10; i++)
            {
                var conta = new ContaCorrente(1, i);
                conta.Depositar(1000m);

                RepositorioDeContas.Salvar(conta);
                
                var contaPoupanca = new ContaPoupanca(2, i);
                contaPoupanca.Depositar(1000m);

                RepositorioDeContas.Salvar(contaPoupanca);
            }
            
            int opcao;
            do
            {
                ExibirMenu();
                Console.Write("Informe a opcao: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:

                        CriarNovaConta();

                        break;

                    case 2:

                        var conta = ObterConta();

                        if (conta == null)
                        {
                            Console.WriteLine("Conta inexistente!");
                            Console.ReadKey();
                        }
                        else
                        {
                            int opcaoConta;
                            do
                            {
                                ExibirMenuDaConta(conta);

                                Console.Write("Informe a opcao: ");
                                opcaoConta = int.Parse(Console.ReadLine());
                                
                                switch (opcaoConta)
                                {
                                    case 1:
                                        
                                        // exibir saldo

                                        ImprimirConta(conta, "Saldo");
                                        Console.WriteLine("Saldo Atual: {0}", conta.Saldo.ToString("C2"));
                                        Console.ReadKey();

                                        break;

                                    case 2:

                                        // exibir extrato
                                        ImprimirExtrato(conta);
                                        Console.ReadKey();

                                        break;

                                    case 3:

                                        // saque

                                        Sacar(conta);

                                        break;

                                    case 4:

                                        // deposito

                                        Depositar(conta);

                                        break;
                                }
                            } while (opcaoConta != 0);
                        }
                        break;

                    case 3:

                        var contas = RepositorioDeContas.Obter();

                        foreach (var c in contas)
                        {
                            c.Movimentar();
                        }

                        Console.WriteLine("Contas movimentadas com sucesso!");
                        Console.ReadKey();

                        break;
                }

            } while (opcao != 0);

        }
    }
}
