using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Model
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente(int agencia, int numero)
        {
            Agencia = agencia;
            Numero = numero;
        }

        public override void Movimentar()
        {
            throw new NotImplementedException();
        }
    }
}
