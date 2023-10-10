namespace Trinca.AgendaChurrasco.Domain.Participante;

public interface IParticipanteRepository
{
    Task Adicionar(ParticipanteModel participanteModel);
    Task Atualizar(ParticipanteModel participanteModel);
    Task Deletar(ParticipanteModel participanteModel);
    Task<ParticipanteModel?> BuscarPorId(Guid id);
}