using Domain.Interface;
using Moq;
using Moq.AutoMock;
using SimpleApi.Test.Services;

namespace SimpleApi.Test
{

    [Collection(nameof(ProdutoCollection))]
    public class ProdutoTest
    {
        private readonly ProdutoFixture _produtoFixture;
        private  Mock<IProdutoRepository> _produtoRepository;
        private  Mock<INotificador> _inotificator;
        private readonly AutoMocker _mocker;


        //Inicializa a o Mock dos repositorios necessarios para o ProdutoService
        private void MocksIntinitalizer()
        {
             _produtoRepository = new Mock<IProdutoRepository>();
            _inotificator = new Mock<INotificador>();
        }

        public ProdutoTest(ProdutoFixture produtoFixture)
        {
            _produtoFixture = produtoFixture;
            _mocker = new AutoMocker();

        }

        [Fact(DisplayName = "Cadastrar Produto Invalido")]
        public async Task Cadastrar_Produto_Invalido_DeveRetornarErro()
        {
            var produto = _produtoFixture.GerarProdutoInvalido();
            Assert.False(produto.IsValid());
        }


        [Fact(DisplayName = "Cadastrar Produto deve ser valido")]
        public async Task Cadastrar_Produto_DeveSerValido_OK()
        {
            var produto = _produtoFixture.GerarProduto();

            Assert.True(produto.IsValid());
        }

        [Fact(DisplayName = "Cadastrar Produto  entidade deve ser valida")]
        public async Task Cadastrar_Produto_ValidarEntidade_OK()
        {
            MocksIntinitalizer();
            var produto = _produtoFixture.GerarProduto();

            var produtoService = new ProdutoService(_produtoRepository.Object, _inotificator.Object);
            await produtoService.Adicionar(produto);

            Assert.True(produto.IsValid());
        }


    }
}