namespace Trinca.AgendaChurrasco.Api.Churrasco;

public class ChurrascoResponseModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public DateTime Data { get; set; }
    public decimal ValorTotal { get; set; }
    public int ParticipantesTotal { get; set; }
}