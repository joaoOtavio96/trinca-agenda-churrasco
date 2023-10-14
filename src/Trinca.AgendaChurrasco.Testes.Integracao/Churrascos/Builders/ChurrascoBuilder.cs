using AutoBogus;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Churrascos.Builders;

public class ChurrascoBuilder : AutoFaker<Churrasco>
{
    public ChurrascoBuilder()
    {
        RuleFor(x => x.Titulo, f => f.Random.String2(50));
        RuleFor(x => x.Descricao, f => f.Random.String2(500));
        RuleFor(x => x.Observacao, f => f.Random.String2(500));
        RuleFor(x => x.Data, f => f.Date.Recent());
        RuleFor(x => x.ValorSugeridoSemBebida, f => f.Random.Decimal());
        RuleFor(x => x.ValorSugeridoComBebida, f => f.Random.Decimal());
        RuleSet("default", f =>
        {
            f.Ignore(x => x.Participantes);
            f.Ignore(x => x.Id);
            f.Ignore(x => x.DataCriacao);
        });
    }

    public ChurrascoBuilder ComValoresInvalidos()
    {
        RuleFor(x => x.Titulo, f => null);
        RuleFor(x => x.Descricao, f => f.Random.String2(600));
        RuleFor(x => x.Data, f => f.Date.Past());
        RuleFor(x => x.ValorSugeridoSemBebida, f => 0);

        return this;
    }
}