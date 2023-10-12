using FluentValidation;

namespace Trinca.AgendaChurrasco.Domain.Churrascos;

public class ChurrascoValidator : AbstractValidator<Churrasco>
{
    public ChurrascoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage(ChurrascoErrorMessages.TituloVazio)
            .MaximumLength(100)
            .WithMessage(ChurrascoErrorMessages.TituloTamanhoMaximo);

        RuleFor(x => x.Descricao)
            .MaximumLength(500)
            .WithMessage(ChurrascoErrorMessages.DescricaoTamanhoMaximo);
        
        RuleFor(x => x.Observacao)
            .MaximumLength(500)
            .WithMessage(ChurrascoErrorMessages.ObservacaoTamanhoMaximo);

        RuleFor(x => x.Data)
            .NotEmpty()
            .WithMessage(ChurrascoErrorMessages.DataObrigatoria)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage(ChurrascoErrorMessages.DataDeveSerFutura);

        RuleFor(x => x.ValorSugeridoSemBebida)
            .GreaterThan(0)
            .WithMessage(ChurrascoErrorMessages.ValorSugeridoDeveSerInformado);
        
        RuleFor(x => x.ValorSugeridoComBebida)
            .GreaterThan(0)
            .WithMessage(ChurrascoErrorMessages.ValorSugeridoDeveSerInformado);
    }
}