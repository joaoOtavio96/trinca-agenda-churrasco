using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Repositories;

namespace Trinca.AgendaChurrasco.Data.Repository;

public class ChurrascoRepository : IChurrascoRepository
{
    private readonly AgendaChurrascoDbContext _context;

    public ChurrascoRepository(AgendaChurrascoDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Churrasco churrasco)
    {
        await _context.AddAsync(churrasco);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Churrasco churrasco)
    {
        _context.Update(churrasco);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Churrasco churrasco)
    {
        _context.Remove(churrasco);
        await _context.SaveChangesAsync();
    }

    public async Task<Churrasco?> BuscarPorId(Guid id)
    {
        return await _context.Churrascos
            .Include(x => x.Participantes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IList<Churrasco>> Listar()
    {
        return await _context.Churrascos
            .Include(x => x.Participantes)
            .ToListAsync();
    }
}