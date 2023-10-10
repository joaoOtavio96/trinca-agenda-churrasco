namespace Trinca.AgendaChurrasco.Api.Participante;

public class ParticipanteResponseModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public bool Pago { get; set; }
}