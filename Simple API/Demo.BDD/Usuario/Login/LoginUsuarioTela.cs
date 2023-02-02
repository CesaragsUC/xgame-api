using Demo.BDD.Test.Config;
using Demo.BDD.Test.Model;

namespace Demo.BDD.Test.Usuario.Login
{
    public class LoginUsuarioTela : BaseUsuarioTela
    {
        public LoginUsuarioTela(SeleniumHelper helper) : base(helper) { }

        public void ClicarNoLinkLogin()
        {
            Helper.ClicarLinkPorTexto("Login");
        }

        public void PreencherFormularioLogin(User usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioLogin(User usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;

            return true;
        }

        public void ClicarNoBotaoLogin()
        {
            var botao = Helper.ObterElementoPorXPath("/html/body/div/main/div/div[1]/section/form/div[4]/button");
            botao.Click();
        }

        public bool Login(User usuario)
        {
            AcessarSite();
            ClicarNoLinkLogin();
            PreencherFormularioLogin(usuario);
            if (!ValidarPreenchimentoFormularioLogin(usuario)) return false;
            ClicarNoBotaoLogin();
            if (!ValidarSaudacaoUsuarioLogado(usuario)) return false;

            return true;
        }
        public void FecharNavegador()
        {
            Helper.FecharNavegador();
        }
    }
}
