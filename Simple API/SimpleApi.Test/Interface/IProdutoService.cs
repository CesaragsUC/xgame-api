using Domain.Entidade;
using FluentValidation.Results;

namespace SimpleApi.Test.Interface
{
    public interface IProdutoService
    {
        Task Adicionar(Produto produto);
        Task Remove(Guid id);
        Task Atualizar(Produto produto);
    }
}
