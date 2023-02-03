using Domain.Entidade;
using Domain.Services;
using Microsoft.Extensions.Options;
using mvc.Models;

namespace mvc.Services
{


    public interface ICategoriaService
    {
        Task<ResponseResult> Adicionar(Categoria model);
        Task<ResponseResult> Atualizar(Categoria model);
        Task<IEnumerable<CategoriaModel>> ObterTodos();
        Task<CategoriaModel> ObterPorId(Guid id);
        Task<ResponseResult> Remover(Guid id);
    }


    public class CategoriaServices : BaseService, ICategoriaService
    {
        private readonly HttpClient _httpClient;
        public CategoriaServices(HttpClient httpClient, 
            IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.XGameAPIUrl);
        }

        public async Task<ResponseResult> Adicionar(Categoria model)
        {
            var categoriaContent = ObterConteudo(model);

            var response = await _httpClient.PostAsync("api/categoria/nova-categoria/", categoriaContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> Atualizar(Categoria model)
        {
            var categoriaContent = ObterConteudo(model);

            var response = await _httpClient.PutAsync("api/categoria/editar/", categoriaContent);

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<CategoriaModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/categoria/by-id/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<CategoriaModel>(response);
        }

        public async Task<IEnumerable<CategoriaModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("api/categoria/todos/");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeseralizaObjetoResponse<IEnumerable<CategoriaModel>>(response);
        }

        public async Task<ResponseResult> Remover(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"api/categoria/remove/{id}");

            if (!TratarErrosResponse(response)) return await DeseralizaObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
