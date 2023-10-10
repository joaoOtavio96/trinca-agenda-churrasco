using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Repositories;

namespace Trinca.AgendaChurrasco.Data.Repository;

public class ParticipanteRepository : IParticipanteRepository
{
    private readonly AgendaChurrascoDbContext _context;
    
    public ParticipanteRepository(AgendaChurrascoDbContext context)
    {
        _context = context;
    }
    
    public async Task Adicionar(Participante participante)
    {
        await _context.AddAsync(participante);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Participante participante)
    {
        _context.Update(participante);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(Participante participante)
    {
        _context.Participantes.Remove(participante);
        await _context.SaveChangesAsync();
    }

    public async Task<Participante?> BuscarPorId(Guid id)
    {
        return await _context.Participantes.FirstOrDefaultAsync(x => x.Id == id);
    }
}