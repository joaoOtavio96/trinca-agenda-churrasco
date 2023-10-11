using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Participantes;

public interface IParticipanteService
{
    Task<Resultado> Adicionar(Participante participante);
    Task<Resultado> Deletar(Guid id);
    Task<Resultado> AtualizarPago(Guid id);
    Task<Resultado> AtualizarNaoPago(Guid id);
}