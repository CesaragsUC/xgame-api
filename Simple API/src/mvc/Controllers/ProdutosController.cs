using AutoMapper;
using Domain.Entidade;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;

        public ProdutosController(ICategoriaService categoriaService,
            IProdutoServices produtoServices,
            IMapper mapper)
        {
            _categoriaService = categoriaService;
            _produtoServices = produtoServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoServices.ObterTodos();

            return View(produtos);
        }

        [HttpGet]
        public async Task<IActionResult> Adicionar()
        {
            var categorias = await _categoriaService.ObterTodos();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoModel model)
        {

            var categorias = await _categoriaService.ObterTodos();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(model.ImagemUpload, imgPrefixo))
            {
                return View(model);
            }

            model.Imagem = imgPrefixo + model.ImagemUpload.FileName;

            var response = await _produtoServices.Adicionar(_mapper.Map<Produto>(model));

            if (ResponsePossuiErros(response))
                return View(model);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var produtos = await _produtoServices.ObterPorId(id);
            var categorias = await _categoriaService.ObterTodos();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            return View(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProdutoModel model)
        {

            var categorias = await _categoriaService.ObterTodos();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");

            //pegamos a imagem atual do produto
            var produtoUpdate = await _produtoServices.ObterPorId(model.Id);
            model.Imagem = produtoUpdate.Imagem;

            //caso tenha nova imagem vindo do form então queremos mudar a imagem.
            if (model.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(model.ImagemUpload, imgPrefixo))
                {
                    return View(model);
                }

                model.Imagem = imgPrefixo + model.ImagemUpload.FileName;
            }
            produtoUpdate.Valor = model.Valor;
            produtoUpdate.Nome = model.Nome;
            produtoUpdate.Imagem = model.Imagem;
            produtoUpdate.Quantidade = model.Quantidade;
            produtoUpdate.Ativo = model.Ativo;
            produtoUpdate.CategoriaId = model.CategoriaId;

            var response = await _produtoServices.Atualizar(_mapper.Map<Produto>(produtoUpdate));

            if (ResponsePossuiErros(response))
                return View(model);


            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Remover(Guid id)
        {
            var response = await _produtoServices.Remover(id);

            if (response.Errors.Mensagens.Any())
                ModelState.AddModelError(string.Empty, "Falha ao excluir produto.");

            return RedirectToAction("Index");
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo == null || arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
