using Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Test.Config
{
    public class DemoFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<XGamesContext>));
                services.Remove(descriptor);

                //var dbConnectionDescriptor = services.SingleOrDefault(
                //    d => d.ServiceType ==
                //        typeof(DbConnection));

                //services.Remove(dbConnectionDescriptor);

                services.AddDbContext<XGamesContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            });

        }
    }

}
