using Microsoft.AspNetCore.Mvc;
using Trinca.AgendaChurrasco.Api.Models;
using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Services;

namespace Trinca.AgendaChurrasco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChurrascoController : ControllerBase
{
    private readonly IChurrascoService _service;

    public ChurrascoController(IChurrascoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ChurrascoRequestModel churrascoModel)
    {
        try
        {
            var churrasco = new Churrasco()
            {
                Titulo = churrascoModel.Titulo,
                Descricao = churrascoModel.Descricao,
                Observacao = churrascoModel.Observacao,
                ValorSugeridoSemBebida = churrascoModel.ValorSugeridoSemBebida,
                ValorSugeridoComBebida = churrascoModel.ValorSugeridoComBebida
            };

            var resultado = await _service.Adicionar(churrasco);

            if (resultado.PossuiErros)
            {
                return BadRequest(resultado);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put(ChurrascoRequestModel churrascoModel)
    {
        try
        {
            var churrasco = new Churrasco()
            {
                Titulo = churrascoModel.Titulo,
                Descricao = churrascoModel.Descricao,
                Observacao = churrascoModel.Observacao,
                ValorSugeridoSemBebida = churrascoModel.ValorSugeridoSemBebida,
                ValorSugeridoComBebida = churrascoModel.ValorSugeridoComBebida
            };

            var resultado = await _service.Atualizar(churrasco);

            if (resultado.PossuiErros)
            {
                return BadRequest(resultado);
            }

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.Deletar(id);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}