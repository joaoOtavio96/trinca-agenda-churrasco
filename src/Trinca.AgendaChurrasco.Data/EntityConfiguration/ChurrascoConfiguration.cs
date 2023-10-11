using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Data.EntityConfiguration;

public class ChurrascoConfiguration : IEntityTypeConfiguration<Churrasco>
{
    public void Configure(EntityTypeBuilder<Churrasco> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(x => x.Data)
            .IsRequired();
        builder.Property(x => x.ValorSugeridoSemBebida)
            .IsRequired()
            .HasColumnType("decimal(5,2)");
        builder.Property(x => x.ValorSugeridoComBebida)
            .IsRequired()
            .HasColumnType("decimal(5,2)");
    }
}