using System.Net.Http.Json;
using System.Text;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Api.Participantes;
using Trinca.AgendaChurrasco.Domain.Participantes;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;
using Trinca.AgendaChurrasco.Testes.Integracao.Churrascos.Builders;
using Trinca.AgendaChurrasco.Testes.Integracao.Infra;
using Trinca.AgendaChurrasco.Testes.Integracao.Participantes.Builders;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Participantes.Specs;

[TestFixture]
public class ParticipanteApiTestes : TesteBase
{
    [Test]
    public async Task Post_DeveInserirNoBanco()
    {
        var churrasco = new ChurrascoBuilder()
            .RuleFor(x => x.IsDeleted, f => false)
            .Generate();
        await DbContext.AddAsync(churrasco);
        await DbContext.SaveChangesAsync();
        var participante = new ParticipanteRequest()
        {
            ChurrascoId = churrasco.Id,
            Nome = new Faker().Random.String2(10),
            Pago = false,
            Valor = 20
        };
        var participanteJson = JsonConvert.SerializeObject(participante);
        
        var conteudo = new StringContent(participanteJson, Encoding.Default, "application/json");
        await HttpClient.PostAsync("/participante", conteudo);

        var participantes = DbContext.Participantes.ToList();
        participantes.Should().HaveCount(1);
    }

    [Test]
    public async Task Post_DeveRetornarErrosValidacao()
    {
        var participante = new ParticipanteRequest()
        {
            Nome = "",
            Pago = false,
            Valor = 0
        };
        var participanteJson = JsonConvert.SerializeObject(participante);
        
        var conteudo = new StringContent(participanteJson, Encoding.Default, "application/json");
        var response = await HttpClient.PostAsync("/participante", conteudo);
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ParticipanteErrorMessages.NomeDeveSerInformado },
                new { Mensagem = ParticipanteErrorMessages.ValorDeveSerInformado }
            }
        });
    }

    [Test]
    public async Task Delete_DeveDeletarDoBanco()
    {
        var participante = new ParticipanteBuilder().Generate();
        await DbContext.AddAsync(participante);
        await DbContext.SaveChangesAsync();

        await HttpClient.DeleteAsync($"/participante/{participante.Id}");

        var existeParticipante = await DbContext.Participantes.AnyAsync(x => x.Id == participante.Id);
        existeParticipante.Should().BeFalse();
    }
    
    [Test]
    public async Task Delete_DeveRetornarErroCasoParticipanteNaoExista()
    {
        var participante = new ParticipanteBuilder().Generate();

        var response = await HttpClient.DeleteAsync($"/participante/{participante.Id}");
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ParticipanteErrorMessages.ParticipanteNaoEncontrado }
            }
        });
    }

    [Test]
    public async Task PutPago_DeveMudarEstadoParticipanteParaPago()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => false)
            .Generate();
        await DbContext.AddAsync(participante);
        await DbContext.SaveChangesAsync();

        await HttpClient.PutAsync($"/participante/{participante.Id}/pago", null);

        var participanteAlterado = await DbContext.Participantes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == participante.Id);
        participanteAlterado.Pago.Should().BeTrue();
    }

    [Test]
    public async Task PutPago_DeveRetornarErroCasoParticipanteNaoExista()
    {
        var participante = new ParticipanteBuilder().Generate();

        var response = await HttpClient.PutAsync($"/participante/{participante.Id}/pago", null);
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ParticipanteErrorMessages.ParticipanteNaoEncontrado }
            }
        });
    }
    
    [Test]
    public async Task Deleteago_DeveMudarEstadoParticipanteParaPago()
    {
        var participante = new ParticipanteBuilder()
            .RuleFor(x => x.Pago, f => true)
            .Generate();
        await DbContext.AddAsync(participante);
        await DbContext.SaveChangesAsync();

        await HttpClient.DeleteAsync($"/participante/{participante.Id}/pago");

        var participanteAlterado = await DbContext.Participantes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == participante.Id);
        participanteAlterado.Pago.Should().BeFalse();
    }

    [Test]
    public async Task DeletePago_DeveRetornarErroCasoParticipanteNaoExista()
    {
        var participante = new ParticipanteBuilder().Generate();

        var response = await HttpClient.DeleteAsync($"/participante/{participante.Id}/pago");
        var resultado = await response.Content.ReadFromJsonAsync<Resultado>();

        resultado.Should().BeEquivalentTo(new
        {
            Erros = new[]
            {
                new { Mensagem = ParticipanteErrorMessages.ParticipanteNaoEncontrado }
            }
        });
    }
}