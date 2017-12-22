using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Model
{
    public abstract class Conta : IEquatable<Conta>
    {
        public int Agencia { get; set; }

        public int Numero { get; set; }

        public decimal Saldo { get; set; }

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

        public bool Equals(Conta x, Conta y)
        {
            return x.Numero == y.Numero && x.Agencia == y.Agencia;
        }

        public int GetHashCode(Conta obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(Conta other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(_extrato, other._extrato) &&
                   Agencia == other.Agencia &&
                   Numero == other.Numero;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Conta) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_extrato != null ? _extrato.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Agencia;
                hashCode = (hashCode * 397) ^ Numero;
                hashCode = (hashCode * 397) ^ Saldo.GetHashCode();
                return hashCode;
            }
        }
    }
}
