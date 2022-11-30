using System.ComponentModel.DataAnnotations.Schema;

namespace mvc.Models
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string? Imagem { get; set; }
        public IFormFile ImagemUpload { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        public Guid CategoriaId { get; set; }
        
    }
}
