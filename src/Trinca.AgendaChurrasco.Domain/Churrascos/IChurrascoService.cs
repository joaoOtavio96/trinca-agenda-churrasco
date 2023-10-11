using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Churrascos;

public interface IChurrascoService
{
    Task<Resultado> Adicionar(Churrasco churrasco);
    Task<Resultado> Atualizar(Churrasco churrasco);
    Task<Resultado> Deletar(Guid id);
    Task<Churrasco?> BuscarPorId(Guid id);
    Task<IList<Churrasco>> Listar();
}