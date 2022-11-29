using Domain.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<IEnumerable<Produto>> ObterProdutos();
    }
}
