namespace Application.API.Model.DTO
{
    public class ProdutoEditDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }

        public Guid CategoriaId { get; set; }

    }
}
