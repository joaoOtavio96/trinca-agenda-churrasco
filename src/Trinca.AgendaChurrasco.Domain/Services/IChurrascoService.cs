using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Validations;

namespace Trinca.AgendaChurrasco.Domain.Services;

public interface IChurrascoService
{
    Task<Resultado> Adicionar(Churrasco churrasco);
    Task<Resultado> Atualizar(Churrasco churrasco);
    Task Deletar(Guid id);
    Task<Churrasco?> BuscarPorId(Guid id);
    Task<IList<Churrasco>> Listar();
}