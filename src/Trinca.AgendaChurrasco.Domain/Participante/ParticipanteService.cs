using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Participante;

public class ParticipanteService : IParticipanteService
{
    private readonly IParticipanteRepository _repository;
    
    public ParticipanteService(IParticipanteRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Resultado> Adicionar(ParticipanteModel participanteModel)
    {
        var validator = new ParticipanteValidator();
        var validationResult = validator.Validate(participanteModel);

        if (!validationResult.IsValid)
            return new Resultado(validationResult.Errors.Select(x => x.ErrorMessage));

        await _repository.Adicionar(participanteModel);

        return new Resultado();
    }

    public async Task<Resultado> Deletar(Guid id)
    {
        var participanteDeletar = await _repository.BuscarPorId(id);

        if (participanteDeletar is null)
            return new Resultado("Participante não encontrado");
        
        await _repository.Deletar(participanteDeletar);

        return new Resultado();
    }

    public async Task<Resultado> AtualizarPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        if(participante is null)
            return new Resultado("Participante não encontrado");
        
        participante.AtualizarPago();
        await _repository.Atualizar(participante);

        return new Resultado();
    }

    public async Task<Resultado> AtualizarNaoPago(Guid id)
    {
        var participante = await _repository.BuscarPorId(id);
        
        if(participante is null)
            return new Resultado("Participante não encontrado");
        
        participante.AtualizarNaoPago();
        await _repository.Atualizar(participante);

        return new Resultado();
    }
}