using System.Net.Http.Json;
using System.Text;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Api.Churrascos;
using Trinca.AgendaChurrasco.Api.Participantes;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;
using Trinca.AgendaChurrasco.Testes.Integracao.Churrascos.Builders;
using Trinca.AgendaChurrasco.Testes.Integracao.Infra;
using Trinca.AgendaChurrasco.Testes.Integracao.Participantes.Builders;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Churrascos.Specs;

[TestFixture]
public class ChurrascoApiTestes : TesteBase
{
    [TearDown]
    public async Task TearDown()
    {
        await RespawnDb.ResetAsync(DbContext.Database.GetConnectionString());
    }
    
    [Test]
    public async Task Post_DeveInserirNoBanco()
    {
        var churrasco = new ChurrascoRequest()
        {
            Titulo = new Faker().Random.String2(10),
            Descricao = new Faker().Random.String2(10),
            Observacao = new Faker().Random.String2(10),
            Data = DateTime.Today,
            ValorSugeridoComBebida = 30,
            ValorSugeridoSemBebida = 20
        };
        
        var churrascoJson = JsonConvert.SerializeObject(churrasco);

        var conteudo = new StringContent(churrascoJson, Encoding.Default, "application/json");
        await HttpClient.PostAsync("/churrasco", conteudo);

        var churrascos = DbContext.Churrascos.ToList();
        churrascos.Should().HaveCount(1);
    }

    [Test]
    public async Task Post_DeveRetornarErrosValidacao()
    {
        var churrasco = new ChurrascoRequest()
        {
            Titulo = "",
            Descricao = new Faker().Random.String2(10),
            Observacao = new Faker().Random.String2(10),
            Data = DateTime.Today.AddDays(-1),
            ValorSugeridoComBebida = 0,
            ValorSugeridoSemBebida = 20
        };
        var churrascoJson = JsonConvert.SerializeObject(churrasco);

        var conteudo = new StringContent(churrascoJson, Encoding.Default, "application/json");
        var response = await HttpClient.PostAsync("/churrasco", conteudo);
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ChurrascoErrorMessages.TituloVazio },
                new { Mensagem = ChurrascoErrorMessages.DataDeveSerFutura },
                new { Mensagem = ChurrascoErrorMessages.ValorSugeridoDeveSerInformado },
            }
        });
    }

    [Test]
    public async Task Put_DeveAtualizarNoBanco()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        await DbContext.AddAsync(churrasco);
        await DbContext.SaveChangesAsync();
        var churrascoAtualizadoRequest = new ChurrascoRequest()
        {
            Titulo = new Faker().Random.String2(10),
            Descricao = new Faker().Random.String2(10),
            Observacao = new Faker().Random.String2(10),
            Data = DateTime.Today,
            ValorSugeridoComBebida = 10,
            ValorSugeridoSemBebida = 20
        };
        var churrascoAtualizadoJson = JsonConvert.SerializeObject(churrascoAtualizadoRequest);
        var conteudo = new StringContent(churrascoAtualizadoJson, Encoding.Default, "application/json");

        await HttpClient.PutAsync($"/churrasco/{churrasco.Id}", conteudo);

        var churrascoAtualizado = await DbContext.Churrascos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == churrasco.Id);
        
        churrascoAtualizado.Should().BeEquivalentTo(new
        {
            churrascoAtualizadoRequest.Titulo,
            churrascoAtualizadoRequest.Descricao,
            churrascoAtualizadoRequest.Observacao,
            churrascoAtualizadoRequest.Data,
            churrascoAtualizadoRequest.ValorSugeridoComBebida,
            churrascoAtualizadoRequest.ValorSugeridoSemBebida
        });
    }
    
    [Test]
    public async Task Put_DeveRetornarErrosValidacao()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        await DbContext.AddAsync(churrasco);
        await DbContext.SaveChangesAsync();
        var churrascoRequest = new ChurrascoRequest()
        {
            Titulo = "",
            Descricao = new Faker().Random.String2(10),
            Observacao = new Faker().Random.String2(10),
            Data = DateTime.Today.AddDays(-1),
            ValorSugeridoComBebida = 0,
            ValorSugeridoSemBebida = 20
        };
        var churrascoJson = JsonConvert.SerializeObject(churrascoRequest);

        var conteudo = new StringContent(churrascoJson, Encoding.Default, "application/json");
        var response = await HttpClient.PutAsync($"/churrasco/{churrasco.Id}", conteudo);
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ChurrascoErrorMessages.TituloVazio },
                new { Mensagem = ChurrascoErrorMessages.DataDeveSerFutura },
                new { Mensagem = ChurrascoErrorMessages.ValorSugeridoDeveSerInformado },
            }
        });
    }

    [Test]
    public async Task Deletar_DeveDeletarNoBanco()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        await DbContext.AddAsync(churrasco);
        await DbContext.SaveChangesAsync();

        await HttpClient.DeleteAsync($"/churrasco/{churrasco.Id}");

        var existeChurrasco = await DbContext.Churrascos.AnyAsync();

        existeChurrasco.Should().BeFalse();
    }

    [Test]
    public async Task Deletar_DeveRetornarMensagensDeErro()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        var response = await HttpClient.DeleteAsync($"/churrasco/{churrasco.Id}");
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();
        
        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ChurrascoErrorMessages.ChurrascoNaoEncontrado }
            }
        });
    }

    [Test]
    public async Task Get_DeveListarChurrascos()
    {
        var churrascos = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate(5);
        await DbContext.AddRangeAsync(churrascos);
        await DbContext.SaveChangesAsync();
        var churrascosEsperados = churrascos.Select(x => new ChurrascoResponse()
        {
            Titulo = x.Titulo,
            Data = x.Data,
            Id = x.Id,
            ParticipantesTotal = 0,
            ValorTotal = 0
        });

        var response = await HttpClient.GetAsync("/churrasco");
        var resultado = await response.Content.ReadFromJsonAsync<IEnumerable<ChurrascoResponse>>();

        resultado.Should().BeEquivalentTo(churrascosEsperados);
    }

    [Test]
    public async Task Get_DeveRetornarDetalheChurrasco()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        churrasco.Participantes = new ParticipanteBuilder().Generate(4);
        await DbContext.AddAsync(churrasco);
        await DbContext.SaveChangesAsync();
        var churrascoEsperado = new ChurrascoDetalheResponse()
        {
            Titulo = churrasco.Titulo,
            Descricao = churrasco.Descricao,
            Observacao = churrasco.Observacao,
            Data = churrasco.Data,
            ValorSugeridoSemBebida = Math.Round(churrasco.ValorSugeridoSemBebida, 2, MidpointRounding.AwayFromZero),
            ValorSugeridoComBebida = Math.Round(churrasco.ValorSugeridoComBebida, 2, MidpointRounding.AwayFromZero),
            ParticipantesTotal = churrasco.ParticipantesTotal(),
            ValorTotal = Math.Round(churrasco.CalcularValorTotal(), 2, MidpointRounding.AwayFromZero),
            Participantes = churrasco.Participantes.Select(x => new ParticipanteResponse()
            {
                Id = x.Id,
                Nome = x.Nome,
                Pago = x.Pago,
                Valor = Math.Round(x.Valor, 2, MidpointRounding.AwayFromZero)
            }).ToList()
        };

        var response = await HttpClient.GetAsync($"/churrasco/{churrasco.Id}");
        var resultado = await response.Content.ReadFromJsonAsync<ChurrascoDetalheResponse>();

        resultado.Should().BeEquivalentTo(churrascoEsperado);
    }
}