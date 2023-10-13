using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Trinca.AgendaChurrasco.Api.Churrascos;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;
using Trinca.AgendaChurrasco.Testes.Integracao.Infra;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Churrascos.Specs;

[TestFixture]
public class ChurrascoApiTestes : TesteBase
{
    [Test]
    public async Task Post_DeveInserirNoBanco()
    {
        var churrasco = new ChurrascoRequest()
        {
            Titulo = "titulo",
            Descricao = "descricao",
            Observacao = "observacao",
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
            Descricao = "descricao",
            Observacao = "observacao",
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
                new { Mensagem = "O titulo não pode ser vazio" },
                new { Mensagem = "A data do churrasco não pode ser uma data passada" },
                new { Mensagem = "Deve ser informado um valor sugerido" },
            }
        });
    }

    [Test]
    public async Task Put_DeveAtualizarNoBanco()
    {
        
    }
}