namespace Trinca.AgendaChurrasco.Domain.Entities;

public class Churrasco : Entity
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Observacao { get; set; }
    public DateTime Data { get; set; }
    public decimal ValorSugeridoSemBebida { get; set; }
    public decimal ValorSugeridoComBebida { get; set; }
    
    public virtual ICollection<Participante> Participantes { get; set; }
    
}