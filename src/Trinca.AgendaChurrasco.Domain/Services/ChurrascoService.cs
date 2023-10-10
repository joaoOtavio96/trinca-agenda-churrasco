using Trinca.AgendaChurrasco.Domain.Entities;
using Trinca.AgendaChurrasco.Domain.Repositories;
using Trinca.AgendaChurrasco.Domain.Validations;

namespace Trinca.AgendaChurrasco.Domain.Services;

public class ChurrascoService : IChurrascoService
{
    private readonly IChurrascoRepository _repository;

    public ChurrascoService(IChurrascoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Resultado> Adicionar(Churrasco churrasco)
    {
        var validator = new ChurrascoValidator();
        var validationResult = validator.Validate(churrasco);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(x => new Erro { Mensagem = x.ErrorMessage });
            
            return new Resultado { Erros = erros.ToList() };
        }

        await _repository.Adicionar(churrasco);

        return new Resultado();
    }

    public async Task<Resultado> Atualizar(Churrasco churrasco)
    {
        var validator = new ChurrascoValidator();
        var validationResult = await validator.ValidateAsync(churrasco);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(x => new Erro { Mensagem = x.ErrorMessage });
            
            return new Resultado { Erros = erros.ToList() };
        }

        var churrascoAtualizar = await _repository.BuscarPorId(churrasco.Id);

        churrascoAtualizar.Titulo = churrasco.Titulo;
        churrascoAtualizar.Descricao = churrasco.Descricao;
        churrascoAtualizar.Observacao = churrasco.Observacao;
        churrascoAtualizar.Data = churrasco.Data;
        
        await _repository.Atualizar(churrascoAtualizar);
        
        return new Resultado();
    }

    public async Task Deletar(Guid id)
    {
        var churrascoDeletar = await _repository.BuscarPorId(id);

        if (churrascoDeletar is null)
            return;
        
        await _repository.Deletar(churrascoDeletar);
    }
}