namespace Trinca.AgendaChurrasco.Domain.Entities;

public class Participante : Entity
{
    public Participante(string nome, decimal valor, bool pago, Guid churrascoId)
    {
        Nome = nome;
        Valor = valor;
        Pago = pago;
        ChurrascoId = churrascoId;
    }
    public string Nome { get; private set; }
    public decimal Valor { get; private set; }
    public bool Pago { get; private set; }

    public Guid ChurrascoId { get; private set; }
    public virtual Churrasco Churrasco { get; set; }

    public void AtualizarPago() => Pago = true;
    public void AtualizarNaoPago() => Pago = false;

}