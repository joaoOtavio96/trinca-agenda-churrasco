using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Api.Churrascos;

public class ChurrascoRequestMapping : Profile
{
    public ChurrascoRequestMapping()
    {
        CreateMap<ChurrascoRequest, Churrasco>()
            .ForMember(x => 
                x.Id, 
                opt => 
                    opt.MapFrom((_, _, _, ctx) =>
                    {
                        if (ctx.TryGetItems(out var itens))
                        {
                            itens.TryGetValue("Id", out var value);
                            return value;
                        }

                        return null;
                    }))
            .ConstructUsing(x => 
                new Churrasco(
                    x.Titulo,
                    x.Descricao,
                    x.Observacao,
                    x.Data,
                    x.ValorSugeridoSemBebida,
                    x.ValorSugeridoComBebida
                ));
        
    }
}