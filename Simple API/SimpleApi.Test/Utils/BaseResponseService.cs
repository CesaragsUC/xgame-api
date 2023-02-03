using Domain.Entidade;
using Domain.Interface;
using Domain.Notificacoes;
using Domain.Services;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using System.Text.Json;
using System.Text;

namespace Application.API.Test.Utils
{
    public abstract class BaseResponseService
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeseralizaObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
        protected ResponseResult RetornoFalhaValidacao()
        {
            var erro = new ResponseResult();
            erro.Errors = new ResponseErrorMessages();
            erro.Errors.Mensagens.Add("Houve um erro nao validacao da entidade");

            return erro;
        }

        protected ResponseResult RetornoBadRequest()
        {
            var erro = new ResponseResult();
            erro.Errors = new ResponseErrorMessages();
            erro.Errors.Mensagens.Add("Houve um erro nao validacao");

            return erro;
        }
    }
}
