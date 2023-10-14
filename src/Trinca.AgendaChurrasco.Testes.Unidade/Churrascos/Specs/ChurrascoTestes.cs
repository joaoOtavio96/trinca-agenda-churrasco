using FluentAssertions;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Participantes;
using Trinca.AgendaChurrasco.Testes.Unidade.Churrascos.Builders;
using Trinca.AgendaChurrasco.Testes.Unidade.Participantes.Builders;

namespace Trinca.AgendaChurrasco.Testes.Unidade.Churrascos.Specs;

public class ChurrascoTestes
{
    [Test]
    public void Atualizar_DeveAtualizarValoresChurrasco()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        var churrascoAtualizado = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        
        churrasco.Atualizar(churrascoAtualizado);

        churrasco.Should().BeEquivalentTo(churrascoAtualizado);
    }

    [Test]
    public void CalcularValorTotal_DeveRetornarValorTotalArrecadado()
    {
        var churrasco = new ChurrascoBuilder().Generate();
        churrasco.Participantes = new ParticipanteBuilder().Generate(2);
        var valorTotalEsperado = churrasco.Participantes.Sum(x => x.Valor);

        var valorTotalChurrasco = churrasco.CalcularValorTotal();

        valorTotalChurrasco.Should().Be(valorTotalEsperado);
    }
    
    [Test]
    public void ParticipantesTotal_DeveRetornarTotalDeParticipantesChurrasco()
    {
        var churrasco = new ChurrascoBuilder().Generate();
        churrasco.Participantes = new ParticipanteBuilder().Generate(2);
        var totalParticipantesEsperado = churrasco.Participantes.Count;

        var totalParticipantes = churrasco.ParticipantesTotal();

        totalParticipantes.Should().Be(totalParticipantesEsperado);
    }
}