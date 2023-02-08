using Application.API.Messages;
using Application.API.Messages.Integracao;
using Application.API.Utils;
using Domain.Entidade;
using Domain.Interface;
using EasyNetQ;
using FluentValidation.Results;
using Infra.Migrations;
using pplication.API.MessageBus;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace Application.API.Integration_Services
{
    public class RegistroProdutoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;


        public RegistroProdutoIntegrationHandler(IMessageBus bus,
            IServiceProvider serviceProvider )
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<ProdutoRegistradoIntegrationEvent, ResponseMessage>(async request => await ProdutoRegistrado(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }
        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }
        private async Task<ResponseMessage> ProdutoRegistrado(ProdutoRegistradoIntegrationEvent message)
        {

            var produto = new Produto
            {
                Id = message.Id,
                Valor = message.Valor,
                Nome = message.Nome,
                Imagem = message.Imagem,
                Quantidade = message.Quantidade,
                Ativo = message.Ativo,
                CategoriaId = message.CategoriaId,
                DataCadastro = message.DataCadastro

            };
            var produtojson =  System.Text.Json.JsonSerializer.Serialize(produto);
            //var produtoText = Encoding.UTF8.GetBytes(produtojson);

            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Produto Cadastrado: " + produtojson);
            Console.ResetColor();

            return new ResponseMessage(new ValidationResult());
        }
    }
}
