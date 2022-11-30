using AutoMapper;
using Domain.Entidade;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    public class CategoriasController : BaseController
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaService categoriaService,
            IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.ObterTodos();

            return View(categorias);
        }

        [HttpGet]
        public async Task<IActionResult> Adicionar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CategoriaModel model)
        {
            var response = await _categoriaService.Adicionar(_mapper.Map<Categoria>(model));

            if (ResponsePossuiErros(response))
                return View(model);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var categoria = await _categoriaService.ObterPorId(id);
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CategoriaModel model)
        {
            var categoriaUpdate = await _categoriaService.ObterPorId(model.Id);
            categoriaUpdate.Nome = model.Nome;

            var response = await _categoriaService.Atualizar(_mapper.Map<Categoria>(categoriaUpdate));

            if (ResponsePossuiErros(response))
                return View(model);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Remover(Guid id)
        {

            var response = await _categoriaService.Remover(id);

            if (response.Errors.Mensagens.Any())
                ModelState.AddModelError(string.Empty, "Falha ao remover categoria.");

            return RedirectToAction("Index");
        }
    }
}
