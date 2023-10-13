using FluentValidation;

namespace Trinca.AgendaChurrasco.Domain.Participantes;

public class ParticipanteValidator : AbstractValidator<Participante>
{
    public ParticipanteValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage(ParticipanteErrorMessages.NomeDeveSerInformado)
            .MaximumLength(100)
            .WithMessage(ParticipanteErrorMessages.NomeTamanhoMaximo);

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage(ParticipanteErrorMessages.ValorDeveSerInformado);
    }
}