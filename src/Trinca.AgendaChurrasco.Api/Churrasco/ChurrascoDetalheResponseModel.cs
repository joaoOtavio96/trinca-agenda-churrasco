using Trinca.AgendaChurrasco.Api.Participante;

namespace Trinca.AgendaChurrasco.Api.Churrasco;

public class ChurrascoDetalheResponseModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Observacao { get; set; }
    public DateTime Data { get; set; }
    public decimal ValorSugeridoSemBebida { get; set; }
    public decimal ValorSugeridoComBebida { get; set; }
    public decimal ValorTotal { get; set; }
    public int ParticipantesTotal { get; set; }
    
    public IList<ParticipanteResponseModel> Participantes { get; set; }
}