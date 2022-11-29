namespace Application.API.Model.DTO
{
    public class ProdutoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        //EF
        public Guid CategoriaId { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
