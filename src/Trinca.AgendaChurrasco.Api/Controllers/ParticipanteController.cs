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
    private readonly ILogger<ParticipanteController> _logger;

    public ParticipanteController(IParticipanteService service, IMapper mapper, ILogger<ParticipanteController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
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
            _logger.LogError(e, e.Message);

            return StatusCode(500);
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
            _logger.LogError(e, e.Message);

            return StatusCode(500);
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
            _logger.LogError(e, e.Message);

            return StatusCode(500);
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
            _logger.LogError(e, e.Message);

            return StatusCode(500);
        }
    }
}