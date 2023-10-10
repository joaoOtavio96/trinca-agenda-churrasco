using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Domain.Participante;

namespace Trinca.AgendaChurrasco.Data.Repository;

public class ParticipanteRepository : IParticipanteRepository
{
    private readonly AgendaChurrascoDbContext _context;
    
    public ParticipanteRepository(AgendaChurrascoDbContext context)
    {
        _context = context;
    }
    
    public async Task Adicionar(ParticipanteModel participanteModel)
    {
        await _context.AddAsync(participanteModel);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(ParticipanteModel participanteModel)
    {
        _context.Update(participanteModel);
        await _context.SaveChangesAsync();
    }

    public async Task Deletar(ParticipanteModel participanteModel)
    {
        _context.Participantes.Remove(participanteModel);
        await _context.SaveChangesAsync();
    }

    public async Task<ParticipanteModel?> BuscarPorId(Guid id)
    {
        return await _context.Participantes.FirstOrDefaultAsync(x => x.Id == id);
    }
}