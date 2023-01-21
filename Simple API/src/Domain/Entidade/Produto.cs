
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entidade
{
    public class Produto : Entity
    {

        public string Nome { get; set; }

        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }

        //EF
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


        public override bool IsValid()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Valor)
                .NotEqual(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(c => c.CategoriaId)
            .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} inválido");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }

}