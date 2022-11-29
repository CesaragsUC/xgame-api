using Domain.Entidade;

namespace Application.API.Interface
{
    public interface ICategoriaService
    {
        Task Adicionar(Categoria categoria);
        Task Remove(Guid id);
        Task Atualizar(Categoria categoria);
    }
}
