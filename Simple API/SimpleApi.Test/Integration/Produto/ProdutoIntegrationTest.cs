using Application.API.Model.DTO;
using Application.API.Test.Fixture;
using Application.API.Test.Utils;
using Domain.Services;
using Moq.AutoMock;
using System.Net.Http.Json;

namespace Application.API.Test.Integration.Produto
{
    [CollectionDefinition(nameof(IntegrationApiFixture))]
    public class ProdutoIntegrationTest : BaseResponseService, IClassFixture<IntegrationApiTestsFixture<Program>>
    {
        private readonly IntegrationApiTestsFixture<Program> _fixture;
        private readonly AutoMocker _mocker;
        private Guid _produtoId;
        public ProdutoIntegrationTest(IntegrationApiTestsFixture<Program> fixture)
        {
            _mocker = new AutoMocker();
            _fixture = fixture;
        }


        [Fact(DisplayName = "Cadastrar Produto Invalido")]
        [Trait("Produto", "Integration API - Produto")]
        public async Task Cadastrar_Produto_Invalido_DeveRetornarErro()
        {
            await _fixture.RealizarLoginApi();

        }


        [Fact(DisplayName = "Cadastrar Produto deve ser valido")]
        [Trait("Produto", "Integration API - Produto")]
        public async Task Cadastrar_Produto_DeveSerValido_OK()
        {

            //Arrange
            var model = new ProdutoAddDTO
            {
                Ativo = true,
                CategoriaId = Guid.Parse("b35f3d4e-bd31-45e9-8615-b0974f599411"),
                Imagem = string.Empty,
                Nome = "Slideck com regulagem de altura",
                Quantidade = 12,
                Valor = 3800.00m
            };

            //Act

            var response = await _fixture.Client.PostAsJsonAsync("/api/Produto/novo-produto", model);

            //Assert
            response.EnsureSuccessStatusCode();
            Xunit.Assert.True(response.IsSuccessStatusCode);

        }

        [Fact(DisplayName = "Cadastrar Produto Invalido")]
        [Trait("Produto", "Integration API - Produto")]
        public async Task Cadastrar_ProdutoInvalido_Deve_RetornarErro()
        {

            //Arrange
            var model = new ProdutoAddDTO
            {
                Ativo = true,
                CategoriaId = Guid.Empty,
                Imagem = string.Empty,
                Nome = string.Empty,
                Quantidade = 0,
                Valor = 0
            };

            //Act

            var response = await _fixture.Client.PostAsJsonAsync("/api/Produto/novo-produto", model);

            var result = await DeseralizaObjetoResponse<ResponseResult>(response);

            //Assert
            Xunit.Assert.True(result.Errors.Mensagens.Contains("O campo Categoria Id inválido"));
            Xunit.Assert.NotEqual(0,result.Errors.Mensagens.Count());
            Xunit.Assert.False(response.IsSuccessStatusCode);
            Xunit.Assert.False(TratarErrosResponse(response));
        }

        [Fact(DisplayName = "Editar Produto com sucesso")]
        [Trait("Produto", "Integration API - Produto")]
        public async Task Editar_Produto_Deve_Retornar_Ok()
        {

            //Arrange
            var model = new ProdutoEditDTO
            {
                Id = Guid.Parse("7128d054-e473-4dd7-8d80-7af73323a2b4"),
                Ativo = true,
                CategoriaId = Guid.Parse("b35f3d4e-bd31-45e9-8615-b0974f599411"),
                Imagem = string.Empty,
                Nome = "Mesa Slideck com regulagem de altura",
                Quantidade = 12,
                Valor = 3800.00m
            };

            //Act

            var response = await _fixture.Client.PutAsJsonAsync("/api/Produto/editar", model);

            //Assert
            Xunit.Assert.True(response.IsSuccessStatusCode);
            Xunit.Assert.True(TratarErrosResponse(response));

        }

        [Fact(DisplayName = "Editar Produto Invalido")]
        [Trait("Produto", "Integration API - Produto")]
        public async Task Editar_ProdutoNomeVazio_Deve_RetornarErro()
        {

            //Arrange
            var model = new ProdutoEditDTO
            {
                Id = Guid.Parse("7128d054-e473-4dd7-8d80-7af73323a2b4"),
                Ativo = true,
                CategoriaId = Guid.Empty,
                Imagem = string.Empty,
                Nome = string.Empty,
                Quantidade = 0,
                Valor = 0
            };

            //Act

            var response = await _fixture.Client.PutAsJsonAsync("/api/Produto/editar", model);

            //Assert
            var result = await DeseralizaObjetoResponse<ResponseResult>(response);

            //Assert
            Xunit.Assert.True(result.Errors.Mensagens.Contains("O campo Categoria Id inválido"));
            Xunit.Assert.NotEqual(0, result.Errors.Mensagens.Count());
            Xunit.Assert.False(response.IsSuccessStatusCode);
            Xunit.Assert.False(TratarErrosResponse(response));

        }
    }
}