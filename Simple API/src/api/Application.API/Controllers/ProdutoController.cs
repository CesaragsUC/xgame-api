using Application.API.Interface;
using Application.API.Messages;
using Application.API.Messages.Integracao;
using Application.API.Model.DTO;
using AutoMapper;
using Domain.Entidade;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pplication.API.MessageBus;
using System.Drawing;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        public readonly IMapper _mapper;
        private readonly IMessageBus _bus;


        public ProdutoController(
            IProdutoRepository produtoRepository,
            IProdutoService produtoService,
            IMapper mapper,
            INotificador notificador,
            IMessageBus bus) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
            _bus = bus;
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
                ResponseMessage result = null;
                var produto = _mapper.Map<Produto>(model);
                await _produtoService.Adicionar(produto);
                if (OperacaoValida())
                {
                    result =  await ProdutoRegistrado(produto);

                }
              

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

        private async Task<ResponseMessage> ProdutoRegistrado(Produto produto)
        {
            var result = await _produtoRepository.ObterProdutoPorId(produto.Id);
            var model = _mapper.Map<ProdutoDTO>(result);

            var produtoRegistado = new ProdutoRegistradoIntegrationEvent(model.Id, model.Nome, model.Imagem, model.Valor,
                model.Quantidade, model.Ativo, model.CategoriaId);

            try
            {
                
                return  await _bus.RequestAsync<ProdutoRegistradoIntegrationEvent, ResponseMessage>(produtoRegistado);

            }
            catch (Exception ex)
            {

                throw ;
            }
        }
    }
}
