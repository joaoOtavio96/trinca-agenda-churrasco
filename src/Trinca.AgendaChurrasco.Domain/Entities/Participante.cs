namespace Trinca.AgendaChurrasco.Domain.Entities;

public class Participante : Entity
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }

    public Guid ChurrascoId { get; set; }
    public virtual Churrasco Churrasco { get; set; }
    
}