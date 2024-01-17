using Application.API.Model.DTO;
using Application.API.Test.Fixture;
using Application.API.Test.Utils;
using Domain.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Test.Integration.Categoria
{

    [CollectionDefinition(nameof(IntegrationApiFixture))]
    public class CategoriaIntegrationTest : BaseResponseService,
        IClassFixture<IntegrationApiTestsFixture<Program>>
    {
        private readonly IntegrationApiTestsFixture<Program> _fixture;
        private readonly AutoMocker _mocker;

        public CategoriaIntegrationTest(IntegrationApiTestsFixture<Program> fixture)
        {
            _mocker = new AutoMocker();
            _fixture = fixture;
        }

        [Fact(DisplayName = "Cadastrar Categoria Com sucesso")]
        [Trait("Categoria", "Integration API - Categoria")]
        public async Task Cadastrar_Categoria_Valido_DeveRetornar_Ok()
        {
            //Arrange
            var model = new CategoriaAddDTO
            {
                Nome = "Cameras",

            };

            //Act
            var response = await _fixture.Client.PostAsJsonAsync("/api/Categoria/nova-categoria", model);

            Xunit.Assert.True(response.IsSuccessStatusCode);
            Xunit.Assert.True(TratarErrosResponse(response));

        }

        [Fact(DisplayName = "Editar Categoria com sucesso")]
        [Trait("Categoria", "Integration API - Categoria")]
        public async Task Editar_Categoria_ComSucesso_DeveRetornar_Ok()
        {
            //Arrange
            var model = new CategoriaEditDTO
            {
                Id = Guid.Parse("e031fd85-6498-4093-bd37-5000916c9ac5"),
                Nome = "Placas de video",

            };

            //Act
            var response = await _fixture.Client.PutAsJsonAsync("/api/Categoria/editar", model);

            Xunit.Assert.True(response.IsSuccessStatusCode);
            Xunit.Assert.True(TratarErrosResponse(response));
        }

        [Fact(DisplayName = "Editar Categoria Invalida")]
        [Trait("Categoria", "Integration API - Categoria")]
        public async Task Cadastrar_Categoria_Invalido_DeveRetornarErro()
        {
            //Arrange
            var model = new CategoriaEditDTO
            {
                Id = Guid.Parse("e031fd85-6498-4093-bd37-5000916c9ac5"),
                Nome = string.Empty,

            };

            //Act
            var response = await _fixture.Client.PutAsJsonAsync("/api/Categoria/editar", model);

            var result = await DeseralizaObjetoResponse<ResponseResult>(response);

            //Assert
            Xunit.Assert.NotEqual(0, result.Errors.Mensagens.Count());
            Xunit.Assert.False(response.IsSuccessStatusCode);
            Xunit.Assert.False(TratarErrosResponse(response));
        }
    }
}
