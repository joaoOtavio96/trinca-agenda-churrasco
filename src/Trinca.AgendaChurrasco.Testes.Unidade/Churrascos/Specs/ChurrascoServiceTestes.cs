using FakeItEasy;
using FakeItEasy.AutoFakeIt;
using FluentAssertions;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;
using Trinca.AgendaChurrasco.Testes.Unidade.Churrascos.Builders;

namespace Trinca.AgendaChurrasco.Testes.Unidade.Churrascos.Specs;

public class ChurrascoServiceTestes
{
    private ChurrascoService _service = null!;
    private AutoFakeIt _autoFakeIt = null!;

    [SetUp]
    public void SetUp()
    {
        _autoFakeIt = new AutoFakeIt();
        _service = _autoFakeIt.Generate<ChurrascoService>();
    }

    [Test]
    public async Task Adicionar_DeveValidarEntidade()
    {
        var churrasco = new ChurrascoBuilder()
            .ComValoresInvalidos()
            .Generate();
        var mensagensEsperadas = new List<string>
        {
            ChurrascoErrorMessages.TituloVazio,
            ChurrascoErrorMessages.DescricaoTamanhoMaximo.Replace("{MaxLength}", "500"),
            ChurrascoErrorMessages.DataDeveSerFutura,
            ChurrascoErrorMessages.ValorSugeridoDeveSerInformado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);

        var resultado = await _service.Adicionar(churrasco);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }
    
    [Test]
    public async Task Adicionar_DeveChamarRepositorio()
    {
        var service = _autoFakeIt.Generate<ChurrascoService>();
        var churrasco = new ChurrascoBuilder().Generate();
        
        await service.Adicionar(churrasco);

        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().Adicionar(A<Churrasco>._))
            .MustHaveHappened();
    }
    
    [Test]
    public async Task Atualizar_DeveValidarEntidade()
    {
        var churrasco = new ChurrascoBuilder()
            .ComValoresInvalidos()
            .Generate();
        var mensagensEsperadas = new List<string>
        {
            ChurrascoErrorMessages.TituloVazio,
            ChurrascoErrorMessages.DescricaoTamanhoMaximo.Replace("{MaxLength}", "500"),
            ChurrascoErrorMessages.DataDeveSerFutura,
            ChurrascoErrorMessages.ValorSugeridoDeveSerInformado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);

        var resultado = await _service.Atualizar(churrasco);

        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task Atualizar_DeveRetornarErroCasoChurrascoNaoSejaEncontrado()
    {
        var churrasco = new ChurrascoBuilder()
            .Generate();
        var mensagensEsperadas = new List<string>
        {
            ChurrascoErrorMessages.ChurrascoNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .Returns(null as Churrasco);
        
        var resultado = await _service.Atualizar(churrasco);
        
        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }

    [Test]
    public async Task Atualizar_DeveChamarRepositorio()
    {
        var churrasco = new ChurrascoBuilder().Generate();
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .Returns(new ChurrascoBuilder().Generate());
        
        await _service.Atualizar(churrasco);

        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().Atualizar(A<Churrasco>._))
            .MustHaveHappened();
    }

    [Test]
    public async Task Deletar_DeveRetornarErroCasoChurrascoNaoSejaEncontrado()
    {
        var mensagensEsperadas = new List<string>
        {
            ChurrascoErrorMessages.ChurrascoNaoEncontrado
        };
        var resultadoEsperado = new Resultado(mensagensEsperadas);
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .Returns(null as Churrasco);
        
        var resultado = await _service.Deletar(Guid.NewGuid());
        
        resultado.Should().BeEquivalentTo(resultadoEsperado);
    }
    
    [Test]
    public async Task Deletar_DeveChamarRepositorio()
    {
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .Returns(new ChurrascoBuilder().Generate());
        
        await _service.Deletar(Guid.NewGuid());

        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().Deletar(A<Churrasco>._))
            .MustHaveHappened();
    }

    [Test]
    public async Task BuscarPorId_DeveChamarRepositorio()
    {
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .Returns(new ChurrascoBuilder().Generate());
        
        await _service.BuscarPorId(Guid.NewGuid());
        
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().BuscarPorId(A<Guid>._))
            .MustHaveHappened();
    }

    [Test]
    public async Task Listar_DeveChamarRepositorio()
    {
        await _service.Listar();
        
        A.CallTo(() => _autoFakeIt.Resolve<IChurrascoRepository>().Listar())
            .MustHaveHappened();
    }
}