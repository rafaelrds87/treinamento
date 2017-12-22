using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Data
{
    public class RepositorioBase<T>
    {
        private List<T> _dados;

        public RepositorioBase()
        {
            _dados = new List<T>();
        }

        public void Salvar(T item)
        {
            _dados.Add(item);
        }

        public IEnumerable<T> Obter()
        {
            return _dados;
        }

        public T Obter(Func<T, bool> filtro)
        {
            return _dados.FirstOrDefault(filtro);
        }

    }
}
