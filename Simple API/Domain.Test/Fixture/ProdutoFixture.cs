using Bogus;
using Xunit;

namespace Domain.Test.Fixture
{

    [CollectionDefinition(nameof(ProdutoCollection))]

    public class ProdutoCollection : ICollectionFixture<ProdutoFixture> { }

    public class ProdutoFixture : IDisposable
    {


        public  Domain.Entidade.Produto GerarProduto()
        {

            var produto = new Domain.Entidade.Produto
            {
                Ativo = true,
                CategoriaId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                DataCadastro = new Faker().Date.Future(),
                Id = Guid.NewGuid(),
                Imagem = new Faker().Lorem.Text(),
                Nome = new Faker().Vehicle.Model(),
                Valor = new Faker().Random.Decimal(1, 200),
                Quantidade = new Faker().Random.Number(1, 5),
            };

            return produto;
        }

        public Domain.Entidade.Produto GerarProdutoInvalido()
        {

            var produto = new Domain.Entidade.Produto
            {
                Ativo = true,
                CategoriaId = Guid.Empty,
                DataCadastro = DateTime.Now,
                Id = Guid.Empty,
                Imagem = "no image",
                Nome = "",
                Valor = 0,
                Quantidade = 0
            };

            return produto;
        }

        public void Dispose()
        {

        }
    }
}
