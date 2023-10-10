using Trinca.AgendaChurrasco.Domain.Shared.Validations;

namespace Trinca.AgendaChurrasco.Domain.Churrasco;

public class ChurrascoService : IChurrascoService
{
    private readonly IChurrascoRepository _repository;

    public ChurrascoService(IChurrascoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Resultado> Adicionar(ChurrascoModel churrascoModel)
    {
        var validator = new ChurrascoValidator();
        var validationResult = validator.Validate(churrascoModel);

        if (!validationResult.IsValid)
            return new Resultado(validationResult.Errors.Select(x => x.ErrorMessage));

        await _repository.Adicionar(churrascoModel);

        return new Resultado();
    }

    public async Task<Resultado> Atualizar(ChurrascoModel churrascoModel)
    {
        var validator = new ChurrascoValidator();
        var validationResult = await validator.ValidateAsync(churrascoModel);

        if (!validationResult.IsValid)
            return new Resultado(validationResult.Errors.Select(x => x.ErrorMessage));

        var churrascoAtualizar = await _repository.BuscarPorId(churrascoModel.Id);

        if(churrascoAtualizar is null)
            return new Resultado("Churrasco não encontrado");
        
        churrascoAtualizar.Atualizar(churrascoModel);
        
        await _repository.Atualizar(churrascoAtualizar);
        
        return new Resultado();
    }

    public async Task<Resultado> Deletar(Guid id)
    {
        var churrascoDeletar = await _repository.BuscarPorId(id);

        if (churrascoDeletar is null)
            return new Resultado("Churrasco não encontrado");
        
        await _repository.Deletar(churrascoDeletar);

        return new Resultado();
    }

    public async Task<ChurrascoModel?> BuscarPorId(Guid id)
    {
        return await _repository.BuscarPorId(id);
    }

    public async Task<IList<ChurrascoModel>> Listar()
    {
        return await _repository.Listar();
    }
}