using Domain.Entidade;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ProdutoRepository : Repository<Produto> , IProdutoRepository
    {
        public ProdutoRepository(XGamesContext db) : base(db)
        {
        }

        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await Db.Produtos
                .Include(p => p.Categoria).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            return await Db.Produtos.
                Include(p=> p.Categoria)
                .AsNoTracking().ToListAsync();
        }
    }
}
