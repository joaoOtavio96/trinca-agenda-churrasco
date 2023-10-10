using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Validations;

namespace Trinca.AgendaChurrasco.Domain.Services;

public interface IParticipanteService
{
    Task<Resultado> Adicionar(Participante participante);
    Task<Resultado> Deletar(Guid id);
    Task<Resultado> AtualizarPago(Guid id);
    Task<Resultado> AtualizarNaoPago(Guid id);
}