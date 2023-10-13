using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Data.EntityConfiguration;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Participantes;
using Trinca.AgendaChurrasco.Domain.Shared.Entities;

namespace Trinca.AgendaChurrasco.Data;

public class AgendaChurrascoDbContext : DbContext
{
    public AgendaChurrascoDbContext(DbContextOptions<AgendaChurrascoDbContext> options) : base(options) { }
    
    public DbSet<Churrasco> Churrascos { get; set; }
    public DbSet<Participante> Participantes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChurrascoConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());
    }
}