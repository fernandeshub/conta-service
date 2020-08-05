using ContaService.API.Models;
using FluentValidation;

namespace ContaService.API.Validation
{
    public class TransferenciaValidator : AbstractValidator<Transferencia> {
	public TransferenciaValidator() {
		RuleFor(x => x.Valor).GreaterThan(0);
        RuleFor(x => x.ContaOrigem).Matches(@"^\d{5}-\d{1}$");
        RuleFor(x => x.ContaDestino).Matches(@"^\d{5}-\d{1}$");
	}
}
}