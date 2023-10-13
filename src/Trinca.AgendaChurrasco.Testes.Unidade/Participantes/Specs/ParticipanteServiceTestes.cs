using FakeItEasy;
using FakeItEasy.AutoFakeIt;
using FluentAssertions;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Participantes;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;
using Trinca.AgendaChurrasco.Testes.Unidade.Churrascos.Builders;
using Trinca.AgendaChurrasco.Testes.Unidade.Participantes.Builders;

namespace Trinca.AgendaChurrasco.Testes.Unidade.Participantes.Specs;

public class ParticipanteServiceTestes
{
    private ParticipanteService _service = null!;
    private AutoFakeIt _autoFakeIt = null!;

    [SetUp]
    public void SetUp()
    {
        _autoFakeIt = new AutoFakeIt();
        _service = _autoFakeIt.Generate<ParticipanteService>();
    }

    [Test]
    public async Task Adicionar_DeveValidarEntidade()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Nome, f => null)
            .RuleFor(x => x.Valor, f => 0)
            .Generate();
        var mensagensEsperadas = new List<string>
        {
            ParticipanteErrorMessages.NomeDeveSerInformado,
            ParticipanteErrorMessages.ValorDeveSerInformado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);

        var resultado = await _service.Adicionar(participante);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task Adicionar_DeveRetornarErroCasoChurrascoParaVincularNaoSejaEncontrado()
    {
        var participante = new ParticipanteBuilder().Generate();
        var mensagensEsperadas = new List<string>
        {
            ChurrascoErrorMessages.ChurrascoNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoService>().BuscarPorId(A<Guid>._))
            .Returns(null as Churrasco);

        var resultado = await _service.Adicionar(participante);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task Adicionar_DeveChamarRepositorio()
    {
        var participante = new ParticipanteBuilder().Generate();
        var churrascoVincular = new ChurrascoBuilder()
            .RuleFor(x => x.Id, f => participante.ChurrascoId)
            .Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoService>().BuscarPorId(A<Guid>._))
            .Returns(churrascoVincular);

        await _service.Adicionar(participante);
        
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().Adicionar(A<Participante>._))
            .MustHaveHappened();
    }

    [Test]
    public async Task Deletar_DeveValidarSePrticipanteExiste()
    {
        var participante = new ParticipanteBuilder().Generate();
        var mensagensEsperadas = new List<string>
        {
            ParticipanteErrorMessages.ParticipanteNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(null as Participante);

        var resultado = await _service.Deletar(participante.Id);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task AtualizarPago_DeveValidarSeParticipanteExiste()
    {
        var participante = new ParticipanteBuilder().Generate();
        var mensagensEsperadas = new List<string>
        {
            ParticipanteErrorMessages.ParticipanteNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(null as Participante);

        var resultado = await _service.AtualizarPago(participante.Id);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task AtualizarPago_DeveAtualizarEstadoEntidade()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => false)
            .Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(participante);

        await _service.AtualizarPago(participante.Id);

        participante.Pago.Should().BeTrue();
    }

    [Test]
    public async Task AtualizarPago_DeveChamarRepositorio()
    {
        var participante = new ParticipanteBuilder().Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(participante);

        await _service.AtualizarPago(participante.Id);
        
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().Atualizar(A<Participante>._))
            .MustHaveHappened();
    }
    
    [Test]
    public async Task AtualizarNaoPago_DeveValidarSeParticipanteExiste()
    {
        var participante = new ParticipanteBuilder().Generate();
        var mensagensEsperadas = new List<string>
        {
            ParticipanteErrorMessages.ParticipanteNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(null as Participante);

        var resultado = await _service.AtualizarNaoPago(participante.Id);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task AtualizarNaoPago_DeveAtualizarEstadoEntidade()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => true)
            .Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(participante);

        await _service.AtualizarNaoPago(participante.Id);

        participante.Pago.Should().BeFalse();
    }

    [Test]
    public async Task AtualizarNaoPago_DeveChamarRepositorio()
    {
        var participante = new ParticipanteBuilder().Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().BuscarPorId(A<Guid>._))
            .Returns(participante);

        await _service.AtualizarNaoPago(participante.Id);
        
        A.CallTo(() => _autoFakeIt.Resolve<IParticipanteRepository>().Atualizar(A<Participante>._))
            .MustHaveHappened();
    }
}