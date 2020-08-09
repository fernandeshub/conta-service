using ContaService.API.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ContaService.API.Application.Validations
{
    public class CreateContaCorrenteCommandValidator : AbstractValidator<CreateContaCorrenteCommand>
    {
        public CreateContaCorrenteCommandValidator(ILogger<CreateContaCorrenteCommandValidator> logger)
        {
             RuleFor(x => x.Numero).Matches(@"^\d{5}-\d{1}$").WithMessage("O número da conta corrente deve respeitar o formato 99999-9");
             logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}