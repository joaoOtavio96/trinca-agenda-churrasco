using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Churrasco;

namespace Trinca.AgendaChurrasco.Api.Churrasco;

public class ModelToViewModelMapping : Profile
{
    public ModelToViewModelMapping()
    {
        CreateMap<ChurrascoModel, ChurrascoResponseViewModel>()
            .ForMember(x => x.ValorTotal, opt => opt.MapFrom(x => x.CalcularValorTotal()))
            .ForMember(x => x.ParticipantesTotal, opt => opt.MapFrom(x => x.ParticipantesTotal()));
        
        CreateMap<ChurrascoModel, ChurrascoDetalheResponseViewModel>()
            .ForMember(x => x.ValorTotal, opt => opt.MapFrom(x => x.CalcularValorTotal()))
            .ForMember(x => x.ParticipantesTotal, opt => opt.MapFrom(x => x.ParticipantesTotal()));

    }
}