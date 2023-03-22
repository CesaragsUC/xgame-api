namespace Application.API.Messages.Integracao
{
    public class ProdutoRegistradoIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public string Imagem { get; private set; }
        public decimal Valor { get; private set; }
        public int Quantidade { get; private set; }

        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Guid CategoriaId { get; private set; }

        public ProdutoRegistradoIntegrationEvent(Guid id, string nome, string imagem, decimal valor, int quantidade,
            bool ativo, Guid categoriId)
        {
            Id = id;
            Nome = nome;
            Imagem = imagem;
            Valor = valor;
            Quantidade = quantidade;
            Ativo = ativo;
            DataCadastro = DateTime.Now;
            CategoriaId = categoriId;
        }
    }
}
