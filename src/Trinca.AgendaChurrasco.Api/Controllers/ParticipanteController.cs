using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trinca.AgendaChurrasco.Api.Participantes;
using Trinca.AgendaChurrasco.Domain.Participantes;

namespace Trinca.AgendaChurrasco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ParticipanteController : ControllerBase
{
    private readonly IParticipanteService _service;
    private readonly IMapper _mapper;

    public ParticipanteController(IParticipanteService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ParticipanteRequest participanteRequest)
    {
        try
        {
            var participante = _mapper.Map<ParticipanteRequest, Participante>(participanteRequest);

            var resultado = await _service.Adicionar(participante);

            if (resultado.PossuiErros)
                return BadRequest(resultado);

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
            var resultado = await _service.Deletar(id);
            
            if (resultado.PossuiErros)
                return BadRequest(resultado);

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
            var resultado = await _service.AtualizarPago(id);
            
            if (resultado.PossuiErros)
                return BadRequest(resultado);

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
            var resultado = await _service.AtualizarNaoPago(id);

            if (resultado.PossuiErros)
                return BadRequest(resultado);
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}