using Trinca.AgendaChurrasco.Domain.Participantes;
using Trinca.AgendaChurrasco.Domain.Shared.Entities;

namespace Trinca.AgendaChurrasco.Domain.Churrascos;

public class Churrasco : Entity
{
    public Churrasco(
        string titulo, 
        string descricao, 
        string observacao, 
        DateTime data, 
        decimal valorSugeridoSemBebida, 
        decimal valorSugeridoComBebida)
    {
        Titulo = titulo;
        Descricao = descricao;
        Observacao = observacao;
        Data = data;
        ValorSugeridoComBebida = valorSugeridoComBebida;
        ValorSugeridoSemBebida = valorSugeridoSemBebida;
    }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Observacao { get; private set; }
    public DateTime Data { get; private set; }
    public decimal ValorSugeridoSemBebida { get; private set; }
    public decimal ValorSugeridoComBebida { get; private set; }
    
    public virtual ICollection<Participante> Participantes { get; set; }

    public void Atualizar(Churrasco churrasco)
    {
        Titulo = churrasco.Titulo;
        Descricao = churrasco.Descricao;
        Observacao = churrasco.Observacao;
        Data = churrasco.Data;
    }

    public decimal CalcularValorTotal()
    {
        if (Participantes is null || !Participantes.Any())
            return 0;

        return Participantes.Sum(x => x.Valor);
    }

    public int ParticipantesTotal()
    {
        if (Participantes is null)
            return 0;
        
        return Participantes.Count;
    }
    
}