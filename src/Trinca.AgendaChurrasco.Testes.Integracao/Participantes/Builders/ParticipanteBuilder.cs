using AutoBogus;
using Trinca.AgendaChurrasco.Domain.Participantes;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Participantes.Builders;

public class ParticipanteBuilder : AutoFaker<Participante>
{
    public ParticipanteBuilder()
    {
        RuleFor(x => x.Nome, f => f.Name.FullName());
        RuleFor(x => x.Valor, f => f.Random.Decimal());
        RuleSet("default", f =>
        {
            f.Ignore(x => x.Id);
            f.Ignore(x => x.DataCriacao);
        });
    }
}