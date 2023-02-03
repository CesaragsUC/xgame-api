using Application.API.Test.Fixture;
using Moq.AutoMock;
using Xunit;

namespace MVC.Test.Integration.Login
{
    [CollectionDefinition(nameof(IntegrationWebFixture))]
    public class LoginIntegrationTest : IClassFixture<IntegrationWebTestsFixture<Program>>
    {
        private readonly IntegrationWebTestsFixture<Program> _fixture;
        private readonly AutoMocker _mocker;

        public LoginIntegrationTest(IntegrationWebTestsFixture<Program> fixture)
        {
            _mocker = new AutoMocker();
            _fixture = fixture;
        }

        [Fact(DisplayName = "Realizar login web")]
        [Trait("Realizar login","Integration Web - Login")]
        public async Task Cadastrar_Produto_Invalido_DeveRetornarErro()
        {
           var result =  await  _fixture.RealizarLoginWeb();

            Xunit.Assert.True(result.EnsureSuccessStatusCode().IsSuccessStatusCode); 

        }


    }
}