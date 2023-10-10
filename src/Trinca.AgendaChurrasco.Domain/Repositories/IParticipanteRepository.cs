using Trinca.AgendaChurrasco.Domain.Entities;

namespace Trinca.AgendaChurrasco.Domain.Repositories;

public interface IParticipanteRepository
{
    Task Adicionar(Participante participante);
    Task Atualizar(Participante participante);
    Task Deletar(Participante participante);
    Task<Participante?> BuscarPorId(Guid id);
}