using Bogus;
using Demo.BDD.Test.Model;
using Xunit;

namespace Demo.BDD.Test.Config
{
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class AutomacaoWebFixtureCollection : ICollectionFixture<AutomacaoWebTestsFixture> { }

    public class AutomacaoWebTestsFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public User Usuario;

        public AutomacaoWebTestsFixture()
        {
            Usuario = new User();
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Navegador.Chrome, Configuration);
        }

        public void GerarDadosUsuario()
        {
            var faker = new Faker("pt_BR");
            Usuario.Email = faker.Internet.Email().ToLower();
            Usuario.Senha = faker.Internet.Password(8, false, "", "@1Ab_");
        }
    }
}
