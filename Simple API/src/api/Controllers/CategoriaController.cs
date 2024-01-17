using Application.API.Interface;
using Application.API.Model.DTO;
using AutoMapper;
using Domain.Entidade;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    
    [ApiController]
    public class CategoriaController : MainController
    {

        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        public readonly IMapper _mapper;

        public CategoriaController(
            ICategoriaRepository categoriaRepository,
            ICategoriaService categoriaService,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
  
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _categoriaRepository.ObterCategorias();
                var categorias = _mapper.Map<IEnumerable<CategoriaDTO>>(result);
                return Ok(categorias);
            }
            catch (Exception ex)
            {

                return BadRequest("Ocorreu um erro.");
            }
        }

        [HttpGet]
        [Route("by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido.");

            try
            {
                var result = await _categoriaRepository.ObterCategoriaPorId(id);
                var categoria = _mapper.Map<CategoriaDTO>(result);
                return CustomResponse(categoria);
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro.");
            }
        }

        [HttpPost]
        [Route("nova-categoria")]
        public async Task<IActionResult> Add(CategoriaAddDTO model)
        {
            var categoria = _mapper.Map<Categoria>(model);

            try
            {
                await _categoriaService.Adicionar(categoria);
                return CustomResponse();
            }
            catch (Exception ex)
            {

                return CustomResponse();
            }

        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Edit(CategoriaEditDTO model)
        {
            var categoria = await _categoriaRepository.ObterCategoriaPorId(model.Id);
            categoria.Nome = model.Nome;

            try
            {
                await _categoriaService.Atualizar(categoria);
                return CustomResponse();
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro ao atualizado a Categoria.");
            }

        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id invalido.");

            try
            {
                await _categoriaRepository.Remover(id);
                return CustomResponse("Categoria excluida com sucesso!");
            }
            catch (Exception ex)
            {

                return CustomResponse("Ocorreu um erro ao excluir a Categoria.");
            }
        }
    }
}
