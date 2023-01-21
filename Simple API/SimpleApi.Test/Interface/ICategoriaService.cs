using Domain.Entidade;
using FluentValidation.Results;

namespace SimpleApi.Test.Interface
{
    public interface ICategoriaService
    {
        Task Adicionar(Categoria categoria);
        Task Remove(Guid id);
        Task Atualizar(Categoria categoria);
    }
}
