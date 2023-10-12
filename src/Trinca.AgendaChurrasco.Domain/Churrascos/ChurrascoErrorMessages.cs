namespace Trinca.AgendaChurrasco.Domain.Churrascos;

public static class ChurrascoErrorMessages
{
    public static readonly string TituloVazio = "O titulo não pode ser vazio";
    public static readonly string TituloTamanhoMaximo = "O titulo não pode passar de {MaxLength} caracteres";
    public static readonly string DescricaoTamanhoMaximo = "A descrição não pode passar de {MaxLength} caracteres";
    public static readonly string ObservacaoTamanhoMaximo = "A observação não pode passar de {MaxLength} caracteres";
    public static readonly string DataObrigatoria = "A data deve ser informada";
    public static readonly string DataDeveSerFutura = "A data do churrasco não pode ser uma data passada";
    public static readonly string ValorSugeridoDeveSerInformado = "Deve ser informado um valor sugerido";
    public static readonly string ChurrascoNaoEncontrado = "Churrasco não encontrado";
}