namespace Trinca.AgendaChurrasco.Domain.Participantes;

public interface IParticipanteRepository
{
    Task Adicionar(Participante participante);
    Task Atualizar(Participante participante);
    Task Deletar(Participante participante);
    Task<Participante?> BuscarPorId(Guid id);
}