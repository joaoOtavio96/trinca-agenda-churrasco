namespace Trinca.AgendaChurrasco.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
}