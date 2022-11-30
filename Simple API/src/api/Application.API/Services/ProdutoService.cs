using Application.API.Interface;
using Domain.Entidade;
using Domain.Entidade.Validacao;
using Domain.Interface;

namespace Application.API.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository,
             INotificador notificador ) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remove(Guid id)
        {
            await _produtoRepository.Remover(id);
        }
    }
}
