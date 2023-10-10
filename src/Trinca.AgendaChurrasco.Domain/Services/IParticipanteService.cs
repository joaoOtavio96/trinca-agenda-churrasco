using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Validations;

namespace Trinca.AgendaChurrasco.Domain.Services;

public interface IParticipanteService
{
    Task<Resultado> Adicionar(Participante participante);
    Task Deletar(Guid id);
    Task AtualizarPago(Guid id);
    Task AtualizarNaoPago(Guid id);
}