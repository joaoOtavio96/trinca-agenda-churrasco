using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Participantes;

namespace Trinca.AgendaChurrasco.Api.Participantes;

public class ParticipanteRequestMapping : Profile
{
    public ParticipanteRequestMapping()
    {
        CreateMap<ParticipanteRequest, Participante>()
            .ConstructUsing(x => new Participante(x.Nome, x.Valor, x.Pago, x.ChurrascoId));
    }
}