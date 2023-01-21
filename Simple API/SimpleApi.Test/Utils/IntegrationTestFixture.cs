//using Microsoft.AspNetCore.Mvc.Testing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SimpleApi.Test.Utils
//{
//    [CollectionDefinition(nameof(IntegrationTestFixtureCollection))]

//    public class IntegrationAPITestFixtureCollection : ICollectionFixture<IntegrationTestFixture>

//    public class IntegrationTestFixture<TStartup> : IDisposable where TStartup : class
//    {
//        public string AntiForgeryFieldName = "__RequestVerificationToken";

//        public string UsuarioEmail;
//        public string UsuarioSenha;

//        public string UsuarioToken;


//        public readonly DemoFactory<TStartup> Factory;
//        public HttpClient Client;

//        public IntegrationTestFixture()
//        {
//            var clientOptions = new WebApplicationFactoryClientOptions
//            {
//                AllowAutoRedirect = true,
//                BaseAddress = new Uri("http://localhost"),
//                HandleCookies = true,
//                MaxAutomaticRedirections  = 7  
//            };

//            Factory = new DemoFactory<TStartup>();
//            Client = Factory.CreateClient(clientOptions);
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
