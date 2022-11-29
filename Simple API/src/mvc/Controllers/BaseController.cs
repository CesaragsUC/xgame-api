using Domain.Interface;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using mvc.Services;

namespace mvc.Controllers
{
    public abstract class BaseController : Controller
    {

        protected bool ResponsePossuiErros(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach (var message in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }

        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected bool OperacaoEhValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
