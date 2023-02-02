using Demo.BDD.Test.Config;
using Demo.BDD.Test.Model;

namespace Demo.BDD.Test.Usuario.Login
{
    public abstract class BaseUsuarioTela : PageObjectModel
    {
        protected BaseUsuarioTela(SeleniumHelper helper) : base(helper) { }

        public void AcessarSite()
        {
            Helper.IrParaUrl(Helper.Configuration.DomainUrl);
        }

        public bool ValidarSaudacaoUsuarioLogado(User usuario)
        {
            return Helper.ObterTextoElementoPorId("saudacaoUsuario").Contains(usuario.Email);
        }

        public bool ValidarMensagemDeErroFormulario(string mensagem)
        {
            return Helper.ObterTextoElementoPorClasseCss("text-danger")
                .Contains(mensagem);
        }
    }
}
