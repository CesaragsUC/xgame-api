using Demo.BDD.Test.Config;
using Demo.BDD.Test.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V85.IndexedDB;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Demo.BDD.Test.Usuario.Login
{
    [Binding]

    public class FazerLoginNoSistemaStepDefinitions : IClassFixture<AutomacaoWebFixtureCollection>
    {
        private IWebDriver _driver;
        private readonly LoginUsuarioTela _loginUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public FazerLoginNoSistemaStepDefinitions(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _loginUsuarioTela = new LoginUsuarioTela(testsFixture.BrowserHelper);

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }



        [When(@"Ele clicar no botao login")]
        public void WhenEleClicarNoBotaoLogin()
        {
            //Act
            _loginUsuarioTela.ClicarNoLinkLogin();

            // Assert
            Xunit.Assert.Contains(_testsFixture.Configuration.LoginUrl,
                _loginUsuarioTela.ObterUrl());
        }

        [When(@"Preencher o formulario de login")]
        public void WhenPreencherOFormularioDeLogin(Table table)
        {
            //Act
            var usuario = new User
            {
                Email = "cesar@teste.com",
                Senha = "Teste@123"
            };
            _testsFixture.Usuario = usuario;
            _loginUsuarioTela.PreencherFormularioLogin(usuario);

            // Assert
            Xunit.Assert.True(_loginUsuarioTela.ValidarPreenchimentoFormularioLogin(usuario));
        }

        [When(@"Clicar no botao login")]
        public void WhenClicarNoBotaoLogin()
        {
            _loginUsuarioTela.ClicarNoBotaoLogin(); 
        }

    }
}
