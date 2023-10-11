using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Participante;

namespace Trinca.AgendaChurrasco.Api.Participante;

public class ViewModelToModelMapping : Profile
{
    public ViewModelToModelMapping()
    {
        CreateMap<ParticipanteRequestViewModel, ParticipanteModel>()
            .ConstructUsing(x => new ParticipanteModel(x.Nome, x.Valor, x.Pago, x.ChurrascoId));
    }
}