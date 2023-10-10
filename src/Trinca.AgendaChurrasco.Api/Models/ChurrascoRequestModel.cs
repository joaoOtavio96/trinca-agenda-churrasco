namespace Trinca.AgendaChurrasco.Api.Models;

public class ChurrascoRequestModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Observacao { get; set; }
    public decimal ValorSugeridoSemBebida { get; set; }
    public decimal ValorSugeridoComBebida { get; set; }
}