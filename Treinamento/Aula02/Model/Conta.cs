using System;
using System.Collections.Generic;

namespace Aula02.Model
{
    public abstract class Conta
    {
        public int Agencia { get; set; }

        public int Numero { get; set; }

        public decimal Saldo { get; protected set; }

        protected List<Item> _extrato = new List<Item>();

        public virtual void Depositar(decimal valor)
        {
            Saldo += valor;
            
            _extrato.Add(new Item()
            {
                Tipo = TipoItem.Deposito,
                Data = DateTime.Now,
                Valor = valor,
                Saldo = this.Saldo
            });
        }

        public virtual void Sacar(decimal valor)
        {
            Saldo -= valor;

            _extrato.Add(new Item()
            {
                Tipo = TipoItem.Saque,
                Data = DateTime.Now,
                Valor = valor * -1,
                Saldo = this.Saldo
            });
        }

        public abstract void Movimentar();

        public Item[] ObterExtrato()
        {
            return _extrato.ToArray();
        }
    }
}
