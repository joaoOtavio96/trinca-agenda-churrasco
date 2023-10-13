using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Participantes;

public class ParticipanteService : IParticipanteService
{
    private readonly IParticipanteRepository _repository;
    private readonly IChurrascoService _churrascoService;
    
    public ParticipanteService(IParticipanteRepository repository, IChurrascoService churrascoService)
    {
        _repository = repository;
        _churrascoService = churrascoService;
    }
    
    public async Task<Resultado> Adicionar(Participante participante)
    {
        var validator = new ParticipanteValidator();
        var validationResult = validator.Validate(participante);

        if (!validationResult.IsValid)
            return new Resultado(validationResult.Errors.Select(x => x.ErrorMessage));

        var churrasco = await _churrascoService.BuscarPorId(participante.ChurrascoId);
        
        if(churrasco is null)
            return new Resultado(ChurrascoErrorMessages.ChurrascoNaoEncontrado);
        
        await _repository.Adicionar(participante);

        return new Resultado();
    }

    public async Task<Resultado> Deletar(Guid id)
    {
        var participanteDeletar = await _repository.BuscarPorId(id);

        if (participanteDeletar is null)
            return new Resultado(ParticipanteErrorMessages.ParticipanteNaoEncontrado);
        
        await _repository.Deletar(participanteDeletar);

        return new Resultado();
    }

    public async Task<Resultado> AtualizarPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        if(participante is null)
            return new Resultado(ParticipanteErrorMessages.ParticipanteNaoEncontrado);
        
        participante.AtualizarPago();
        await _repository.Atualizar(participante);

        return new Resultado();
    }

    public async Task<Resultado> AtualizarNaoPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        if(participante is null)
            return new Resultado(ParticipanteErrorMessages.ParticipanteNaoEncontrado);
        
        participante.AtualizarNaoPago();
        await _repository.Atualizar(participante);

        return new Resultado();
    }
}