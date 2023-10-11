using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Api.Churrascos;

public class ChurrascoRequestMapping : Profile
{
    public ChurrascoRequestMapping()
    {
        CreateMap<ChurrascoRequest, Churrasco>()
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