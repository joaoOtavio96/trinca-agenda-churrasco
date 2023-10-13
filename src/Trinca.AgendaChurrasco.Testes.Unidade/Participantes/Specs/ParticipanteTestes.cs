using FluentAssertions;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Testes.Unidade.Participantes.Builders;

namespace Trinca.AgendaChurrasco.Testes.Unidade.Participantes.Specs;

public class ParticipanteTestes
{
    [Test]
    public void AtualizarPago_DeveAtualizarEstadoEntidade()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => false)
            .Generate();
        
        participante.AtualizarPago();

        participante.Pago.Should().BeTrue();
    }
    
    [Test]
    public void AtualizarNaoPago_DeveAtualizarEstadoEntidade()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => true)
            .Generate();
        
        participante.AtualizarNaoPago();

        participante.Pago.Should().BeFalse();
    }
}