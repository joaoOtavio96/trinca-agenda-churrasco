using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Churrascos;

namespace Trinca.AgendaChurrasco.Api.Churrascos;

public class ChurrascoResponseMapping : Profile
{
    public ChurrascoResponseMapping()
    {
        CreateMap<Churrasco, ChurrascoResponse>()
            .ForMember(x => x.ValorTotal, opt => opt.MapFrom(x => x.CalcularValorTotal()))
            .ForMember(x => x.ParticipantesTotal, opt => opt.MapFrom(x => x.ParticipantesTotal()));
        
        CreateMap<Churrasco, ChurrascoDetalheResponse>()
            .ForMember(x => x.ValorTotal, opt => opt.MapFrom(x => x.CalcularValorTotal()))
            .ForMember(x => x.ParticipantesTotal, opt => opt.MapFrom(x => x.ParticipantesTotal()));

    }
}