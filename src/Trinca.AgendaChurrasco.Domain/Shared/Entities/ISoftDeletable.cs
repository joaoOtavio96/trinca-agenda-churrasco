namespace Trinca.AgendaChurrasco.Domain.Shared.Entities;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
}