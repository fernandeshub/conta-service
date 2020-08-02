using ContaService.API.ViewModels;
using FluentValidation;

namespace ContaService.API.Validation
{
    public class LancamentoValidator : AbstractValidator<Lancamento> {
	public LancamentoValidator() {
		RuleFor(x => x.Valor).GreaterThan(0);
        RuleFor(x => x.ContaOrigem).Matches(@"^\d{5}-\d{1}$");
        RuleFor(x => x.ContaDestino).Matches(@"^\d{5}-\d{1}$");
	}
}
}