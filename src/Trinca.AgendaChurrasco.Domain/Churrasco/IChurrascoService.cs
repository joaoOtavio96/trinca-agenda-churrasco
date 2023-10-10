using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Churrasco;

public interface IChurrascoService
{
    Task<Resultado> Adicionar(ChurrascoModel churrascoModel);
    Task<Resultado> Atualizar(ChurrascoModel churrascoModel);
    Task<Resultado> Deletar(Guid id);
    Task<ChurrascoModel?> BuscarPorId(Guid id);
    Task<IList<ChurrascoModel>> Listar();
}