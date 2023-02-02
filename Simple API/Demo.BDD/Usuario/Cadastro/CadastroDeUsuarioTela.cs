using Demo.BDD.Test.Config;
using Demo.BDD.Test.Model;
using Demo.BDD.Test.Usuario.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BDD.Test.Usuario.Cadastro
{
    public class CadastroDeUsuarioTela : BaseUsuarioTela
    {
        public CadastroDeUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }


        public void ClicarNoLinkRegistrar()
        {
            Helper.ClicarLinkPorTexto("Register");
        }

        public void PreencherFormularioRegistro(User usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
            Helper.PreencherTextBoxPorId("Input_ConfirmPassword", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioRegistro(User usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;
            if (Helper.ObterValorTextBoxPorId("Input_ConfirmPassword") != usuario.Senha) return false;

            return true;
        }

        public void ClicarNoBotaoRegistrar()
        {
            var botao = Helper.ObterElementoPorXPath("/html/body/div/main/div/div[1]/form/button");
            botao.Click();
        }
        public void FecharNavegador()
        {
            Helper.FecharNavegador();
        }
    }
}
