using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Repositories;
using Trinca.AgendaChurrasco.Domain.Validations;

namespace Trinca.AgendaChurrasco.Domain.Services;

public class ParticipanteService : IParticipanteService
{
    private readonly IParticipanteRepository _repository;
    
    public ParticipanteService(IParticipanteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Resultado> Adicionar(Participante participante)
    {
        var validator = new ParticipanteValidator();
        var validationResult = validator.Validate(participante);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(x => new Erro { Mensagem = x.ErrorMessage });
            
            return new Resultado { Erros = erros.ToList() };
        }

        await _repository.Adicionar(participante);

        return new Resultado();
    }

    public async Task Deletar(Guid id)
    {
        var participanteDeletar = await _repository.BuscarPorId(id);

        if (participanteDeletar is null)
            return;
        
        await _repository.Deletar(participanteDeletar);
    }

    public async Task AtualizarPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        participante.AtualizarPago();

        await _repository.Atualizar(participante);
    }

    public async Task AtualizarNaoPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        participante.AtualizarNaoPago();

        await _repository.Atualizar(participante);
    }
}