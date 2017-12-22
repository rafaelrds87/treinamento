using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Model
{
    public class Item
    {
        public TipoItem Tipo { get; set; }

        public DateTime Data { get; set; }

        public decimal Valor { get; set; }
        
        public decimal Saldo { get; set; }

        public override string ToString()
        {
            return $"{Tipo} - {Data.ToString("dd-MM-yyyy HH:mm")} - {Valor.ToString("C2")} - Saldo: {Saldo.ToString("C2")}";
        }
    }
}
