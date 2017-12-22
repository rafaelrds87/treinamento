using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        static void ImprimirExtrato(Conta c)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("-- IMPRIMINDO EXTRATO ----------------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($" Agencia: {c.Agencia}   Conta: {c.Numero}");
            Console.WriteLine("--------------------------------------");

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
        }

        static void ExibirMenuDaConta(Conta conta)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine($" Conta: {conta.Numero}   Agencia: {conta.Agencia}");
            Console.WriteLine("---------------------------------");

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
        

        static RepositorioBase<Conta> RepositorioDeContas = new RepositorioBase<Conta>();

        static void Main(string[] args)
        {
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



                        break;
                }

            } while (opcao != 0);

        }
    }
}
