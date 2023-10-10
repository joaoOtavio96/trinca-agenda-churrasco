namespace Trinca.AgendaChurrasco.Api.Models;

public class ParticipanteRequestModel
{
    public Guid ChurrascoId { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }
}