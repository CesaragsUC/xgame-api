using Application.API.Messages;
using Application.API.Messages.Integracao;
using Domain.Entidade;
using Domain.Interface;
using FluentValidation.Results;
using pplication.API.MessageBus;

namespace Application.API.Integration_Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(IMessageBus bus,
            IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request => await RegistrarCliente(request));

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
        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            ValidationResult sucesso;

            var cliente = new Cliente
            {
                Id = message.Id,
                Ativo = true,
                Cpf = message.Cpf,
                Email = message.Email,
                Nome = message.Nome,
                DataCadastro = message.DataCadastro
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                var clienteRepository = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
                sucesso = await clienteRepository.Add(cliente);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
