using Application.API.Interface;
using Domain.Entidade;
using Domain.Interface;

namespace Application.API.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task Adicionar(Categoria categoria)
        {
            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remove(Guid id)
        {
            await _categoriaRepository.Remover(id);
        }
    }
}
