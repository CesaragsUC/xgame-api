//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Entidade.Validacao
//{
//    public class ProdutoValidation : AbstractValidator<Produto>
//    {
//        public ProdutoValidation()
//        {
//            RuleFor(c => c.Nome)
//                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
//                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

//            RuleFor(c => c.Valor)
//                .NotEqual(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

//            RuleFor(c => c.CategoriaId)
//            .NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} inválido");

//            RuleFor(c => c.Quantidade)
//                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
//        }
//    }
//}
