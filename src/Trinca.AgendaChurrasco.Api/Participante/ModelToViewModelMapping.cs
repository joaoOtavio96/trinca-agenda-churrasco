using AutoMapper;
using Trinca.AgendaChurrasco.Domain.Participante;

namespace Trinca.AgendaChurrasco.Api.Participante;

public class ModelToViewModelMapping : Profile
{
    public ModelToViewModelMapping()
    {
        CreateMap<ParticipanteModel, ParticipanteResponseViewModel>();
    }
}