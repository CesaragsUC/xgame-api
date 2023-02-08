using Domain.Utils;
using FluentValidation;

namespace Domain.Entidade.Validacao
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                  .WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.Cpf)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }

        protected static bool TerEmailValido(string email)
        {
            return EmailValidation.ValidarEmail(email);
        }
    }
}
