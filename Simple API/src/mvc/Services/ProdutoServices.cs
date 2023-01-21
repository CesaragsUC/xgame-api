using Domain.Entidade;
using Domain.Interface;
using Domain.Services;
using Microsoft.Extensions.Options;
using mvc.Models;
using System.Collections.Generic;

namespace mvc.Services
{

    public interface IProdutoServices
    {
        Task<ProdutoModel> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoModel>> ObterTodos();
        Task<ResponseResult> Adicionar(Produto produto);
        Task<ResponseResult> Atualizar(Produto produto);
        Task<ResponseResult> Remover(Guid id);
    }

    public class ProdutoServices : BaseService, IProdutoServices
    {
        private readonly HttpClient _httpClient;
        public ProdutoServices(HttpClient httpClient,
            IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.XGameAPIUrl);
        }

        public async Task<ResponseResult> Adicionar(Produto produto)
        {

            var produtoContent = ObterConteudo(produto);

            var response = await _httpClient.PostAsync("api/produto/novo-produto/", produtoContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(Produto produto)
        {
            var produtoContent = ObterConteudo(produto);

            var response = await _httpClient.PutAsync("api/produto/editar/", produtoContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ProdutoModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/produto/by-id/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<ProdutoModel>(response);
        }

        public async Task<IEnumerable<ProdutoModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("api/produto/todos/");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<IEnumerable<ProdutoModel>>(response);
        }

        public async Task<ResponseResult> Remover(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"api/produto/remove/{id}");

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
