namespace Trinca.AgendaChurrasco.Domain.Churrasco;

public interface IChurrascoRepository
{
    Task Adicionar(ChurrascoModel churrascoModel);
    Task Atualizar(ChurrascoModel churrascoModel);
    Task Deletar(ChurrascoModel churrascoModel);
    Task<ChurrascoModel?> BuscarPorId(Guid id);
    Task<IList<ChurrascoModel>> Listar();
}