using Core.Test.Config;
using Moq.AutoMock;

namespace Core.Test
{

    [Collection(nameof(IntegrationApiTestsFixture))]
    public class ProdutoIntegrationTest
    {
        private readonly IntegrationTestsFixture<Program> _fixture;
        private readonly AutoMocker _mocker;

        public ProdutoIntegrationTest(
            IntegrationTestsFixture<Program> fixture)
        {
            _mocker = new AutoMocker();
            _fixture = fixture;
        }

        [Fact(DisplayName = "Cadastrar Produto Invalido")]
        public async Task Cadastrar_Produto_Invalido_DeveRetornarErro()
        {


           await  _fixture.RealizarLoginWeb();

        }


        [Fact(DisplayName = "Cadastrar Produto deve ser valido")]
        public async Task Cadastrar_Produto_DeveSerValido_OK()
        {
        }


    }
}