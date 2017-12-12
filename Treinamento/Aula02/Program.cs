using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula02.Model;

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


        static void Main(string[] args)
        {
            Conta c = new ContaCorrente(367, 3527);

            c.Depositar(100m);
            c.Sacar(30m);
            c.Depositar(150m);
            c.Sacar(10m);
            
            ImprimirExtrato(c);

            Console.ReadKey();
        }
    }
}
