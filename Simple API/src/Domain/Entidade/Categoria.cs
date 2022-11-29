using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
