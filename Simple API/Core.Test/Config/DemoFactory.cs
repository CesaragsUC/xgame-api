using Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Test.Config
{
    public class DemoFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connection = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings")["DefaultConnection"];

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<XGamesContext>));
                services.Remove(descriptor);

                //Usar Db
                services.AddDbContext<XGamesContext>(options =>
                options.UseSqlServer(connection));


                #region InmemoryConfig

                // services.AddDbContext<XGamesContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
                //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));

                //var sp = services.BuildServiceProvider();
                //using (var scope = sp.CreateScope())
                //using (var appContext = scope.ServiceProvider.GetRequiredService<XGamesContext>())
                //{
                //    try
                //    {
                //        appContext.Database.EnsureCreated();
                //    }
                //    catch (Exception ex)
                //    {
                //        //Log errors or do anything you think it's needed
                //        throw;
                //    }
                //}

                //var sp2 = services.BuildServiceProvider();
                //using (var scope = sp.CreateScope())
                //using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                //{
                //    try
                //    {
                //        appContext.Database.EnsureCreated();
                //    }
                //    catch (Exception ex)
                //    {
                //        //Log errors or do anything you think it's needed
                //        throw;
                //    }
                //}

                #endregion



            });

        }
    }

}
