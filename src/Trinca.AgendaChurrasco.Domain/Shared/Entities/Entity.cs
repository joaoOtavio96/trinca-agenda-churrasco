namespace Trinca.AgendaChurrasco.Domain.Shared.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
}