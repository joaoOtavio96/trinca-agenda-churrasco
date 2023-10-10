namespace Trinca.AgendaChurrasco.Api.Participante;

public class ParticipanteRequestViewModel
{
    public Guid ChurrascoId { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }
}