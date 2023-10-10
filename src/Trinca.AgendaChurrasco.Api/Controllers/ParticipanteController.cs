using Microsoft.AspNetCore.Mvc;
using Trinca.AgendaChurrasco.Api.Models;
using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Services;

namespace Trinca.AgendaChurrasco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParticipanteController : ControllerBase
{
    private readonly IParticipanteService _service;

    public ParticipanteController(IParticipanteService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ParticipanteRequestModel participanteModel)
    {
        try
        {
            var participante = new Participante(
                participanteModel.Nome, 
                participanteModel.Valor, 
                participanteModel.Pago,
                participanteModel.ChurrascoId);

            var resultado = await _service.Adicionar(participante);

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

    [HttpDelete("{id}")]
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

    [HttpPut("{id}/pago")]
    public async Task<IActionResult> PutPago(Guid id)
    {
        try
        {
            await _service.AtualizarPago(id);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpDelete("{id}/pago")]
    public async Task<IActionResult> DeletePago(Guid id)
    {
        try
        {
            await _service.AtualizarNaoPago(id);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}