using Application.API.Extensions;
using Application.API.Integration_Services;
using pplication.API.MessageBus;

namespace Application.API.Config
{
    public  static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroClienteIntegrationHandler>();

            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroProdutoIntegrationHandler>();

            
        }
    }
}
