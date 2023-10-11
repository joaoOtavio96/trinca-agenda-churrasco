namespace Trinca.AgendaChurrasco.Domain.Churrascos;

public interface IChurrascoRepository
{
    Task Adicionar(Churrasco churrasco);
    Task Atualizar(Churrasco churrasco);
    Task Deletar(Churrasco churrasco);
    Task<Churrasco?> BuscarPorId(Guid id);
    Task<IList<Churrasco>> Listar();
}