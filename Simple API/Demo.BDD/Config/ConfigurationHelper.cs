using Microsoft.Extensions.Configuration;

namespace Demo.BDD.Test.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;
        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        public string PaginaInicial => _config.GetSection("PaginaInicial").Value;
        public string ProdutosUrl => $"{DomainUrl}{_config.GetSection("ProdutosUrl").Value}";
        public string ProdutoEditarUrl => $"{DomainUrl}{_config.GetSection("ProdutoEditarUrl").Value}";
        public string DomainUrl => _config.GetSection("DomainUrl").Value;
        public string RegisterUrl => $"{DomainUrl}{_config.GetSection("RegisterUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";
        public string CategoriaUrl => $"{_config.GetSection("CategoriaUrl").Value}";
        public string CategoriaAdicionarUrl => $"{_config.GetSection("CategoriaAdicionarUrl").Value}";
        public string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderPath}{_config.GetSection("FolderPicture").Value}";
    }
}
