using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Participantes;

namespace Trinca.AgendaChurrasco.Api.Participantes;

public class ParticipanteResponseMapping : Profile
{
    public ParticipanteResponseMapping()
    {
        CreateMap<Participante, ParticipanteResponse>();
    }
}