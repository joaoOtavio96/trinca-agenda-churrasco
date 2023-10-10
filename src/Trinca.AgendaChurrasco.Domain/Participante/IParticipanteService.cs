using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Participante;

public interface IParticipanteService
{
    Task<Resultado> Adicionar(ParticipanteModel participanteModel);
    Task<Resultado> Deletar(Guid id);
    Task<Resultado> AtualizarPago(Guid id);
    Task<Resultado> AtualizarNaoPago(Guid id);
}