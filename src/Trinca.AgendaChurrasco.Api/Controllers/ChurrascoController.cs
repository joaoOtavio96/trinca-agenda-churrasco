using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trinca.AgendaChurrasco.Api.Churrascos;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChurrascoController : ControllerBase
{
    private readonly IChurrascoService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ChurrascoController> _logger;

    public ChurrascoController(IChurrascoService service, IMapper mapper, ILogger<ChurrascoController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var churrascos = await _service.Listar();
            
            var churrascoListarResponse = _mapper.Map<IEnumerable<ChurrascoResponse>>(churrascos);
            
            return Ok(churrascoListarResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            return StatusCode(500);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var churrasco = await _service.BuscarPorId(id);

            if (churrasco is null)
                return NotFound();

            var churrascoDetalhe = _mapper.Map<ChurrascoDetalheResponse>(churrasco);
    
            return Ok(churrascoDetalhe);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(ChurrascoRequest churrascoRequest)
    {
        try
        {
            var churrasco = _mapper.Map<Churrasco>(churrascoRequest);
            
            var resultado = await _service.Adicionar(churrasco);

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(ChurrascoRequest churrascoRequest, Guid id)
    {
        try
        {
            var churrasco = _mapper
                .Map<Churrasco>(
                    churrascoRequest, 
                    opts => opts.Items["Id"] = id
                    );

            var resultado = await _service.Atualizar(churrasco);

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
}