using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidade
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


        }
    }
}
