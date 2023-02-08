using Domain.Entidade;

namespace Domain.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<IEnumerable<Produto>> ObterProdutos();
    }
}
