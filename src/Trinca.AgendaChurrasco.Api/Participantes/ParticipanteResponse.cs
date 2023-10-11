namespace Trinca.AgendaChurrasco.Api.Participantes;

public class ParticipanteResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }
}