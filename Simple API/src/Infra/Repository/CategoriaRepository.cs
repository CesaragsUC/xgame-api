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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(XGamesContext db) : base(db)
        {
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid id)
        {
            return await Db.Categorias.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await Db.Categorias.AsNoTracking().ToListAsync();
        }
    }
}
