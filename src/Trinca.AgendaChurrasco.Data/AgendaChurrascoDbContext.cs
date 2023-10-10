using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Data.EntityConfiguration;
using Trinca.AgendaChurrasco.Domain.Churrasco;
using Trinca.AgendaChurrasco.Domain.Participante;

namespace Trinca.AgendaChurrasco.Data;

public class AgendaChurrascoDbContext : DbContext
{
    public AgendaChurrascoDbContext(DbContextOptions<AgendaChurrascoDbContext> options) : base(options) { }
    
    public DbSet<ChurrascoModel> Churrascos { get; set; }
    public DbSet<ParticipanteModel> Participantes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChurrascoConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());
    }
}