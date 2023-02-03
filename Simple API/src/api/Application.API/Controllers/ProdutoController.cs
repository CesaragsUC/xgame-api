using Application.API.Interface;
using Application.API.Model.DTO;
using AutoMapper;
using Domain.Entidade;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        public readonly IMapper _mapper;

        public ProdutoController(
            IProdutoRepository produtoRepository,
            IProdutoService produtoService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _produtoRepository.ObterProdutos();
                var produtos = _mapper.Map<IEnumerable<ProdutoDTO>>(result);
                return CustomResponse(produtos);
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro.");
            }
        }

        [HttpGet]
        [Route("by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido.");

            try
            {
                var result = await _produtoRepository.ObterProdutoPorId(id);
                var produto = _mapper.Map<ProdutoDTO>(result);
                return CustomResponse(produto);
            }
            catch (Exception ex)
            {

                return BadRequest("Ocorreu um erro.");
            }
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> Add(ProdutoAddDTO model)
        {
            
            try
            {
                var produto = _mapper.Map<Produto>(model);
                await _produtoService.Adicionar(produto);
                return CustomResponse();
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro ao criar o produto.");
            }

        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Edit(ProdutoEditDTO model)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(model.Id);
            produto.Valor = model.Valor;
            produto.Nome = model.Nome;
            produto.Imagem = model.Imagem;
            produto.Quantidade = model.Quantidade;
            produto.Ativo = model.Ativo;
            produto.CategoriaId = model.CategoriaId;

            try
            {
                await _produtoService.Atualizar(produto);
                return CustomResponse();
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro ao atualizar o produto.");
            }

        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido.");

            try
            {
                await _produtoRepository.Remover(id);
                return CustomResponse("Produto excluido com sucesso!");
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro ao excluir o produto.");
            }
        }
    }
}
