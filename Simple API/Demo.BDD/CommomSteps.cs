using Demo.BDD.Test.Config;
using Demo.BDD.Test.Usuario.Cadastro;
using Demo.BDD.Test.Usuario.Login;
using TechTalk.SpecFlow;
using Xunit;

namespace Demo.BDD.Test
{
    [Binding]
    public class CommomSteps : IClassFixture<AutomacaoWebFixtureCollection>
    {
        private readonly CadastroDeUsuarioTela _cadastroUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public CommomSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _cadastroUsuarioTela = new CadastroDeUsuarioTela(testsFixture.BrowserHelper);
        }

        [Given(@"Que o visitante esta acessando o site")]
        public void GivenQueOVisitanteEstaAcessandoOSite()
        {
            // Act
            _cadastroUsuarioTela.AcessarSite();

            // Assert
            Xunit.Assert.Contains(_testsFixture.Configuration.DomainUrl, _cadastroUsuarioTela.ObterUrl());
        }

        [Then(@"Ele Sera redirecionado para pagina inicial")]
        public void ThenEleSeraRedirecionadoParaPaginaInicial()
        {
            // Assert
            Xunit.Assert.Contains(_testsFixture.Configuration.DomainUrl, _cadastroUsuarioTela.ObterUrl());
        }

        [Then(@"Sera exibido seu email no topo do site")]
        public void ThenSeraExibidoSeuEmailNoTopoDoSite()
        {
            // Assert
            Xunit.Assert.True(_cadastroUsuarioTela.ValidarSaudacaoUsuarioLogado(_testsFixture.Usuario));
        }
    }
}
