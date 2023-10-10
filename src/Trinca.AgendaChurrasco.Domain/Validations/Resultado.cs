namespace Trinca.AgendaChurrasco.Domain.Validations;

public class Resultado
{
    public Resultado() { }
    public Resultado(IEnumerable<string> mensagens) =>
        Erros = mensagens
            .Select(x => new Erro { Mensagem = x })
            .ToList();

    public Resultado(string mensagem) =>
        Erros.Add(new Erro { Mensagem = mensagem });
    
    public bool PossuiErros => Erros.Any();
    public IList<Erro> Erros { get; set; } = new List<Erro>();
}

public class Erro
{
    public string Mensagem { get; set; }
}