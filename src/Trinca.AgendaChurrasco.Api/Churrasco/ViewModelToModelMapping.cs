using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Churrasco;

namespace Trinca.AgendaChurrasco.Api.Churrasco;

public class ViewModelToModelMapping : Profile
{
    public ViewModelToModelMapping()
    {
        CreateMap<ChurrascoRequestViewModel, ChurrascoModel>()
            .ConstructUsing(x => 
                new ChurrascoModel(
                    x.Titulo,
                    x.Descricao,
                    x.Observacao,
                    x.Data,
                    x.ValorSugeridoSemBebida,
                    x.ValorSugeridoComBebida
                ));
        
    }
}