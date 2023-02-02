using Demo.BDD.Test.Config;
using Demo.BDD.Test.Model;
using Demo.BDD.Test.Usuario.Login;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace Demo.BDD.Test.Usuario.Cadastro
{
    [Binding]
    public class CadastroDoUsuarioStepDefinitions : IClassFixture<AutomacaoWebFixtureCollection>
    {

        private IWebDriver _driver;
        private readonly CadastroDeUsuarioTela _cadastroUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public CadastroDoUsuarioStepDefinitions(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroUsuarioTela = new CadastroDeUsuarioTela(testsFixture.BrowserHelper);

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Given(@"Clicar em se registrar registrar")]
        public void GivenClicarEmSeRegistrarRegistrar()
        {
            //Act
            _cadastroUsuarioTela.ClicarNoLinkRegistrar();

            //Assert
            Xunit.Assert.Contains(_testsFixture.Configuration.RegisterUrl, _cadastroUsuarioTela.ObterUrl());
        }


        [Given(@"Preencher o formulario")]
        public void GivenPreencherOFormulario(Table table)
        {
            //Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;

            //Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            //Assert
            Xunit.Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [Given(@"Clicar no botao registrar")]
        public void GivenClicarNoBotaoRegistrar()
        {
            //Arrange
            _cadastroUsuarioTela.ClicarNoBotaoRegistrar();

        }

        [Given(@"Preencher o formulario com senha sem letras maiusculas")]
        public void GivenPreencherOFormularioComSenhaSemLetrasMaiusculas(Table table)
        {
            //Act
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "teste@123";

            //Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            //Assert
            Xunit.Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }

        [Then(@"ele recebera uma mensagem de erro dizendo que a senha precisa de letras maiusculas")]
        public void ThenEleReceberaUmaMensagemDeErroDizendoQueASenhaPrecisaDeLetrasMaiusculas()
        {
            //Assert
            Xunit.Assert.True(_cadastroUsuarioTela.ValidarMensagemDeErroFormulario("Passwords must have at least one uppercase ('A'-'Z')"));
        }

        [Given(@"Preencher o formulario com senha sem caracteres especiais")]
        public void GivenPreencherOFormularioComSenhaSemCaracteresEspeciais(Table table)
        {
            // Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "Teste123";

            // Act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            // Assert
            Xunit.Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }


        [Then(@"ele recebera uma mensagem de erro dizendo que a senha precisa conter caracteres especiais")]
        public void ThenEleReceberaUmaMensagemDeErroDizendoQueASenhaPrecisaConterCaracteresEspeciais()
        {
            Xunit.Assert.True(_cadastroUsuarioTela
                .ValidarMensagemDeErroFormulario("Passwords must have at least one non alphanumeric character"));
        }
    }
}
