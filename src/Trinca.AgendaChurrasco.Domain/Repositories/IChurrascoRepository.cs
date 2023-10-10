using Trinca.AgendaChurrasco.Domain.Entities;

namespace Trinca.AgendaChurrasco.Domain.Repositories;

public interface IChurrascoRepository
{
    Task Adicionar(Churrasco churrasco);
    Task Atualizar(Churrasco churrasco);
    Task Deletar(Churrasco churrasco);
    Task<Churrasco?> BuscarPorId(Guid id);
    Task<IList<Churrasco>> Listar();
}