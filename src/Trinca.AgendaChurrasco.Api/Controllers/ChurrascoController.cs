using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trinca.AgendaChurrasco.Api.Churrasco;
using Trinca.AgendaChurrasco.Api.Participante;
using Trinca.AgendaChurrasco.Domain.Churrasco;

namespace Trinca.AgendaChurrasco.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChurrascoController : ControllerBase
{
    private readonly IChurrascoService _service;
    private readonly IMapper _mapper;

    public ChurrascoController(IChurrascoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var churrascos = await _service.Listar();

            var churrascoListarResponse = churrascos
                .Select(x => new ChurrascoResponseViewModel()
                { 
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Data = x.Data,
                    ValorTotal = x.CalcularValorTotal(),
                    ParticipantesTotal = x.ParticipantesTotal()
                });
            
            return Ok(churrascoListarResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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

            var participantes = churrasco.Participantes
                .Select(x => new ParticipanteResponseViewModel()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Valor = x.Valor,
                    Pago = x.Pago
                });

            var churrascoDetalhe = new ChurrascoDetalheResponseViewModel
            {
                Titulo = churrasco.Titulo,
                Descricao = churrasco.Descricao,
                Observacao = churrasco.Observacao,
                Data = churrasco.Data,
                ValorSugeridoComBebida = churrasco.ValorSugeridoComBebida,
                ValorSugeridoSemBebida = churrasco.ValorSugeridoSemBebida,
                ValorTotal = churrasco.CalcularValorTotal(),
                ParticipantesTotal = churrasco.ParticipantesTotal(),
                Participantes = participantes.ToList()
            };
    
            return Ok(churrascoDetalhe);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(ChurrascoRequestViewModel churrascoViewModel)
    {
        try
        {
            var churrasco = _mapper.Map<ChurrascoRequestViewModel, ChurrascoModel>(churrascoViewModel);
            
            var resultado = await _service.Adicionar(churrasco);

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(ChurrascoRequestViewModel churrascoViewModel, Guid id)
    {
        try
        {
            var churrasco = _mapper
                .Map<ChurrascoRequestViewModel, ChurrascoModel>(
                    churrascoViewModel, 
                    opts => opts.Items["Id"] = id
                    );

            var resultado = await _service.Atualizar(churrasco);

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
}