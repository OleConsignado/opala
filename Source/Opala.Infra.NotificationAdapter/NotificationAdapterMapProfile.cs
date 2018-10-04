using AutoMapper;
using Opala.Core.Domain.Models;
using Opala.Infra.NotificationAdapter.Clients;

namespace Opala.Infra.NotificationAdapter
{
    public class NotificationAdapterMapProfile : Profile
    {
        public NotificationAdapterMapProfile()
        {
            CreateMap<NotificationResponse, NotificationResult>()
                .ForMember(d => d.EnvioId, source => source.MapFrom(s => s.Codigo))
                .ForMember(dest => dest.Response, source => source.MapFrom(s => s.Resp))
                .ForMember(dest => dest.ErroMessage, source => source.MapFrom(s => s.ErroMessage));
        }
    }
}
