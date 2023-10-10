namespace Trinca.AgendaChurrasco.Domain.Validations;

public class Resultado
{
    public bool PossuiErros => Erros.Any();
    public IList<Erro> Erros { get; set; } = new List<Erro>();
}

public class Erro
{
    public string Mensagem { get; set; }
}