using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trinca.AgendaChurrasco.Domain.Participantes;

namespace Trinca.AgendaChurrasco.Data.EntityConfiguration;

public class ParticipanteConfiguration : IEntityTypeConfiguration<Participante>
{
    public void Configure(EntityTypeBuilder<Participante> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.Valor)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.HasOne(x => x.Churrasco)
            .WithMany(x => x.Participantes)
            .HasForeignKey(x => x.ChurrascoId);
    }
}