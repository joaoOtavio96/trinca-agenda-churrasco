namespace Trinca.AgendaChurrasco.Api.Participantes;

public class ParticipanteRequest
{
    public Guid ChurrascoId { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }
}