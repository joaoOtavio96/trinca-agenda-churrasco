using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Domain.Churrasco;

namespace Trinca.AgendaChurrasco.Data.Repository;

public class ChurrascoRepository : IChurrascoRepository
{
    private readonly AgendaChurrascoDbContext _context;

    public ChurrascoRepository(AgendaChurrascoDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(ChurrascoModel churrascoModel)
    {
        await _context.AddAsync(churrascoModel);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(ChurrascoModel churrascoModel)
    {
        _context.Update(churrascoModel);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(ChurrascoModel churrascoModel)
    {
        _context.Remove(churrascoModel);
        await _context.SaveChangesAsync();
    }

    public async Task<ChurrascoModel?> BuscarPorId(Guid id)
    {
        return await _context.Churrascos
            .Include(x => x.Participantes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<ChurrascoModel>> Listar()
    {
        return await _context.Churrascos
            .Include(x => x.Participantes)
            .ToListAsync();
    }
}